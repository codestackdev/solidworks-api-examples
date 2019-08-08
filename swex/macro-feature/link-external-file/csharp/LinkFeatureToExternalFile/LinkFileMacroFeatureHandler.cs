using CodeStack.SwEx.MacroFeature.Base;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.IO;
using System.Linq;

namespace CodeStack.SwEx.Examples.LinkFeatureToExternalFile
{
    public class LinkFileMacroFeatureHandler : IMacroFeatureHandler
    {
        private ISldWorks m_App;

        internal IModelDoc2 Model { get; private set; }
        internal IFeature Feature { get; private set; }
        internal long LastUpdateStamp { get; private set; }
        internal IBody2[] CachedBodies { get; private set; }
        internal Exception LastError { get; private set; }

        public void Init(ISldWorks app, IModelDoc2 model, IFeature feat)
        {
            m_App = app;
            Model = model;
            Feature = feat;

            if (Model is PartDoc)
            {
                (Model as PartDoc).RegenNotify += OnPreRegeneration;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private int OnPreRegeneration()
        {
            var featData = Feature.GetDefinition() as IMacroFeatureData;

            var parameters = featData.GetParameters<LinkFileMacroFeatureParameters>(Feature, Model);
            UpdateCachedBodyIfNeeded(parameters);

            return 0;
        }

        private void UpdateCachedBodyIfNeeded(LinkFileMacroFeatureParameters parameters)
        {
            LastError = null;
            IModelDoc2 refDoc = null;
            bool isRefDocLoaded = false;

            try
            {
                if (File.Exists(parameters.LinkedFilePath))
                {
                    LastUpdateStamp = File.GetLastWriteTimeUtc(parameters.LinkedFilePath).Ticks;

                    refDoc = m_App.GetOpenDocumentByName(parameters.LinkedFilePath) as IModelDoc2;

                    isRefDocLoaded = refDoc != null;

                    if (LastUpdateStamp != parameters.FileLastUpdateTimeStamp
                        || (isRefDocLoaded && refDoc.GetSaveFlag()) || CachedBodies == null)
                    {
                        if (!isRefDocLoaded)
                        {
                            m_App.DocumentVisible(false, (int)swDocumentTypes_e.swDocPART);

                            var docSpec = m_App.GetOpenDocSpec(parameters.LinkedFilePath) as IDocumentSpecification;
                            docSpec.Silent = true;
                            docSpec.ReadOnly = true;

                            refDoc = m_App.OpenDoc7(docSpec);

                            if (refDoc == null)
                            {
                                throw new InvalidOperationException($"Failed to load the referenced file ${docSpec.FileName} with error: {(swFileLoadError_e)docSpec.Error}");
                            }
                        }

                        if (refDoc is IPartDoc)
                        {
                            var bodies = (refDoc as IPartDoc).GetBodies2((int)swBodyType_e.swAllBodies, true) as object[];

                            if (bodies != null && bodies.Any())
                            {
                                var resBodies = bodies.Cast<IBody2>().Select(b => b.ICopy()).ToArray();

                                CachedBodies = resBodies;
                            }
                            else
                            {
                                throw new InvalidOperationException("No bodies in the referenced document");
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException("Referenced document is not a part");
                        }
                    }
                }
                else
                {
                    throw new FileNotFoundException($"Linked file '${parameters.LinkedFilePath}' is not found");
                }
            }
            catch (Exception ex)
            {
                LastError = ex;
            }
            finally
            {
                m_App.DocumentVisible(true, (int)swDocumentTypes_e.swDocPART);

                if (!isRefDocLoaded && refDoc != null)
                {
                    m_App.CloseDoc(refDoc.GetTitle());
                }
            }
        }

        public void Unload(MacroFeatureUnloadReason_e reason)
        {
            if (Model is PartDoc)
            {
                (Model as PartDoc).RegenNotify -= OnPreRegeneration;
            }
        }
    }
}
