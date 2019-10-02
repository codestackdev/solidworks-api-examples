using CodeStack.SwEx.AddIn.Base;
using CodeStack.SwEx.AddIn.Core;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeStack.SwEx.AddIn.Examples.DistanceHeatMap
{
    public class ColorizerDocument : IDocumentHandler
    {
        private DistanceColorContour m_Contour;
        private IModelDoc2 m_Model;

        public void Init(ISldWorks app, IModelDoc2 model)
        {
            if (model is IPartDoc)
            {
                m_Contour = new DistanceColorContour(model as IPartDoc);
                m_Model = model;
                m_Model.Extension.InstallModelColorizer(m_Contour);
            }
        }

        public void Dispose()
        {
            if (m_Contour != null)
            {
                m_Model.Extension.RemoveModelColorizer(m_Contour);
            }
        }
    }
}
