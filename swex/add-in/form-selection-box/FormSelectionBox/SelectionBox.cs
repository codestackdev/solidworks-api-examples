//**********************
//Selection Box Control In Windows Forms
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/solidworks-api-examples/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex/add-in/
//**********************

using CodeStack.SwEx.AddIn.Core;
using CodeStack.SwEx.AddIn.Enums;
using SolidWorks.Interop.swconst;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CodeStack
{
    public class SelectionBox : ListBox
    {
        public SelectionBox()
        {
            BackColor = System.Drawing.Color.FromArgb(207, 228, 247);
            SelectionMode = SelectionMode.One;
        }

        private DocumentHandler m_Context;

        public DocumentHandler Context
        {
            set
            {
                m_Context = value;
                m_Context.Selection += OnSelection;
            }
        }

        public swSelectType_e[] Filter { get; set; }

        public int Mark { get; set; } = -1;

        private object m_Selection;

        public object Selection
        {
            get
            {
                return m_Selection;
            }
            set
            {
                if (value == null && m_Selection != null)
                {
                    var ind = FindObjectSelectionIndex(m_Selection);
                    m_Context.Model.ISelectionManager.DeSelect2(ind, Mark);
                }

                m_Selection = value;

                if (m_Selection != null)
                {
                    if (FindObjectSelectionIndex(m_Selection) != -1
                        || m_Context.Model.Extension.MultiSelect2(new DispatchWrapper[] { new DispatchWrapper(value) }, true, null) == 1)
                    {
                        var ind = FindObjectSelectionIndex(m_Selection);
                        var selMgr = m_Context.Model.ISelectionManager;
                        selMgr.SetSelectedObjectMark(ind, Mark, (int)swSelectionMarkAction_e.swSelectionMarkSet);
                    }
                }

                Items.Clear();

                if (m_Selection != null)
                {
                    Items.Add($"{GetSelectedObjectBaseName(m_Selection)}<1>");
                }
            }
        }

        private bool OnSelection(DocumentHandler docHandler, swSelectType_e selType, SelectionState_e state)
        {
            switch (state)
            {
                case SelectionState_e.UserPreSelect:
                    if (!Filter?.Contains(selType) == true)
                    {
                        return false;
                    }
                    break;
                case SelectionState_e.UserPostSelect:
                    var sel = FindLastObjectByMark(Mark);
                    if (sel == Selection)
                    {
                        sel = null;
                    }
                    Selection = sel;
                    break;
                case SelectionState_e.ClearSelection:
                    Selection = null;
                    break;
            }

            return true;
        }

        private int FindObjectSelectionIndex(object obj)
        {
            var selMgr = m_Context.Model.ISelectionManager;

            for (int i = 1; i < selMgr.GetSelectedObjectCount2(-1) + 1; i++)
            {
                if (selMgr.GetSelectedObject6(i, -1) == obj)
                {
                    return i;
                }
            }

            return -1;
        }

        private object FindLastObjectByMark(int mark)
        {
            var selMgr = m_Context.Model.ISelectionManager;

            for (int i = selMgr.GetSelectedObjectCount2(-1); i > 0 ; i--)
            {
                if (mark == -1 || selMgr.GetSelectedObjectMark(i) == mark)
                {
                    return selMgr.GetSelectedObject6(i, -1);
                }
            }

            return null;
        }

        private string GetSelectedObjectBaseName(object obj)
        {
            string selId;
            string selName;
            int selType;
            m_Context.Model.ISelectionManager.GetSelectByIdSpecification(obj, out selId, out selName, out selType);
            
            return selName;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            m_Context.Selection -= OnSelection;
        }
    }
}
