Imports CodeStack.SwEx.AddIn
Imports CodeStack.SwEx.AddIn.Attributes
Imports System.Runtime.InteropServices
Imports SolidWorks.Interop.sldworks
Imports CodeStack.SwEx.Common.Attributes
Imports CodeStack.SwEx.Examples.LinkFeatureToExternalFile.My.Resources
Imports System.Windows.Forms
Imports System.ComponentModel

Namespace CodeStack.SwEx.Examples.LinkFeatureToExternalFile

	<ComVisible(True), Guid("82489B11-9D3B-4AC8-89B5-6AF27E0E474C")>
	<AutoRegister("Link To External File")>
	Public Class AddIn
		Inherits SwAddInEx

		<Title("Linked To File (VB.NET)")>
		<Icon(GetType(Resources), NameOf(Resources.linked_part_icon))>
		Private Enum Commands_e
			<Icon(GetType(Resources), NameOf(Resources.linked_part_icon))>
			<Title("Insert link to file")>
			<Description("Inserts bodies linked to external file")>
			InsertLinkToFile
		End Enum

		Public Overrides Function OnConnect() As Boolean
			Me.AddCommandGroup(Of Commands_e)(AddressOf OnButtonClick)
			Return True
		End Function

		Private Sub OnButtonClick(ByVal cmd As Commands_e)
			Select Case cmd
				Case Commands_e.InsertLinkToFile
					InsertLinkToFileMacroFeature()
			End Select
		End Sub

		Private Sub InsertLinkToFileMacroFeature()
			Using openFileDlg = New OpenFileDialog()
				openFileDlg.Filter = "SOLIDWORKS Part Files (*.sldprt)|*.sldprt|All Files (*.*)|*.*"
				openFileDlg.RestoreDirectory = True

				If openFileDlg.ShowDialog() = DialogResult.OK Then
					Dim linkedFilePath = openFileDlg.FileName
					App.IActiveDoc2.FeatureManager.InsertComFeature(Of LinkFileMacroFeature, LinkFileMacroFeatureParameters)(
						New LinkFileMacroFeatureParameters() With
						{
							.LinkedFilePath = linkedFilePath
						})
				End If
			End Using
		End Sub

	End Class

End Namespace
