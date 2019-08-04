using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsertNote
{
    public static class NoteHelper
    {
        public static void InsertNote(IDrawingDoc draw, string text, NoteSize size)
        {
            int height = 0;

            switch (size)
            {
                case NoteSize.Small:
                    height = 24;
                    break;

                case NoteSize.Normal:
                    height = 36;
                    break;

                case NoteSize.Large:
                    height = 64;
                    break;
            }

            InsertNote(draw, text, height, 0, 0, null);
        }

        public static void InsertNote(IDrawingDoc draw, string text, int height, double offsetX, double offsetY, IEntity entity)
        {
            double x = 0;
            double y = 0;

            if (entity != null)
            {
                entity.Select4(false, null);

                var view = (draw as IModelDoc2).ISelectionManager.GetSelectedObjectsDrawingView2(1, -1);

                var viewOutline = view.GetOutline() as double[];

                x = viewOutline[0] + offsetX;
                y = viewOutline[1] - offsetY;
            }
            else
            {
                double sheetWidth = 0;
                double sheetHeight = 0;
                draw.IGetCurrentSheet().GetSize(ref sheetWidth, ref sheetHeight);

                x = offsetX;
                y = sheetHeight - offsetY;
            }

            var note = (draw as IModelDoc2).InsertNote(text) as INote;

            note.SetHeightInPoints(height);

            note.IGetAnnotation().SetPosition2(x, y, 0);
        }
    }
}
