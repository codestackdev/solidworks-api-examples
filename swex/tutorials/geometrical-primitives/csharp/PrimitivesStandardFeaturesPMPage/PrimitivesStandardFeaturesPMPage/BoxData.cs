//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using CodeStack.PrimitivesStandardFeaturesPMPage.Properties;
using CodeStack.SwEx.Common.Attributes;
using CodeStack.SwEx.PMPage.Attributes;
using CodeStack.SwEx.PMPage.Base;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeStack.SwEx.PMPage.Controls;
using SolidWorks.Interop.sldworks;

namespace CodeStack.PrimitivesStandardFeaturesPMPage
{
    [Title(typeof(Resources), nameof(Resources.CommandTitleCreateBox))]
    [Icon(typeof(Resources), nameof(Resources.box_icon))]
    [PageOptions(swPropertyManagerPageOptions_e.swPropertyManagerOptions_CancelButton | swPropertyManagerPageOptions_e.swPropertyManagerOptions_OkayButton)]
    public class BoxData
    {
        [SelectionBox(typeof(ReferenceSelectionCustomFilter), swSelectType_e.swSelFACES, swSelectType_e.swSelDATUMPLANES)]
        [ControlAttribution(swControlBitmapLabelType_e.swBitmapLabel_SelectFace)]
        [Description("Base face")]
        [ControlOptions(height: 12)]
        public IEntity Reference { get; set; }

        [ControlAttribution(swControlBitmapLabelType_e.swBitmapLabel_Width)]
        [Description("Distance in X direction")]
        [NumberBoxOptions(swNumberboxUnitType_e.swNumberBox_Length, 0, 1000, 0.01, false, 0.1, 0.001, swPropMgrPageNumberBoxStyle_e.swPropMgrPageNumberBoxStyle_Thumbwheel)]
        public double Width { get; set; }

        [ControlAttribution(swControlBitmapLabelType_e.swBitmapLabel_LinearDistance)]
        [Description("Distance in Y direction")]
        [NumberBoxOptions(swNumberboxUnitType_e.swNumberBox_Length, 0, 1000, 0.01, false, 0.1, 0.001, swPropMgrPageNumberBoxStyle_e.swPropMgrPageNumberBoxStyle_Thumbwheel)]
        public double Length { get; set; }

        [Description("Distance in Z direction")]
        [NumberBoxOptions(swNumberboxUnitType_e.swNumberBox_Length, 0, 1000, 0.01, false, 0.1, 0.001, swPropMgrPageNumberBoxStyle_e.swPropMgrPageNumberBoxStyle_Thumbwheel)]
        [Icon(typeof(Resources), nameof(Resources.height_icon))]
        public double Height { get; set; }
    }
}
