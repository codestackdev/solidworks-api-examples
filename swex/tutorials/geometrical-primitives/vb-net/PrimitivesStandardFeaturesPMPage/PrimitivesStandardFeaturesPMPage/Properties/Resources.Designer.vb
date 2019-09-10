Imports System

Namespace CodeStack.PrimitivesStandardFeaturesPMPage.Properties
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")>
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>
    Friend Class Resources
        Private Shared resourceMan As Global.System.Resources.ResourceManager
        Private Shared resourceCulture As Global.System.Globalization.CultureInfo

        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>
        Friend Sub New()
        End Sub

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>
        Friend Shared ReadOnly Property ResourceManager As Global.System.Resources.ResourceManager
            Get

                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("CodeStack.PrimitivesStandardFeaturesPMPage.Properties.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If

                Return resourceMan
            End Get
        End Property

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>
        Friend Shared Property Culture As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set(ByVal value As Global.System.Globalization.CultureInfo)
                resourceCulture = value
            End Set
        End Property

        Friend Shared ReadOnly Property box_icon As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("box_icon", resourceCulture)
                Return (CType((obj), System.Drawing.Bitmap))
            End Get
        End Property

        Friend Shared ReadOnly Property CommandTitleCreateBox As String
            Get
                Return ResourceManager.GetString("CommandTitleCreateBox", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property CommandTitleCreateCylinder As String
            Get
                Return ResourceManager.GetString("CommandTitleCreateCylinder", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property cylinder_icon As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("cylinder_icon", resourceCulture)
                Return (CType((obj), System.Drawing.Bitmap))
            End Get
        End Property

        Friend Shared ReadOnly Property height_icon As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("height-icon", resourceCulture)
                Return (CType((obj), System.Drawing.Bitmap))
            End Get
        End Property

        Friend Shared ReadOnly Property primitives_icon As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("primitives_icon", resourceCulture)
                Return (CType((obj), System.Drawing.Bitmap))
            End Get
        End Property
    End Class
End Namespace
