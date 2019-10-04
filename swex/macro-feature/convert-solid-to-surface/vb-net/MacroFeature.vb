Imports CodeStack.SwEx.Common.Attributes
Imports CodeStack.SwEx.MacroFeature.Attributes
Imports CodeStack.SwEx.MacroFeature.Base
Imports CodeStack.SwEx.MacroFeature.Examples.ConvertSolidToSurface.My.Resources
Imports SolidWorks.Interop.sldworks

<Icon(GetType(Resources), NameOf(Resources.solid_to_surface))>
<Title(GetType(Resources), NameOf(Resources.ConvertSolidToSurfaceFeatureName))>
Public Class MacroFeature
	Inherits MacroFeatureEx(Of DataModel)

	Dim m_Page As PropertyPage

	Protected Overrides Function OnEditDefinition(app As ISldWorks, model As IModelDoc2, feature As IFeature) As Boolean

		Dim featData As IMacroFeatureData = feature.GetDefinition
		featData.AccessSelections(model, Nothing)

		Dim data As DataModel = GetParameters(feature, featData, model)

		model.ClearSelection2(True)

		m_Page = New PropertyPage(app, model, feature, featData, data)
		AddHandler m_Page.PageClosed, AddressOf OnPageClosed
		m_Page.Show()
		Return True

	End Function

	Protected Overrides Function OnRebuild(app As ISldWorks, model As IModelDoc2, feature As IFeature, parameters As DataModel) As MacroFeatureRebuildResult

		Dim resBodies(parameters.Bodies.Count - 1) As IBody2

		Dim featData As IMacroFeatureData = feature.GetDefinition()

		For i As Integer = 0 To parameters.Bodies.Count - 1
			Dim faces() As IFace2 = CType(parameters.Bodies(i).GetFaces(), Object()).Cast(Of IFace2).ToArray()

			Dim modeler As IModeler = app.GetModeler()

			resBodies(i) = modeler.CreateSheetFromFaces(faces)
		Next

		Return MacroFeatureRebuildBodyResult.FromBodies(resBodies, featData, True)

	End Function

	Private Sub OnPageClosed(model As IModelDoc2, feat As IFeature, featData As IMacroFeatureData, data As DataModel, isOk As Boolean)
		If isOk Then
			SetParameters(model, feat, featData, data)
			feat.ModifyDefinition(featData, model, Nothing)
		Else
			featData.ReleaseSelectionAccess()
		End If
	End Sub

End Class
