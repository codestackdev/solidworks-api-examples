using CodeStack.SwEx.AddIn.Attributes;
using CodeStack.SwEx.AddIn.Base;
using CodeStack.SwEx.AddIn.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeStack.SwEx.AddIn.Examples.DistanceHeatMap
{
    [AutoRegister("Distance Heat Map")]
    [Guid("4F856841-AEF5-43DF-B5E6-6AB978C0E3F6")]
    [ComVisible(true)]
    public class AddIn : SwAddInEx
    {
        private IDocumentsHandler<ColorizerDocument> m_DocHandler;

        public override bool OnConnect()
        {
            m_DocHandler = CreateDocumentsHandler<ColorizerDocument>();

            return true;
        }
    }
}
