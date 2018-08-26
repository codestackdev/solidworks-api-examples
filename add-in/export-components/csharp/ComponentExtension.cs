using SolidWorks.Interop.swconst;
using System;

namespace SolidWorks.Interop.sldworks
{
    public static class ComponentExtension
    {
        public static void Export(this IComponent2 comp, string filePath)
        {
            var model = comp.IGetModelDoc();

            if (model != null)
            {
                int err = -1;
                int warn = -1;
                
                model.Extension.SaveAs(filePath, (int)swSaveAsVersion_e.swSaveAsCurrentVersion,
                    (int)swSaveAsOptions_e.swSaveAsOptions_Silent, null, ref err, ref warn);
            }
            else
            {
                throw new NullReferenceException("Model");
            }
        }
    }
}
