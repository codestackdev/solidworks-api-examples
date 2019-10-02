//**********************
//Examples for SwEx Framework
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestack-net-dev/swex-examples/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex/add-in/
//**********************

using CodeStack.SwEx.AddIn.Attributes;
using CodeStack.SwEx.AddIn.Base;
using CodeStack.SwEx.AddIn.Core;
using CodeStack.SwEx.AddIn.Examples.IssuesManager.Properties;
using CodeStack.SwEx.AddIn.Examples.IssuesManager.ViewModels;
using CodeStack.SwEx.AddIn.Examples.IssuesManager.Views;
using CodeStack.SwEx.Common.Attributes;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CodeStack.SwEx.AddIn.Examples.IssuesManager
{
    [AutoRegister("Issues Manager")]
    [Guid("160F5F50-CB41-4604-8DED-8E4989E6B572")]
    [ComVisible(true)]
    public class AddIn : SwAddInEx
    {
        [TaskPaneIcon(typeof(Resources), nameof(Resources.issues_icon))]
        [Title("Issues Manager")]
        private enum IssuesMgrCommands_e
        {
            [Icon(typeof(Resources), nameof(Resources.new_issue_icon))]
            [Title("Create New Issue")]
            CreateNewIssue,

            [TaskPaneStandardButton(swTaskPaneBitmapsOptions_e.swTaskPaneBitmapsOptions_Close)]
            [Title("Remove Selected Issue")]
            RemoveIssue
        }

        private IDocumentsHandler<IssuesDocument> m_DocHandler;
        private IssuesControl m_IssuesControl;

        public override bool OnConnect()
        {
            m_DocHandler = CreateDocumentsHandler<IssuesDocument>();

            m_DocHandler.HandlerCreated += OnDocHandlerCreated;

            IssuesControlHost ctrlHost;
            CreateTaskPane<IssuesControlHost, IssuesMgrCommands_e>(OnTaskPaneButtonClicked, out ctrlHost);

            m_IssuesControl = ctrlHost.IssuesControl;
            return true;
        }

        private void OnTaskPaneButtonClicked(IssuesMgrCommands_e cmd)
        {
            if (App.IActiveDoc2 != null)
            {
                switch (cmd)
                {
                    case IssuesMgrCommands_e.CreateNewIssue:
                        m_DocHandler[App.IActiveDoc2].CreateNewIssue();
                        break;

                    case IssuesMgrCommands_e.RemoveIssue:
                        m_DocHandler[App.IActiveDoc2].RemoveActiveIssue();
                        break;
                }
            }
            else
            {
                App.SendMsgToUser2("Open the model",
                    (int)swMessageBoxIcon_e.swMbStop, (int)swMessageBoxBtn_e.swMbOk);
            }
        }

        private void OnDocHandlerCreated(IssuesDocument issuesDoc)
        {
            issuesDoc.Destroyed += OnIssuesDocDestroyed;
            issuesDoc.ShowIssues += OnShowIssues;
        }
        
        private void OnIssuesDocDestroyed(DocumentHandler docHandler)
        {
            //destroying last document
            if (App.IFrameObject().GetModelWindowCount() == 1)
            {
                m_IssuesControl.DataContext = null;
            }
        }

        private void OnShowIssues(IssuesVM issues)
        {
            m_IssuesControl.DataContext = issues;
        }
    }
}
