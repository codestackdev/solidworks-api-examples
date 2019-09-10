'**********************
'SwEx - development tools for SOLIDWORKS
'Copyright(C) 2019 www.codestack.net
'License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
'Product URL: https://www.codestack.net/labs/solidworks/swex
'**********************

Imports CodeStack.PrimitivesStandardFeaturesPMPage.CodeStack.PrimitivesStandardFeaturesPMPage.Properties
Imports CodeStack.SwEx.AddIn
Imports CodeStack.SwEx.AddIn.Attributes
Imports CodeStack.SwEx.AddIn.Enums
Imports CodeStack.SwEx.Common.Attributes
Imports CodeStack.SwEx.PMPage
Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst
Imports System
Imports System.ComponentModel
Imports System.Runtime.InteropServices

<ComVisible(True), Guid("AC4F9577-C0A7-42D8-9A1E-DAB88CBA2600")>
<AutoRegister("Geometry Primitives With PMPage", "Create geometrical primitives with Property Manager Page")>
Public Class AddIn
    Inherits SwAddInEx

    <Title("Primitives")>
    <Description("Creates geometrical primitives")>
    <Icon(GetType(Resources), NameOf(Resources.primitives_icon))>
    Private Enum Commands_e

        <Title(GetType(Resources), NameOf(Resources.CommandTitleCreateCylinder))>
        <Description("Creates extruded cylinder on selected face or plane")>
        <Icon(GetType(Resources), NameOf(Resources.cylinder_icon))>
        <CommandItemInfo(True, True, swWorkspaceTypes_e.Part, True)>
        CreateCylinder

        <Title(GetType(Resources), NameOf(Resources.CommandTitleCreateBox))>
        <Description("Creates extruded box on selected face or plane")>
        <Icon(GetType(Resources), NameOf(Resources.box_icon))>
        <CommandItemInfo(True, True, swWorkspaceTypes_e.Part, True)>
        CreateBox

    End Enum

    Private m_CurBoxData As BoxData
    Private m_CurCylData As CylinderData

    Private m_BoxPmPage As PropertyManagerPageEx(Of PropertyPageHandler, BoxData)
    Private m_CylPmPage As PropertyManagerPageEx(Of PropertyPageHandler, CylinderData)

    Public Overrides Function OnConnect() As Boolean

        AddCommandGroup(Of Commands_e)(AddressOf OnButtonClick)

        m_CurBoxData = New BoxData() With {
            .Height = 0.1,
            .Length = 0.2,
            .Width = 0.3
        }
        m_CurCylData = New CylinderData() With {
            .Diameter = 0.1,
            .Height = 0.2
        }

        m_BoxPmPage = New PropertyManagerPageEx(Of PropertyPageHandler, BoxData)(App)
        AddHandler m_BoxPmPage.Handler.Closing, AddressOf OnBoxPageClosing
        AddHandler m_BoxPmPage.Handler.Closed, AddressOf OnBoxPageClosed

        m_CylPmPage = New PropertyManagerPageEx(Of PropertyPageHandler, CylinderData)(App)
        AddHandler m_CylPmPage.Handler.Closing, AddressOf OnCylPageClosing
        AddHandler m_CylPmPage.Handler.Closed, AddressOf OnCylPageClosed

        Return True

    End Function

    Private Sub OnBoxPageClosing(ByVal reason As swPropertyManagerPageCloseReasons_e, ByVal arg As SwEx.PMPage.Base.ClosingArg)
        ValidateReference(m_CurBoxData.Reference, reason, arg)
    End Sub

    Private Sub OnCylPageClosing(ByVal reason As swPropertyManagerPageCloseReasons_e, ByVal arg As SwEx.PMPage.Base.ClosingArg)
        ValidateReference(m_CurCylData.Reference, reason, arg)
    End Sub

    Private Sub ValidateReference(ByVal reference As IEntity, ByVal reason As swPropertyManagerPageCloseReasons_e, ByVal arg As SwEx.PMPage.Base.ClosingArg)
        If reason = swPropertyManagerPageCloseReasons_e.swPropertyManagerPageClose_Okay Then

            If reference Is Nothing Then
                arg.ErrorMessage = "Select reference"
                arg.Cancel = True
            End If
        End If
    End Sub

    Private Sub OnBoxPageClosed(ByVal reason As swPropertyManagerPageCloseReasons_e)
        If reason = swPropertyManagerPageCloseReasons_e.swPropertyManagerPageClose_Okay Then
            App.IActiveDoc2.CreateBox(m_CurBoxData.Reference, m_CurBoxData.Width, m_CurBoxData.Length, m_CurBoxData.Height)
        End If
    End Sub

    Private Sub OnCylPageClosed(ByVal reason As swPropertyManagerPageCloseReasons_e)
        If reason = swPropertyManagerPageCloseReasons_e.swPropertyManagerPageClose_Okay Then
            App.IActiveDoc2.CreateCylinder(m_CurCylData.Reference, m_CurCylData.Diameter, m_CurCylData.Height)
        End If
    End Sub

    Private Sub OnButtonClick(ByVal cmd As Commands_e)
        Try

            Select Case cmd
                Case Commands_e.CreateBox
                    m_BoxPmPage.Show(m_CurBoxData)
                Case Commands_e.CreateCylinder
                    m_CylPmPage.Show(m_CurCylData)
            End Select

        Catch ex As Exception
            App.SendMsgToUser2(ex.Message, CInt(swMessageBoxIcon_e.swMbStop), CInt(swMessageBoxBtn_e.swMbOk))
        End Try
    End Sub

End Class

