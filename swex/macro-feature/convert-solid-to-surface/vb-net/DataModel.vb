Imports System.ComponentModel
Imports CodeStack.SwEx.MacroFeature.Attributes
Imports CodeStack.SwEx.PMPage.Attributes
Imports CodeStack.SwEx.MacroFeature.Examples.ConvertSolidToSurface.My.Resources
Imports SolidWorks.Interop.sldworks
Imports CodeStack.SwEx.Common.Attributes

<PageOptions(GetType(Resources), NameOf(Resources.solid_to_surface),
	SolidWorks.Interop.swconst.swPropertyManagerPageOptions_e.swPropertyManagerOptions_CancelButton + SolidWorks.Interop.swconst.swPropertyManagerPageOptions_e.swPropertyManagerOptions_OkayButton)>
<Message("Select solid bodies to convert to surface bodies", "Convert Solid To Surface")>
<Title(GetType(Resources), NameOf(Resources.ConvertSolidToSurfaceTitle))>
Public Class DataModel

	<SelectionBox(SolidWorks.Interop.swconst.swSelectType_e.swSelSOLIDBODIES)>
	<ParameterEditBody>
	<ControlAttribution(GetType(Resources), NameOf(Resources.solid_body))>
	<ControlOptions(,,,,,,, 60)>
	Public Property Bodies As List(Of IBody2)

End Class
