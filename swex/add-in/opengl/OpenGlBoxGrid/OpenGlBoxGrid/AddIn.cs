//**********************
//Render Box Grid With Transparency
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/solidworks-api-examples/blob/master/LICENSE
//Product URL: https://www.codestack.net/solidworks-api/adornment/opengl/box-grid-transparency/
//**********************

using CodeStack.SwEx.AddIn;
using CodeStack.SwEx.AddIn.Attributes;
using System;
using System.Runtime.InteropServices;

namespace CodeStack.OpenGlBoxGrid
{
    [ComVisible(true), Guid("FAB0F03B-785E-4113-B120-E18E7C73B9EB")]
    [AutoRegister("OpenGL Box Grid")]
    public class AddIn : SwAddInEx
    {
        public override bool OnConnect()
        {
            CreateDocumentsHandler<OpenGlDocumentHandler>();
            return true;
        }
    }
}
