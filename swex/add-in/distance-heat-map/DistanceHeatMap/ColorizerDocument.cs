using CodeStack.SwEx.AddIn.Core;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeStack.SwEx.AddIn.Examples.DistanceHeatMap
{
    public class ColorizerDocument : DocumentHandler
    {
        private DistanceColorContour m_Contour;

        public override void OnInit()
        {
            if (Model is IPartDoc)
            {
                m_Contour = new DistanceColorContour(Model as IPartDoc);
                Model.Extension.InstallModelColorizer(m_Contour);
            }
        }

        public override void OnDestroy()
        {
            if (m_Contour != null)
            {
                Model.Extension.RemoveModelColorizer(m_Contour);
            }
        }
    }
}
