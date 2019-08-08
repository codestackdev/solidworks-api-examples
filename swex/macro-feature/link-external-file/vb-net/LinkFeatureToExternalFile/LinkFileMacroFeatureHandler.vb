Imports CodeStack.SwEx.MacroFeature.Base
Imports SolidWorks.Interop.sldworks
Imports System.IO
Imports SolidWorks.Interop.swconst

Public Class LinkFileMacroFeatureHandler
	Implements IMacroFeatureHandler

	Private m_App As ISldWorks
	Friend Property Model As IModelDoc2
	Friend Property Feature As IFeature
	Friend Property LastUpdateStamp As Long
	Friend Property CachedBodies As IBody2()
	Friend Property LastError As Exception

	Public Sub Init(ByVal app As ISldWorks, ByVal modelDoc As IModelDoc2, ByVal feat As IFeature) Implements IMacroFeatureHandler.Init

		m_App = app
		Model = modelDoc
		Feature = feat

		If TypeOf Model Is PartDoc Then
			AddHandler CType(Model, PartDoc).RegenNotify, AddressOf OnPreRegeneration
		Else
			Throw New NotSupportedException()
		End If

	End Sub

	Private Function OnPreRegeneration() As Integer
		Dim featData = TryCast(Feature.GetDefinition(), IMacroFeatureData)
		Dim parameters = featData.GetParameters(Of LinkFileMacroFeatureParameters)(Feature, Model)
		UpdateCachedBodyIfNeeded(parameters)
		Return 0
	End Function

	Private Sub UpdateCachedBodyIfNeeded(ByVal parameters As LinkFileMacroFeatureParameters)

		LastError = Nothing

		Dim refDoc As IModelDoc2 = Nothing
		Dim isRefDocLoaded As Boolean = False

		Try

			If File.Exists(parameters.LinkedFilePath) Then

				LastUpdateStamp = File.GetLastWriteTimeUtc(parameters.LinkedFilePath).Ticks
				refDoc = TryCast(m_App.GetOpenDocumentByName(parameters.LinkedFilePath), IModelDoc2)
				isRefDocLoaded = refDoc IsNot Nothing

				If LastUpdateStamp <> parameters.FileLastUpdateTimeStamp OrElse (isRefDocLoaded AndAlso refDoc.GetSaveFlag()) OrElse CachedBodies Is Nothing Then

					If Not isRefDocLoaded Then

						m_App.DocumentVisible(False, CInt(swDocumentTypes_e.swDocPART))

						Dim docSpec = TryCast(m_App.GetOpenDocSpec(parameters.LinkedFilePath), IDocumentSpecification)

						docSpec.Silent = True
						docSpec.ReadOnly = True
						refDoc = m_App.OpenDoc7(docSpec)

						If refDoc Is Nothing Then
							Throw New InvalidOperationException($"Failed to load the referenced file ${docSpec.FileName} with error: {CType(docSpec.[Error], swFileLoadError_e)}")
						End If

					End If

					If TypeOf refDoc Is IPartDoc Then

						Dim bodies = TryCast((TryCast(refDoc, IPartDoc)).GetBodies2(CInt(swBodyType_e.swAllBodies), True), Object())

						If bodies IsNot Nothing And bodies.Any() Then
							Dim resBodies = bodies.Cast(Of IBody2)().[Select](Function(b) b.ICopy()).ToArray()
							CachedBodies = resBodies
						Else
							Throw New InvalidOperationException("No bodies in the referenced document")
						End If

					Else
						Throw New InvalidOperationException("Referenced document is not a part")
					End If

				End If
			Else
				Throw New FileNotFoundException($"Linked file '${parameters.LinkedFilePath}' is not found")
			End If

		Catch ex As Exception
			LastError = ex
		Finally
			m_App.DocumentVisible(True, CInt(swDocumentTypes_e.swDocPART))

			If Not isRefDocLoaded AndAlso refDoc IsNot Nothing Then
				m_App.CloseDoc(refDoc.GetTitle())
			End If
		End Try
	End Sub

	Public Sub Unload(ByVal reason As MacroFeatureUnloadReason_e) Implements IMacroFeatureHandler.Unload
		If TypeOf Model Is PartDoc Then
			RemoveHandler CType(Model, PartDoc).RegenNotify, AddressOf OnPreRegeneration
		End If
	End Sub

End Class