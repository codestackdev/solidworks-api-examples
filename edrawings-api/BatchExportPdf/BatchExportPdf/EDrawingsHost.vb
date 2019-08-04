'**********************
'Batch export SOLIDWORKS files to PDF via eDrawings API (without SOLIDWORKS)
'Copyright(C) 2019 www.codestack.net
'License: https://github.com/codestack-net-dev/solidworks-api-examples/blob/master/LICENSE
'Product URL: https://www.codestack.net/edrawings-api/output/print-to-pdf/
'**********************

Imports System.Windows.Forms
Imports eDrawings.Interop.EModelViewControl

Public Class EDrawingsHost
    Inherits AxHost

    Public Event ControlLoaded As Action(Of EModelViewControl)
    Private m_IsLoaded As Boolean

    Public Sub New()
        MyBase.New("22945A69-1191-4DCF-9E6F-409BDE94D101")
        m_IsLoaded = False
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()

        If Not m_IsLoaded Then
            m_IsLoaded = True
            Dim ctrl = TryCast(Me.GetOcx(), EModelViewControl)
            RaiseEvent ControlLoaded(TryCast(Me.GetOcx(), EModelViewControl))
        End If
    End Sub

End Class
