Imports CodeStack.SwEx.AddIn.Attributes
Imports CodeStack.SwEx.AddIn.Enums
Imports CodeStack.SwEx.AddIn.Examples.SwExportComponent.My.Resources
Imports System.ComponentModel
Imports SolidWorks.Interop.swconst
Imports CodeStack.SwEx.Common.Attributes

<Title(GetType(Resources), NameOf(Resources.ToolbarTitle))>
<Description("Exports selected component to neutral format")>
<Icon(GetType(Resources), NameOf(Resources.export))>
Public Enum ExportCommands_e

	<Title("Export To Parasolid")>
	<Description("Exports selected component to parasolid")>
	<Icon(GetType(Resources), NameOf(Resources.export_parasolid))>
	<CommandItemInfo(True, True, swWorkspaceTypes_e.Assembly,
					 True, swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow)>
	Parasolid

	<Title("Export To Iges")>
	<Description("Exports selected component to iges")>
	<Icon(GetType(Resources), NameOf(Resources.export_iges))>
	<CommandItemInfo(True, True, swWorkspaceTypes_e.Assembly,
					 True, swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow)>
	Iges

	<Title("Export To Step")>
	<Description("Exports selected component to step")>
	<Icon(GetType(Resources), NameOf(Resources.export_step))>
	<CommandItemInfo(True, True, swWorkspaceTypes_e.Assembly,
					 True, swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow)>
	[Step]

End Enum
