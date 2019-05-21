using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodeStack.SwEx.AddIn.Examples.IssuesManager
{
    public class IssuesVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event Func<int, Issue> LoadIssue;

        private ICommand m_CreateNewIssueCommand;
        private IssueVM m_ActiveIssue;

        private readonly List<IssueVM> m_EditedIssues;
        
        public IssuesVM(IEnumerable<int> issues)
        {
            if (issues == null)
            {
                throw new ArgumentNullException(nameof(issues));
            }

            Issues = new ObservableCollection<int>(issues);

            m_EditedIssues = new List<IssueVM>();
        }

        public ObservableCollection<int> Issues { get; private set; }
        
        public int SelectedIssueId
        {
            get
            {
                if (ActiveIssue != null)
                {
                    return ActiveIssue.Issue.Id;
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                if (value != -1)
                {
                    var issueVm = m_EditedIssues.FirstOrDefault(i => i.Issue.Id == value);

                    if (issueVm == null)
                    {
                        var issue = LoadIssue?.Invoke(value);

                        if (issue == null)
                        {
                            throw new NullReferenceException($"Failed to find the issue by id {value}");
                        }

                        issueVm = new IssueVM(issue);
                        m_EditedIssues.Add(issueVm);
                    }

                    ActiveIssue = issueVm;

                    NotifyChanged();
                }
            }
        }
        
        public IssueVM ActiveIssue
        {
            get
            {
                return m_ActiveIssue;
            }
            set
            {
                m_ActiveIssue = value;
                NotifyChanged();
            }
        }

        public IEnumerable<Issue> EditedIssues
        {
            get
            {
                return m_EditedIssues.Select(i => i.Issue);
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
                        var issueId = Issues.Any() ? Issues.Max() + 1 : 1;
                        var newIssue = new IssueVM(issueId);
                        Issues.Add(issueId);
                        m_EditedIssues.Add(newIssue);
                        SelectedIssueId = issueId;
                    });
                }

                return m_CreateNewIssueCommand;
            }
        }

        private void NotifyChanged([CallerMemberName]string prpName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prpName));
        }
    }
}
