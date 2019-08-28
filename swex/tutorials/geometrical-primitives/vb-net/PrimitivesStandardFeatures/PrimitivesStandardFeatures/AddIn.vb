'**********************
'SwEx - development tools for SOLIDWORKS
'Copyright(C) 2019 www.codestack.net
'License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
'Product URL: https://www.codestack.net/labs/solidworks/swex
'**********************

Imports CodeStack.SwEx.AddIn
Imports CodeStack.SwEx.AddIn.Attributes
Imports CodeStack.SwEx.AddIn.Enums
Imports CodeStack.SwEx.Common.Attributes
Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports CodeStack.PrimitivesStandardFeatures.My.Resources

<ComVisible(True), Guid("AE6C7933-945B-466D-A221-6E239F2D51CF")>
<AutoRegister("Geometry Primitives (VB.NET)", "Create geometrical primitives")>
Public Class AddIn
	Inherits SwAddInEx

	<Title("Primitives")>
	<Description("Creates geometricas primitives")>
	<Icon(GetType(Resources), NameOf(Resources.primitives_icon))>
	Private Enum Commands_e

		<Title("Create Cylinder")>
		<Description("Creates extruded cylinder on selected face or plane")>
		<Icon(GetType(Resources), NameOf(Resources.cylinder_icon))>
		<CommandItemInfo(True, True, swWorkspaceTypes_e.Part, True)>
		CreateCylinder

		<Title("Create Box")>
		<Description("Creates extruded box on selected face or plane")>
		<Icon(GetType(Resources), NameOf(Resources.box_icon))>
		<CommandItemInfo(True, True, swWorkspaceTypes_e.Part, True)>
		CreateBox

	End Enum

	Public Overrides Function OnConnect() As Boolean
		AddCommandGroup(Of Commands_e)(AddressOf OnButtonClick, AddressOf OnButtonEnable)
		Return True
	End Function

	Private Sub OnButtonClick(ByVal cmd As Commands_e)
		Try

			Select Case cmd
				Case Commands_e.CreateBox
					App.IActiveDoc2.CreateBox(0.1, 0.2, 0.3)
				Case Commands_e.CreateCylinder
					App.IActiveDoc2.CreateCylinder(0.1, 0.2)
			End Select

		Catch ex As Exception
			App.SendMsgToUser2(ex.Message, CInt(swMessageBoxIcon_e.swMbStop), CInt(swMessageBoxBtn_e.swMbOk))
		End Try
	End Sub

	Private Sub OnButtonEnable(ByVal cmd As Commands_e, ByRef state As CommandItemEnableState_e)
		Select Case cmd
			Case Commands_e.CreateBox, Commands_e.CreateCylinder
				Dim selType = CType(App.IActiveDoc2.ISelectionManager.GetSelectedObjectType3(1, -1), swSelectType_e)

				If TypeOf App.IActiveDoc2 Is IPartDoc AndAlso (selType = swSelectType_e.swSelFACES OrElse selType = swSelectType_e.swSelDATUMPLANES) Then

					If selType = swSelectType_e.swSelFACES Then
						Dim face = TryCast(App.IActiveDoc2.ISelectionManager.GetSelectedObject6(1, -1), IFace2)

						If Not face.IGetSurface().IsPlane() Then
							state = CommandItemEnableState_e.DeselectDisable
						End If
					End If
				Else
					state = CommandItemEnableState_e.DeselectDisable
				End If
		End Select
	End Sub
End Class
