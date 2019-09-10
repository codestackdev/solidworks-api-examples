'**********************
'SwEx - development tools for SOLIDWORKS
'Copyright(C) 2019 www.codestack.net
'License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
'Product URL: https://www.codestack.net/labs/solidworks/swex
'**********************

Imports CodeStack.SwEx.PMPage.Base
Imports CodeStack.SwEx.PMPage.Controls
Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst

Public Class ReferenceSelectionCustomFilter
    Inherits SelectionCustomFilter(Of IEntity)

    Protected Overrides Function Filter(ByVal selBox As IPropertyManagerPageControlEx, ByVal selection As IEntity, ByVal selType As swSelectType_e, ByRef itemText As String) As Boolean
        If selType = swSelectType_e.swSelFACES Then

            If selType = swSelectType_e.swSelFACES Then
                Dim face = TryCast(selection, IFace2)

                If Not face.IGetSurface().IsPlane() Then
                    Return False
                End If
            End If
        End If

        Return True
    End Function

End Class
