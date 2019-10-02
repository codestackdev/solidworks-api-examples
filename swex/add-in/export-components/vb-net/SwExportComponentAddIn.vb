Imports CodeStack.SwEx.AddIn.Attributes
Imports CodeStack.SwEx.AddIn.Enums
Imports SolidWorks.Interop.sldworks
Imports System.IO
Imports System.Runtime.InteropServices

<AutoRegister("Export Components (VB.NET)", "Exports selected component to neutral format", True)>
<Guid("5A40017C-81AB-4A1F-AD03-35B02CDD0CA1")>
<ComVisible(True)>
Public Class SwExportComponentAddIn
    Inherits SwAddInEx

    Public Overrides Function OnConnect() As Boolean

        'TODO init the resources
        'TODO: validate license

        AddCommandGroup(Of ExportCommands_e)(AddressOf OnCommand, AddressOf OnEnableCommand)

        'TODO: return false if error occurs (e.g. license is not validated)
        Return True

    End Function

    Public Overrides Function OnDisconnect() As Boolean

        'TODO: clean the resources
        'TODO: delete temp files
        Return True

    End Function

    Private Sub OnCommand(ByVal cmd As ExportCommands_e)
        Dim ext = ""

        Select Case cmd
            Case ExportCommands_e.Parasolid
                ext = ".x_t"
            Case ExportCommands_e.Iges
                ext = ".igs"
            Case ExportCommands_e.[Step]
                ext = ".step"
            Case Else
                Throw New NotSupportedException()
        End Select

		Dim comp = TryCast(App.IActiveDoc2.ISelectionManager.GetSelectedObjectsComponent4(1, -1),
				IComponent2)

		If comp IsNot Nothing Then
			Dim filePath = Path.Combine(Path.GetDirectoryName(
											App.IActiveDoc2.GetPathName()),
											"Export",
											Path.GetFileNameWithoutExtension(comp.GetPathName()) & ext)
			comp.Export(filePath)
        Else
            Debug.Assert(False, "Command should be disabled")
            Throw New NullReferenceException("Component")
        End If

    End Sub

    Private Sub OnEnableCommand(ByVal cmd As ExportCommands_e, ByRef state As CommandItemEnableState_e)

        'state Is already calculated based on swWorkspaceTypes_e value specified for the
        'command (i.e. in this case if the active model Is no an assembly the state of the button will be
        'DeselectDisable. So we only need to verify if the state is DeselectEnable
        If state = CommandItemEnableState_e.DeselectEnable Then
			If App.IActiveDoc2.ISelectionManager.GetSelectedObjectsComponent4(1, -1) Is Nothing Then
				'if no components selected deselect and disable the command
				state = CommandItemEnableState_e.DeselectDisable
			End If
		End If

    End Sub

End Class
