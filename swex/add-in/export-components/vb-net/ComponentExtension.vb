Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst
Imports System.Runtime.CompilerServices

Module ComponentExtension

    <Extension()>
    Sub Export(ByVal comp As IComponent2, ByVal filePath As String)

        Dim model = comp.IGetModelDoc()

        If model IsNot Nothing Then
            Dim err As Integer = -1
            Dim warn As Integer = -1
            model.Extension.SaveAs(filePath, CInt(swSaveAsVersion_e.swSaveAsCurrentVersion),
                                       CInt(swSaveAsOptions_e.swSaveAsOptions_Silent), Nothing, err, warn)
        Else
            Throw New NullReferenceException("Model")
        End If

    End Sub

End Module
