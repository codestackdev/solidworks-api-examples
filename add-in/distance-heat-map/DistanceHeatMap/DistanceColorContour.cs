using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swpublished;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeStack.SwEx.AddIn.Examples.DistanceHeatMap
{
    [ComVisible(true)]
    public class DistanceColorContour : ISwColorContour1
    {
        private readonly IPartDoc m_Part;

        internal DistanceColorContour(IPartDoc part)
        {
            m_Part = part;
        }

        public int Color(double value)
        {
            var box = m_Part.GetPartBox(true) as double[];
            var maxDist = GetDistance(box[0], box[1], box[2], box[3], box[4], box[5]);

            var colorCode = value / maxDist;

            var color = MapToHeatColor(colorCode);

            return ColorTranslator.ToWin32(color);
        }

        public string DisplayString(double value)
        {
            return $"Distance to selected point or origin is {value*1000} m";
        }

        public bool NeedsUpdate()
        {
            return true;
        }

        public int Value(object face, float vertexX, float vertexY, float vertexZ,
            float normalX, float normalY, float normalZ, out double value)
        {
            var selPt = (m_Part as IModelDoc2).ISelectionManager.GetSelectionPoint2(1, -1) as double[];

            if (selPt == null)
            {
                selPt = new double[3];
            }

            value = GetDistance(vertexX, vertexY, vertexZ, selPt[0], selPt[1], selPt[2]);

            return 0;
        }
        
        //Based on http://csharphelper.com/blog/2014/09/map-numeric-values-to-colors-in-a-rainbow-in-c/
        private Color MapToHeatColor(double value)
        {
            int colorCode = (int)(1023 * value);

            if (colorCode < 256)
            {
                return System.Drawing.Color.FromArgb(255, colorCode, 0);
            }
            else if (colorCode < 512)
            {
                colorCode -= 256;
                return System.Drawing.Color.FromArgb(255 - colorCode, 255, 0);
            }
            else if (colorCode < 768)
            {
                colorCode -= 512;
                return System.Drawing.Color.FromArgb(0, 255, colorCode);
            }
            else
            {
                colorCode -= 768;
                return System.Drawing.Color.FromArgb(0, 255 - colorCode, 255);
            }
        }

        private double GetDistance(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2) + Math.Pow(z2 - z1, 2));
        }
    }
}
