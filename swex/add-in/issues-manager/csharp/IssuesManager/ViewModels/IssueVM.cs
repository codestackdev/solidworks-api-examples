//**********************
//Examples for SwEx Framework
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestack-net-dev/swex-examples/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex/add-in/
//**********************

using CodeStack.SwEx.AddIn.Examples.IssuesManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CodeStack.SwEx.AddIn.Examples.IssuesManager.ViewModels
{
    public class IssueVM : INotifyPropertyChanged
    {
        public event Action<IssueVM> Modified;

        public event PropertyChangedEventHandler PropertyChanged;
        
        private Issue m_Issue;
        private bool m_IsDirty;
        private bool m_IsDeleted;
        
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
                if (m_Issue.Summary != value)
                {
                    m_Issue.Summary = value;
                    this.NotifyChanged();
                    IsDirty = true;
                }
            }
        }

        public string Description
        {
            get
            {
                return m_Issue.Description;
            }
            set
            {
                if (m_Issue.Description != value)
                {
                    m_Issue.Description = value;
                    this.NotifyChanged();
                    IsDirty = true;
                }
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
                if (m_Issue.Severity != value)
                {
                    m_Issue.Severity = value;
                    this.NotifyChanged();
                    IsDirty = true;
                }
            }
        }

        public Status_e Status
        {
            get
            {
                return m_Issue.Status;
            }
            set
            {
                if (m_Issue.Status != value)
                {
                    m_Issue.Status = value;
                    this.NotifyChanged();
                    IsDirty = true;
                }
            }
        }

        public bool IsDirty
        {
            get
            {
                return m_IsDirty;
            }
            set
            {
                m_IsDirty = value;
                this.NotifyChanged();
                if (value)
                {
                    Modified?.Invoke(this);
                }
            }
        }
        
        public bool IsDeleted
        {
            get
            {
                return m_IsDeleted;
            }
            set
            {
                m_IsDeleted = value;
                this.NotifyChanged();
            }
        }
        
        public bool IsLoaded { get; set; }
        
        internal Issue Issue
        {
            get
            {
                return m_Issue;
            }
            set
            {
                m_Issue = value;
            }
        }

        public int Id
        {
            get
            {
                return Issue.Id;
            }
        }

        private void NotifyChanged([CallerMemberName]string prpName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prpName));
        }
    }
}
