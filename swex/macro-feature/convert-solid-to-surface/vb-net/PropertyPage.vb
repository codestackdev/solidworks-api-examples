Imports System.Runtime.InteropServices
Imports CodeStack.SwEx.PMPage
Imports CodeStack.SwEx.PMPage.Base
Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst

<ComVisible(True)>
Public Class PropertyPageHandler
	Inherits PropertyManagerPageHandlerEx

End Class

Public Delegate Sub PageClosedDelegate(model As IModelDoc2, feat As IFeature, featData As IMacroFeatureData, data As DataModel, isOk As Boolean)

Public Class PropertyPage

	Public Event PageClosed As PageClosedDelegate

	ReadOnly m_Page As PropertyManagerPageEx(Of PropertyPageHandler, DataModel)

	ReadOnly m_App As ISldWorks
	ReadOnly m_Model As IModelDoc2
	ReadOnly m_Data As DataModel
	ReadOnly m_Feat As IFeature
	Dim m_FeatData As IMacroFeatureData

	Sub New(app As ISldWorks, model As IModelDoc2, data As DataModel)

		m_App = app
		m_Model = model
		m_Data = data

		m_Page = New PropertyManagerPageEx(Of PropertyPageHandler, DataModel)(m_App)
		AddHandler m_Page.Handler.Closed, AddressOf OnPageClosed
		AddHandler m_Page.Handler.Closing, AddressOf OnClosing
	End Sub

	Sub New(app As ISldWorks, model As IModelDoc2, feat As IFeature, featData As IMacroFeatureData, data As DataModel)
		Me.New(app, model, data)
		m_Feat = feat
		m_FeatData = featData
	End Sub

	Public Sub Show()
		m_Page.Show(m_Data)
	End Sub

	Private Sub OnPageClosed(reason As swPropertyManagerPageCloseReasons_e)
		RaiseEvent PageClosed(m_Model, m_Feat, m_FeatData, m_Data, reason = swPropertyManagerPageCloseReasons_e.swPropertyManagerPageClose_Okay)
	End Sub

	Private Sub OnClosing(reason As swPropertyManagerPageCloseReasons_e, arg As ClosingArg)
		If reason = swPropertyManagerPageCloseReasons_e.swPropertyManagerPageClose_Okay Then
			If m_Data.Bodies Is Nothing Or m_Data.Bodies.Count = 0 Then
				arg.ErrorMessage = "Please select bodies to convert"
				arg.ErrorTitle = "Convert Solid To Surface"
				arg.Cancel = True
			End If
		End If
    End Sub

End Class
