using CodeStack.SwEx.PMPage.Attributes;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace InsertNote
{
    [PageOptions(swPropertyManagerPageOptions_e.swPropertyManagerOptions_CancelButton
        | swPropertyManagerPageOptions_e.swPropertyManagerOptions_OkayButton)]
    public class NoteData
    {
        [Description("Note Text")]
        public string Text { get; set; }

        [Description("Font Size")]
        [NumberBoxOptions(swNumberboxUnitType_e.swNumberBox_UnitlessInteger, 8, 100, 5, true,
            10, 1, swPropMgrPageNumberBoxStyle_e.swPropMgrPageNumberBoxStyle_Thumbwheel)]
        public int Size { get; set; } = 48;

        public NotePosition Position { get; set; } = new NotePosition();
    }

    public class NotePosition
    {
        [Description("Attached Entity")]
        [SelectionBox(swSelectType_e.swSelFACES, swSelectType_e.swSelEDGES, swSelectType_e.swSelVERTICES)]
        [ControlAttribution(swControlBitmapLabelType_e.swBitmapLabel_SelectEdgeFaceVertex)]
        public IEntity AttachedEntity { get; set; }

        [Description("X Offset")]
        [NumberBoxOptions(swNumberboxUnitType_e.swNumberBox_Length, 0, 1000, 0.01, true, 0.02, 0.001)]
        public double X { get; set; }

        [Description("Y Offset")]
        [NumberBoxOptions(swNumberboxUnitType_e.swNumberBox_Length, 0, 1000, 0.01, true, 0.02, 0.001)]
        public double Y { get; set; }
    }
}
