Imports CodeStack.SwEx.MacroFeature
Imports CodeStack.SwEx.MacroFeature.Attributes
Imports System.Runtime.InteropServices
Imports CodeStack.SwEx.MacroFeature.Base
Imports SolidWorks.Interop.sldworks
Imports CodeStack.SwEx.Common.Attributes
Imports CodeStack.SwEx.Examples.LinkFeatureToExternalFile.My.Resources

<ComVisible(True), Guid("11F925DE-805F-40F6-AF26-16C760234E82")>
<ProgId("CodeStack.LinkFileMacroFeature.VBNET")>
<Options("LinkFileMacroFeature")>
<Icon(GetType(Resources), NameOf(Resources.linked_part_icon))>
Public Class LinkFileMacroFeature
	Inherits MacroFeatureEx(Of LinkFileMacroFeatureParameters, LinkFileMacroFeatureHandler)

	Protected Overrides Function OnRebuild(ByVal handler As LinkFileMacroFeatureHandler, ByVal parameters As LinkFileMacroFeatureParameters) As MacroFeatureRebuildResult
		If handler.LastError Is Nothing Then
			parameters.FileLastUpdateTimeStamp = handler.LastUpdateStamp
			SetParameters(handler.Model, handler.Feature, TryCast(handler.Feature.GetDefinition(), IMacroFeatureData), parameters)
			Return MacroFeatureRebuildResult.FromBodies(handler.CachedBodies.Select(Function(b) b.ICopy()).ToArray(), TryCast(handler.Feature.GetDefinition(), IMacroFeatureData))
		Else
			Return MacroFeatureRebuildResult.FromStatus(False, handler.LastError.ToString())
		End If
	End Function

End Class
