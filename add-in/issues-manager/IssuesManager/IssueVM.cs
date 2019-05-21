using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CodeStack.SwEx.AddIn.Examples.IssuesManager
{
    public class IssueVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private readonly Issue m_Issue;
        
        public IssueVM(int id) : this(
            new Issue()
            {
                Author = Environment.UserName,
                DateCreated = DateTime.Now,
                Id = id
            })
        {
        }

        public IssueVM(Issue issue)
        {
            m_Issue = issue;
        }

        public string Summary
        {
            get
            {
                return m_Issue.Summary;
            }
            set
            {
                m_Issue.Summary = value;
                this.NotifyChanged();
            }
        }

        public string Description
        {
            get
            {
                return m_Issue.Summary;
            }
            set
            {
                m_Issue.Summary = value;
                this.NotifyChanged();
            }
        }

        public string Author
        {
            get
            {
                return m_Issue.Author;
            }
        }

        public DateTime DateCreated
        {
            get
            {
                return m_Issue.DateCreated;
            }
        }

        public Severity_e Severity
        {
            get
            {
                return m_Issue.Severity;
            }
            set
            {
                m_Issue.Severity = value;
                this.NotifyChanged();
            }
        }

        internal Issue Issue
        {
            get
            {
                return m_Issue;
            }
        }

        private void NotifyChanged([CallerMemberName]string prpName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prpName));
        }
    }
}
