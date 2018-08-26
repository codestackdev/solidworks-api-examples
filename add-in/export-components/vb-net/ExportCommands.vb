Imports CodeStack.SwEx.AddIn.Attributes
Imports CodeStack.SwEx.AddIn.Enums
Imports CodeStack.SwEx.AddIn.Examples.SwExportComponent.My.Resources
Imports System.ComponentModel

<Title("Export Components")>
<Description("Exports selected component to neutral format")>
<Icon(GetType(Resources), NameOf(Resources.export))>
Public Enum ExportCommands_e

    <Title("Export To Parasolid")>
    <Description("Exports selected component to parasolid")>
    <Icon(GetType(Resources), NameOf(Resources.export_parasolid))>
    <CommandItemInfo(True, True, swWorkspaceTypes_e.Assembly)>
    Parasolid

    <Title("Export To Iges")>
    <Description("Exports selected component to iges")>
    <Icon(GetType(Resources), NameOf(Resources.export_iges))>
    <CommandItemInfo(True, True, swWorkspaceTypes_e.Assembly)>
    Iges

    <Title("Export To Step")>
    <Description("Exports selected component to step")>
    <Icon(GetType(Resources), NameOf(Resources.export_step))>
    <CommandItemInfo(True, True, swWorkspaceTypes_e.Assembly)>
    [Step]

End Enum
