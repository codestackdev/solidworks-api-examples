//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using CodeStack.PrimitivesStandardFeaturesPMPage.Properties;
using CodeStack.SwEx.Common.Attributes;
using CodeStack.SwEx.PMPage.Attributes;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeStack.PrimitivesStandardFeaturesPMPage
{
    [Title(typeof(Resources), nameof(Resources.CommandTitleCreateCylinder))]
    [Icon(typeof(Resources), nameof(Resources.cylinder_icon))]
    [PageOptions(swPropertyManagerPageOptions_e.swPropertyManagerOptions_CancelButton | swPropertyManagerPageOptions_e.swPropertyManagerOptions_OkayButton)]
    public class CylinderData
    {
        [SelectionBox(typeof(ReferenceSelectionCustomFilter), swSelectType_e.swSelFACES, swSelectType_e.swSelDATUMPLANES)]
        [ControlAttribution(swControlBitmapLabelType_e.swBitmapLabel_SelectFace)]
        [Description("Base face")]
        [ControlOptions(height: 12)]
        public IEntity Reference { get; set; }

        [ControlAttribution(swControlBitmapLabelType_e.swBitmapLabel_Diameter)]
        [Description("Diameter of base")]
        [NumberBoxOptions(swNumberboxUnitType_e.swNumberBox_Length, 0, 1000, 0.01, false, 0.1, 0.001, swPropMgrPageNumberBoxStyle_e.swPropMgrPageNumberBoxStyle_Thumbwheel)]
        public double Diameter { get; set; }

        [Description("Distance in Z direction")]
        [NumberBoxOptions(swNumberboxUnitType_e.swNumberBox_Length, 0, 1000, 0.01, false, 0.1, 0.001, swPropMgrPageNumberBoxStyle_e.swPropMgrPageNumberBoxStyle_Thumbwheel)]
        [Icon(typeof(Resources), nameof(Resources.height_icon))]
        public double Height { get; set; }
    }
}
