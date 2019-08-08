using CodeStack.SwEx.MacroFeature;
using CodeStack.SwEx.MacroFeature.Attributes;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using CodeStack.SwEx.MacroFeature.Base;
using SolidWorks.Interop.sldworks;
using CodeStack.SwEx.Common.Attributes;
using CodeStack.SwEx.Examples.LinkFeatureToExternalFile.Properties;

namespace CodeStack.SwEx.Examples.LinkFeatureToExternalFile
{

    [ComVisible(true), Guid("3590F6C4-6345-4A0A-9161-196896F3B335")]
    [ProgId("CodeStack.LinkFileMacroFeature.CSharp")]
    [Options("LinkFileMacroFeature")]
    [Icon(typeof(Resources), nameof(Resources.linked_part_icon))]
    public class LinkFileMacroFeature : MacroFeatureEx<LinkFileMacroFeatureParameters, LinkFileMacroFeatureHandler>
    {
        protected override MacroFeatureRebuildResult OnRebuild(LinkFileMacroFeatureHandler handler, LinkFileMacroFeatureParameters parameters)
        {
            if (handler.LastError == null)
            {
                parameters.FileLastUpdateTimeStamp = handler.LastUpdateStamp;

                SetParameters(handler.Model, handler.Feature, handler.Feature.GetDefinition() as IMacroFeatureData, parameters);

                return MacroFeatureRebuildResult.FromBodies(
                    handler.CachedBodies.Select(b => b.ICopy()).ToArray(), 
                    handler.Feature.GetDefinition() as IMacroFeatureData);
            }
            else
            {
                return MacroFeatureRebuildResult.FromStatus(false, handler.LastError.ToString());
            }
        }
    }
}
