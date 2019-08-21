'**********************
'Render Tetrahedron And Handle View Display Modes
'Copyright(C) 2019 www.codestack.net
'License: https://github.com/codestackdev/solidworks-api-examples/blob/master/LICENSE
'Product URL: https://www.codestack.net/solidworks-api/adornment/opengl/display-mode-tetrahedron/
'**********************

Imports CodeStack.SwEx.AddIn
Imports CodeStack.SwEx.AddIn.Attributes
Imports System.Runtime.InteropServices

<ComVisible(True), Guid("A28F5BB7-E468-48B6-9BBD-9E7A31FF8CC8")>
<AutoRegister("OpenGL Box Tetrahedron")>
Public Class AddIn
	Inherits SwAddInEx

	Public Overrides Function OnConnect() As Boolean
		CreateDocumentsHandler(Of OpenGlDocumentHandler)()
		Return True
	End Function
End Class