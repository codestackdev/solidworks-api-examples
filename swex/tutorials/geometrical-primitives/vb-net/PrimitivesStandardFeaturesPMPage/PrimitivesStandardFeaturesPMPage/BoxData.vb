'**********************
'SwEx - development tools for SOLIDWORKS
'Copyright(C) 2019 www.codestack.net
'License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
'Product URL: https://www.codestack.net/labs/solidworks/swex
'**********************

Imports CodeStack.SwEx.Common.Attributes
Imports CodeStack.SwEx.PMPage.Attributes
Imports SolidWorks.Interop.swconst
Imports System
Imports System.ComponentModel
Imports SolidWorks.Interop.sldworks
Imports CodeStack.PrimitivesStandardFeaturesPMPage.CodeStack.PrimitivesStandardFeaturesPMPage.Properties


<Title(GetType(Resources), NameOf(Resources.CommandTitleCreateBox))>
<Icon(GetType(Resources), NameOf(Resources.box_icon))>
<PageOptions(swPropertyManagerPageOptions_e.swPropertyManagerOptions_CancelButton Or swPropertyManagerPageOptions_e.swPropertyManagerOptions_OkayButton)>
Public Class BoxData

    <SelectionBox(GetType(ReferenceSelectionCustomFilter), swSelectType_e.swSelFACES, swSelectType_e.swSelDATUMPLANES)>
    <ControlAttribution(swControlBitmapLabelType_e.swBitmapLabel_SelectFace)>
    <Description("Base face")>
    <ControlOptions(,,,,,,, 12)>
    Public Property Reference As IEntity

    <ControlAttribution(swControlBitmapLabelType_e.swBitmapLabel_Width)>
    <Description("Distance in X direction")>
    <NumberBoxOptions(swNumberboxUnitType_e.swNumberBox_Length, 0, 1000, 0.01, False, 0.1, 0.001, swPropMgrPageNumberBoxStyle_e.swPropMgrPageNumberBoxStyle_Thumbwheel)>
    Public Property Width As Double

    <ControlAttribution(swControlBitmapLabelType_e.swBitmapLabel_LinearDistance)>
    <Description("Distance in Y direction")>
    <NumberBoxOptions(swNumberboxUnitType_e.swNumberBox_Length, 0, 1000, 0.01, False, 0.1, 0.001, swPropMgrPageNumberBoxStyle_e.swPropMgrPageNumberBoxStyle_Thumbwheel)>
    Public Property Length As Double

    <Description("Distance in Z direction")>
    <NumberBoxOptions(swNumberboxUnitType_e.swNumberBox_Length, 0, 1000, 0.01, False, 0.1, 0.001, swPropMgrPageNumberBoxStyle_e.swPropMgrPageNumberBoxStyle_Thumbwheel)>
    <Icon(GetType(Resources), NameOf(Resources.height_icon))>
    Public Property Height As Double

End Class

