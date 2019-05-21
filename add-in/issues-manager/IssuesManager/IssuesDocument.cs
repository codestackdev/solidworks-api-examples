using CodeStack.SwEx.AddIn.Core;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CodeStack.SwEx.AddIn.Examples.IssuesManager
{
    public class IssuesDocument : DocumentHandler
    {
        private const string STORAGE_NAME = "_CodeStackIssuesStore_";

        public event Action<IssuesVM> ShowIssues;

        private IssuesVM m_IssuesVm;

        public IssuesDocument()
        {
        }
        
        public override void OnActivate()
        {
            ShowIssues?.Invoke(m_IssuesVm);
        }

        public override void OnLoadFromStorageStore()
        {
            IEnumerable<int> issuesIds = null;

            using (var storageHandler = Model.Access3rdPartyStorageStore(STORAGE_NAME, false))
            {
                if (storageHandler.Storage != null)
                {
                    using (var storage = storageHandler.Storage)
                    {
                        issuesIds = storage.GetSubStreamNames().Select(n => int.Parse(n));
                    }
                }
            }

            if (issuesIds == null)
            {
                issuesIds = Enumerable.Empty<int>();
            }

            m_IssuesVm = new IssuesVM(issuesIds);

            m_IssuesVm.LoadIssue += OnLoadIssue;

            if (Model.Visible)
            {
                ShowIssues?.Invoke(m_IssuesVm);
            }
        }

        private Issue OnLoadIssue(int issueId)
        {
            Model.SetSaveFlag();

            using (var storageHandler = Model.Access3rdPartyStorageStore(STORAGE_NAME, false))
            {
                using (var storage = storageHandler.Storage)
                {
                    using (var stream = storage.TryOpenStream(issueId.ToString(), false))
                    {
                        var xmlSer = new XmlSerializer(typeof(Issue));
                        return xmlSer.Deserialize(stream) as Issue;
                    }
                }
            }
        }

        public override void OnSaveToStorageStore()
        {
            if (m_IssuesVm.EditedIssues.Any())
            {
                using (var storageHandler = Model.Access3rdPartyStorageStore(STORAGE_NAME, true))
                {
                    using (var storage = storageHandler.Storage)
                    {
                        foreach (var issue in m_IssuesVm.EditedIssues)
                        {
                            using (var stream = storage.TryOpenStream(issue.Id.ToString(), true))
                            {
                                var xmlSer = new XmlSerializer(typeof(Issue));
                                xmlSer.Serialize(stream, issue);
                            }
                        }
                    }
                }
            }
        }
    }
}
