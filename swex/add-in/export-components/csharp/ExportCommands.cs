using CodeStack.SwEx.AddIn.Attributes;
using CodeStack.SwEx.AddIn.Enums;
using CodeStack.SwEx.AddIn.Examples.SwExportComponent.Properties;
using CodeStack.SwEx.Common.Attributes;
using SolidWorks.Interop.swconst;
using System.ComponentModel;

namespace CodeStack.SwEx.AddIn.Examples.SwExportComponent
{
    [Title(typeof(Resources), nameof(Resources.ToolbarTitle))]
    [Description("Exports selected component to neutral format")]
    [Icon(typeof(Resources), nameof(Resources.export))]
    public enum ExportCommands_e
    {
        [Title("Export To Parasolid")]
        [Description("Exports selected component to parasolid")]
        [Icon(typeof(Resources), nameof(Resources.export_parasolid))]
        [CommandItemInfo(true, true, swWorkspaceTypes_e.Assembly,
            true, swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow)]
        Parasolid,

        [Title("Export To Iges")]
        [Description("Exports selected component to iges")]
        [Icon(typeof(Resources), nameof(Resources.export_iges))]
        [CommandItemInfo(true, true, swWorkspaceTypes_e.Assembly,
            true, swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow)]
        Iges,

        [Title("Export To Step")]
        [Description("Exports selected component to step")]
        [Icon(typeof(Resources), nameof(Resources.export_step))]
        [CommandItemInfo(true, true, swWorkspaceTypes_e.Assembly,
            true, swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow)]
        Step
    }
}
