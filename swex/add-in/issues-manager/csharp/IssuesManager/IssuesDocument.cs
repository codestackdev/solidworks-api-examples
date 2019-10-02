//**********************
//Examples for SwEx Framework
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestack-net-dev/swex-examples/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex/add-in/
//**********************

using CodeStack.SwEx.AddIn.Core;
using CodeStack.SwEx.AddIn.Enums;
using CodeStack.SwEx.AddIn.Examples.IssuesManager.Models;
using CodeStack.SwEx.AddIn.Examples.IssuesManager.ViewModels;
using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CodeStack.SwEx.AddIn.Examples.IssuesManager
{
    public class IssuesDocument : DocumentHandler
    {
        private const string STORAGE_NAME = "_CodeStackIssuesStore_";
        private const string ISSUES_SUB_STORAGE_NAME = "IssuesStore";
        private const string ISSUES_SUMMARIES_STREAM_NAME = "Summaries";

        public event Action<IssuesVM> ShowIssues;

        private IssuesVM m_IssuesVm;

        public override void OnInit()
        {
            this.Access3rdPartyData += OnAccess3rdPartyData;
        }

        private void OnAccess3rdPartyData(DocumentHandler docHandler, Enums.Access3rdPartyDataState_e state)
        {
            switch (state)
            {
                case Access3rdPartyDataState_e.StorageRead:
                    LoadIssuesFromStorageStore();
                    break;

                case Access3rdPartyDataState_e.StorageWrite:
                    SaveIssuesToStorageStore();
                    break;
            }
        }

        public override void OnActivate()
        {
            ShowIssues?.Invoke(m_IssuesVm);
        }

        public void CreateNewIssue()
        {
            m_IssuesVm.CreateNewIssue();
        }

        public void RemoveActiveIssue()
        {
            m_IssuesVm.RemoveActiveIssue();
        }

        private void LoadIssuesFromStorageStore()
        {
            IEnumerable<int> issuesIds = null;
            IssueInfo[] issueInfos = null;

            using (var storageHandler = Model.Access3rdPartyStorageStore(STORAGE_NAME, false))
            {
                if (storageHandler.Storage != null)
                {
                    using (var storage = storageHandler.Storage)
                    {
                        using (var issuesStore = storage.TryOpenStorage(ISSUES_SUB_STORAGE_NAME, false))
                        {
                            if (issuesStore != null)
                            {
                                issuesIds = issuesStore.GetSubStreamNames().Select(n => int.Parse(n));
                            }
                        }

                        using (var stream = storage.TryOpenStream(ISSUES_SUMMARIES_STREAM_NAME, false))
                        {
                            if (stream != null)
                            {
                                var ser = new DataContractSerializer(typeof(IssueInfo[]));
                                issueInfos = ser.ReadObject(stream) as IssueInfo[];
                            }
                        }
                    }
                }
            }

            if (issuesIds == null)
            {
                issuesIds = Enumerable.Empty<int>();
            }

            if (issueInfos == null)
            {
                issueInfos = new IssueInfo[0];
            }

            if (!issueInfos.Select(i => i.Id).OrderBy(i => i)
                .SequenceEqual(issuesIds.OrderBy(i => i)))
            {
                throw new InvalidOperationException("Issues mismatch");
            }

            m_IssuesVm = new IssuesVM(issueInfos);
            m_IssuesVm.Modified += OnIssuesModified;
            m_IssuesVm.LoadIssue += OnLoadIssue;

            if (Model.Visible)
            {
                ShowIssues?.Invoke(m_IssuesVm);
            }
        }

        private void OnIssuesModified()
        {
            Model.SetSaveFlag();
        }

        private Issue OnLoadIssue(int issueId)
        {
            using (var storageHandler = Model.Access3rdPartyStorageStore(STORAGE_NAME, false))
            {
                using (var storage = storageHandler.Storage)
                {
                    using (var issueStorage = storage.TryOpenStorage(ISSUES_SUB_STORAGE_NAME, false))
                    {
                        using (var stream = issueStorage.TryOpenStream(issueId.ToString(), false))
                        {
                            var ser = new DataContractSerializer(typeof(Issue));
                            return ser.ReadObject(stream) as Issue;
                        }
                    }
                }
            }
        }

        private void SaveIssuesToStorageStore()
        {
            var loadedIssues = m_IssuesVm.Issues.Where(i => i.IsLoaded);

            if (loadedIssues.Any(i => i.IsDeleted || i.IsDirty))
            {
                using (var storageHandler = Model.Access3rdPartyStorageStore(STORAGE_NAME, true))
                {
                    using (var storage = storageHandler.Storage)
                    {
                        using (var stream = storage.TryOpenStream(ISSUES_SUMMARIES_STREAM_NAME, true))
                        {
                            var ser = new DataContractSerializer(typeof(IssueInfo[]));
                            ser.WriteObject(stream, m_IssuesVm.Issues
                                .Select(i => i.Issue.GetInfo()).ToArray());
                        }

                        using (var issuesStore = storage.TryOpenStorage(ISSUES_SUB_STORAGE_NAME, true))
                        {
                            foreach (var removedIssue in loadedIssues.Where(i => i.IsDeleted))
                            {
                                //TODO: simplify when issue #23 is implemented
                                issuesStore.Storage.DestroyElement(removedIssue.Id.ToString());                                
                            }

                            foreach (var modifiedIssue in loadedIssues.Where(i => !i.IsDeleted && i.IsDirty))
                            {
                                using (var stream = issuesStore.TryOpenStream(modifiedIssue.Id.ToString(), true))
                                {
                                    var ser = new DataContractSerializer(typeof(Issue));
                                    ser.WriteObject(stream, modifiedIssue.Issue);
                                }
                            }
                        }
                    }
                }
            }

            m_IssuesVm.FlushChanges();
        }

        public override void OnDestroy()
        {
            this.Access3rdPartyData -= OnAccess3rdPartyData;
        }
    }
}
