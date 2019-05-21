using CodeStack.SwEx.AddIn.Attributes;
using CodeStack.SwEx.AddIn.Base;
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
        private IDocumentsHandler<IssuesDocument> m_DocHandler;
        private IssuesControl m_IssuesControl;

        public override bool OnConnect()
        {
            m_DocHandler = CreateDocumentsHandler<IssuesDocument>();

            m_DocHandler.HandlerCreated += OnDocHandlerCreated;

            IssuesControlHost ctrlHost;
            CreateTaskPane(out ctrlHost);

            m_IssuesControl = ctrlHost.IssuesControl;
            return true;
        }

        private void OnDocHandlerCreated(IssuesDocument issuesDoc)
        {
            issuesDoc.ShowIssues += OnShowIssues;
        }

        private void OnShowIssues(IssuesVM issues)
        {
            m_IssuesControl.DataContext = issues;
        }
    }
}
