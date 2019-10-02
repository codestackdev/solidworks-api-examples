'**********************
'Examples for SwEx Framework
'Copyright(C) 2019 www.codestack.net
'License: https : //github.com/codestack-net-dev/swex-examples/blob/master/LICENSE
'Product URL: https : //www.codestack.net/labs/solidworks/swex/add-in/
'**********************

Imports System.Xml.Serialization
Imports CodeStack.SwEx.AddIn.Core
Imports CodeStack.SwEx.AddIn.Enums
Imports SolidWorks.Interop.sldworks

Public Class IssuesDocument
	Inherits DocumentHandler

	Const STREAM_NAME As String = "Issues"

	Public Property IssuesControl As IssuesControl

	Dim m_Issues As ObjectModel.ObservableCollection(Of Issue)

	Public Overrides Sub OnInit()
		AddHandler Me.Access3rdPartyData, AddressOf OnAccess3rdPartyData
	End Sub

	Public Overrides Sub OnActivate()
		IssuesControl.DataContext = m_Issues
	End Sub

	Public Sub CreateNewIssue()
		Dim newIssue As New Issue
		Dim maxId As Integer
		If m_Issues.Any() Then
			maxId = m_Issues.Max(Function(item As Issue)
									 Return item.Id
								 End Function)
		End If
		newIssue.Id = maxId + 1
		m_Issues.Add(newIssue)
	End Sub

	Private Sub OnAccess3rdPartyData(docHandler As DocumentHandler, state As Access3rdPartyDataState_e)
		Select Case state
			Case Access3rdPartyDataState_e.StreamRead
				LoadIssuesFromStream()
			Case Access3rdPartyDataState_e.StreamWrite
				SaveIssuesToStream()
		End Select
	End Sub

	Private Sub LoadIssuesFromStream()

		Using streamHandler = Model.Access3rdPartyStream(STREAM_NAME, False)
			If streamHandler.Stream IsNot Nothing Then
				Dim xmlSer = New XmlSerializer(GetType(ObjectModel.ObservableCollection(Of Issue)))
				m_Issues = xmlSer.Deserialize(streamHandler.Stream)
			End If
		End Using

		If m_Issues Is Nothing Then
			m_Issues = New ObjectModel.ObservableCollection(Of Issue)
		End If

		IssuesControl.DataContext = m_Issues

	End Sub

	Private Sub SaveIssuesToStream()
		Using streamHandler = Model.Access3rdPartyStream(STREAM_NAME, True)
			Dim xmlSer = New XmlSerializer(GetType(ObjectModel.ObservableCollection(Of Issue)))
			xmlSer.Serialize(streamHandler.Stream, m_Issues)
		End Using
	End Sub

	Public Overrides Sub OnDestroy()
		RemoveHandler Me.Access3rdPartyData, AddressOf OnAccess3rdPartyData
	End Sub

End Class
