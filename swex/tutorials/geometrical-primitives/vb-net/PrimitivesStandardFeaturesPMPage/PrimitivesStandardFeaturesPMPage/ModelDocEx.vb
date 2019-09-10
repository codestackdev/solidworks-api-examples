'**********************
'SwEx - development tools for SOLIDWORKS
'Copyright(C) 2019 www.codestack.net
'License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
'Product URL: https://www.codestack.net/labs/solidworks/swex
'**********************

Imports SolidWorks.Interop.swconst
Imports System
Imports System.Runtime.CompilerServices
Imports SolidWorks.Interop.sldworks

Public Module ModelDocEx

    <Extension()>
    Sub CreateBox(ByVal model As IModelDoc2, ByVal reference As IEntity, ByVal width As Double, ByVal length As Double, ByVal height As Double)
        CreateExtrudedPrimitive(model, reference, Function(skMgr) skMgr.CreateCenterRectangle(0, 0, 0, width / 2, height / 2, 0) IsNot Nothing, height)
    End Sub

    <Extension()>
    Sub CreateCylinder(ByVal model As IModelDoc2, ByVal reference As IEntity, ByVal diam As Double, ByVal height As Double)
        CreateExtrudedPrimitive(model, reference, Function(skMgr) skMgr.CreateCircleByRadius(0, 0, 0, diam / 2) IsNot Nothing, height)
    End Sub

    Private Sub CreateExtrudedPrimitive(ByVal model As IModelDoc2, ByVal reference As IEntity, ByVal creator As Func(Of ISketchManager, Boolean), ByVal height As Double)
        Try

            If Not reference.SelectByMark(False, 0) Then
                Throw New Exception("Failed to select reference")
            End If

            model.IActiveView.EnableGraphicsUpdate = False
            model.FeatureManager.EnableFeatureTree = False
            Dim sketch = CreateSketch(model, creator)
            ExtrudeSketch(model, sketch, height)
        Finally
            model.IActiveView.EnableGraphicsUpdate = True
            model.FeatureManager.EnableFeatureTree = True
        End Try
    End Sub

    Private Function CreateSketch(ByVal model As IModelDoc2, ByVal creator As Func(Of ISketchManager, Boolean)) As Sketch

        If model Is Nothing Then
            Throw New NullReferenceException("Model is null")
        End If

        Dim skMgr = model.SketchManager
        skMgr.InsertSketch(True)
        skMgr.AddToDB = True
        Dim sketch = skMgr.ActiveSketch

        If sketch Is Nothing Then
            Throw New NullReferenceException("Sketch is null")
        End If

        If Not creator(skMgr) Then
            Throw New NullReferenceException("Failed to create rectangle")
        End If

        skMgr.AddToDB = False
        skMgr.InsertSketch(True)

        Return sketch

    End Function

    Private Sub ExtrudeSketch(ByVal model As IModelDoc2, ByVal sketch As Sketch, ByVal height As Double)

        If (TryCast(sketch, IFeature)).Select2(False, 0) Then
            Dim feat = model.FeatureManager.FeatureExtrusion3(True, False, False, CInt(swEndConditions_e.swEndCondBlind), CInt(CInt(swEndConditions_e.swEndCondBlind)), height, 0, False, False, False, False, 0, 0, False, False, False, False, False, False, False, CInt(swStartConditions_e.swStartSketchPlane), 0, False)

            If feat Is Nothing Then
                Throw New NullReferenceException("Failed to create extrusion feature")
            End If
        Else
            Throw New Exception("Failed to select sketch")
        End If

    End Sub

End Module
