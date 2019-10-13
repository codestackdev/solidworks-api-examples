//**********************
//Selection Box Control In Windows Forms
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/solidworks-api-examples/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex/add-in/
//**********************

using CodeStack.SwEx.AddIn;
using CodeStack.SwEx.AddIn.Attributes;
using CodeStack.SwEx.AddIn.Base;
using CodeStack.SwEx.AddIn.Core;
using CodeStack.SwEx.AddIn.Enums;
using CodeStack.SwEx.Common.Attributes;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeStack
{
    [AutoRegister]
    [ComVisible(true), Guid("5CDA8244-1EDB-4673-9974-E943B31F0ACF")]
    public class FormSelectionBoxAddIn : SwAddInEx
    {
        private class SwWindow : IWin32Window
        {
            private readonly IntPtr m_Handler;

            internal SwWindow(ISldWorks app)
            {
                m_Handler = new IntPtr(app.IFrameObject().GetHWnd());
            }

            public IntPtr Handle
            {
                get
                {
                    return m_Handler;
                }
            }
        }

        [Title("FormSelectionBox")]
        private enum Commands_e
        {
            [CommandItemInfo(swWorkspaceTypes_e.AllDocuments)]
            ShowForm
        }

        private SelectionForm m_Form;
        private IDocumentsHandler<DocumentHandler> m_DocsHandler;

        public override bool OnConnect()
        {
            m_DocsHandler = CreateDocumentsHandler();
            AddCommandGroup<Commands_e>(OnButtonClick);
            return true;
        }

        private void OnButtonClick(Commands_e cmd)
        {
            switch (cmd)
            {
                case Commands_e.ShowForm:
                    m_Form = new SelectionForm(m_DocsHandler[App.IActiveDoc2]);
                    m_Form.Show(new SwWindow(App));
                    break;
            }
        }
    }
}
