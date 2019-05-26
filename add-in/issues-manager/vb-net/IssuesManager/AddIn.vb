'**********************
'Examples for SwEx Framework
'Copyright(C) 2019 www.codestack.net
'License: https : //github.com/codestack-net-dev/swex-examples/blob/master/LICENSE
'Product URL: https : //www.codestack.net/labs/solidworks/swex/add-in/
'**********************

Imports System.Runtime.InteropServices
Imports CodeStack.SwEx.AddIn.Attributes
Imports CodeStack.SwEx.AddIn.Base
Imports CodeStack.SwEx.AddIn.Examples.IssuesManager.My.Resources
Imports CodeStack.SwEx.Common.Attributes

<ComVisible(True)>
<AutoRegister("Issues Manager")>
Public Class AddIn
	Inherits SwAddInEx

	<Icon(GetType(Resources), NameOf(Resources.issues_icon))>
	<Title("Issues Manager")>
	Enum IssuesManagerCommands_e
		<Icon(GetType(Resources), NameOf(Resources.new_issue_icon))>
		<Title("Create New Issue")>
		CreateNewIssue
	End Enum

	Dim m_Ctrl As IssuesControl
	Dim m_DocsHandler As IDocumentsHandler(Of IssuesDocument)

	Public Overrides Function OnConnect() As Boolean
		Dim ctrl As IssuesControlHost = Nothing
		CreateTaskPane(Of IssuesControlHost, IssuesManagerCommands_e)(AddressOf OnCreateNewIssue, ctrl)
		m_Ctrl = ctrl.IssuesControl
		m_DocsHandler = CreateDocumentsHandler(Of IssuesDocument)()
		AddHandler m_DocsHandler.HandlerCreated, AddressOf OnDocumentHandlerCreated

		Return True
	End Function

	Sub OnCreateNewIssue(cmd As IssuesManagerCommands_e)
		Select Case cmd
			Case IssuesManagerCommands_e.CreateNewIssue
				m_DocsHandler(App.IActiveDoc2).CreateNewIssue()
		End Select
	End Sub

	Sub OnDocumentHandlerCreated(docsHandler As IssuesDocument)
		docsHandler.IssuesControl = m_Ctrl
	End Sub

End Class
