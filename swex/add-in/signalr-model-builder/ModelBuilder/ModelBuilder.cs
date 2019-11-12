//**********************
//Connect Web Page To Desktop Application
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/solidworks-api-examples/blob/master/LICENSE
//Product URL: https://blog.codestack.net/connect-web-page-desktop
//**********************

using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.IO;

namespace CodeStack.ModelBuilder
{
    public class ModelBuilder
    {
        private readonly ISldWorks m_App;
        private readonly string m_BuildDir;

        private int m_TotalModelsBuilt;

        public ModelBuilder(ISldWorks app, string buildDir)
        {
            m_BuildDir = buildDir;
            m_App = app;
        }

        public double Build(double width, double height, double length, out int totalModelsBuilt)
        {
            m_TotalModelsBuilt++;

            var templatePath = m_App.GetUserPreferenceStringValue((int)swUserPreferenceStringValue_e.swDefaultTemplatePart);

            var part = m_App.NewDocument(templatePath, (int)swDwgPaperSizes_e.swDwgPapersUserDefined, 0, 0) as IPartDoc;

            var box = m_App.IGetModeler().CreateBodyFromBox(
                new double[]
                {
                0, 0, 0,
                1, 0, 0,
                width, length, height
                });

            var feat = part.CreateFeatureFromBody3(box, false, (int)swCreateFeatureBodyOpts_e.swCreateFeatureBodySimplify);

            int errs = -1;
            int warns = -1;

            var outPath = Path.Combine(m_BuildDir, $"{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")}.sldprt");

            var dir = Path.GetDirectoryName(outPath);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            (part as IModelDoc2).Extension.SaveAs(outPath, (int)swSaveAsVersion_e.swSaveAsCurrentVersion,
                (int)swSaveAsOptions_e.swSaveAsOptions_Silent, null, ref errs, ref warns);

            totalModelsBuilt = m_TotalModelsBuilt;

            var mass = (part as IModelDoc2).Extension.CreateMassProperty().Mass;

            m_App.CloseDoc((part as IModelDoc2).GetTitle());

            return mass;
        }
    }
}
