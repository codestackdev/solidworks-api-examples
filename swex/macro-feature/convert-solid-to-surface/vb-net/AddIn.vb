Imports System.Runtime.InteropServices
Imports CodeStack.SwEx.AddIn
Imports CodeStack.SwEx.AddIn.Attributes
Imports CodeStack.SwEx.Common.Attributes
Imports CodeStack.SwEx.MacroFeature.Examples.ConvertSolidToSurface.My.Resources
Imports SolidWorks.Interop.sldworks

<Icon(GetType(Resources), NameOf(Resources.solid_to_surface))>
<Title(GetType(Resources), NameOf(Resources.ConvertSolidToSurfaceTitle))>
<Summary(GetType(Resources), NameOf(Resources.ConvertSolidToSurfaceDescription))>
Public Enum Commands_e
	<Icon(GetType(Resources), NameOf(Resources.solid_to_surface))>
	<Title(GetType(Resources), NameOf(Resources.ConvertSolidToSurfaceTitle))>
	<Summary(GetType(Resources), NameOf(Resources.ConvertSolidToSurfaceDescription))>
	<CommandItemInfo(True, True, Enums.swWorkspaceTypes_e.Part, True)>
	ConvertSolidToSurface
End Enum

<ComVisible(True)>
<Guid("E7210006-B2FD-488C-86B1-EADDD03EDC2C")>
<AutoRegister>
<Title(GetType(Resources), NameOf(Resources.ConvertSolidToSurfaceTitle))>
<Summary(GetType(Resources), NameOf(Resources.ConvertSolidToSurfaceDescription))>
Public Class AddIn
	Inherits SwAddInEx

	Dim m_Page As PropertyPage

	Public Overrides Function OnConnect() As Boolean
		Me.AddCommandGroup(Of Commands_e)(AddressOf OnButtonClicked)
		Return True
	End Function

	Private Sub OnButtonClicked(btn As Commands_e)
		Select Case btn
			Case Commands_e.ConvertSolidToSurface
				m_Page = New PropertyPage(App, App.IActiveDoc2, New DataModel())
				AddHandler m_Page.PageClosed, AddressOf OnPageClosed
				m_Page.Show()
		End Select
	End Sub

	Private Sub OnPageClosed(model As IModelDoc2, feat As IFeature, featData As IMacroFeatureData, data As DataModel, isOk As Boolean)
		If isOk Then
			model.FeatureManager.InsertComFeature(Of MacroFeature, DataModel)(data)
		End If
	End Sub

End Class
