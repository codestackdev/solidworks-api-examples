﻿//**********************
//Examples for SwEx Framework
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestack-net-dev/swex-examples/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex/add-in/
//**********************

using CodeStack.SwEx.AddIn.Examples.IssuesManager.Commands;
using CodeStack.SwEx.AddIn.Examples.IssuesManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodeStack.SwEx.AddIn.Examples.IssuesManager.ViewModels
{
    public class IssuesVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event Func<int, Issue> LoadIssue;
        public event Action Modified;

        private ICommand m_CreateNewIssueCommand;
        private IssueVM m_ActiveIssue;

        public IssuesVM(IssueInfo[] issueInfos)
        {
            if (issueInfos == null)
            {
                throw new ArgumentNullException(nameof(issueInfos));
            }

            Issues = new ObservableCollection<IssueVM>(
                issueInfos.Select(i =>
                {
                    var issueVm = CreateIssueVm(new Issue(i));

                    issueVm.IsLoaded = false;
                    return issueVm;
                }));
        }

        public ObservableCollection<IssueVM> Issues { get; private set; }

        private IssueVM CreateIssueVm(Issue issue)
        {
            var issueVm = new IssueVM(issue);
            issueVm.Modified += OnIssueModified;
            return issueVm;
        }

        private void OnIssueModified(IssueVM issueVm)
        {
            Modified?.Invoke();
        }

        public void RemoveActiveIssue()
        {
            if (ActiveIssue != null)
            {
                ActiveIssue.IsDeleted = true;
            }
        }

        public void FlushChanges()
        {
            for (int i = Issues.Count - 1; i >= 0; i--)
            {
                if (Issues[i].IsDeleted)
                {
                    Issues.RemoveAt(i);
                }
                else if (Issues[i].IsDirty)
                {
                    Issues[i].IsDirty = false;
                }
            }
        }

        public IssueVM ActiveIssue
        {
            get
            {
                if (m_ActiveIssue != null)
                {
                    if (!m_ActiveIssue.IsLoaded)
                    {
                        var issue = LoadIssue?.Invoke(m_ActiveIssue.Id);
                        m_ActiveIssue.Issue = issue;
                        m_ActiveIssue.IsLoaded = true;
                    }
                }

                return m_ActiveIssue;
            }
            set
            {
                m_ActiveIssue = value;

                NotifyChanged();
            }
        }
        
        public ICommand CreateNewIssueCommand
        {
            get
            {
                if (m_CreateNewIssueCommand == null)
                {
                    m_CreateNewIssueCommand = new RelayCommand(x =>
                    {
                        CreateNewIssue();
                    });
                }

                return m_CreateNewIssueCommand;
            }
        }

        public void CreateNewIssue()
        {
            var issueId = Issues.Any() ? Issues.Max(i => i.Id) + 1 : 1;
            var newIssue = new Issue(issueId)
            {
                Author = Environment.UserName,
                DateCreated = DateTime.Now,
            };

            var issueVm = CreateIssueVm(newIssue);
            issueVm.IsLoaded = true;
            issueVm.IsDirty = true;

            Issues.Add(issueVm);
            ActiveIssue = issueVm;
        }

        private void NotifyChanged([CallerMemberName]string prpName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prpName));
        }
    }
}
