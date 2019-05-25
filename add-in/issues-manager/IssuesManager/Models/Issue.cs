//**********************
//Examples for SwEx Framework
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestack-net-dev/swex-examples/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex/add-in/
//**********************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CodeStack.SwEx.AddIn.Examples.IssuesManager.Models
{
    [DataContract]
    public class Issue : IssueInfo
    {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        public Issue(int id)
        {
            Id = id;
        }

        public Issue(IssueInfo info) : this(info.Id)
        {
            Summary = info.Summary;
            Severity = info.Severity;
            Status = info.Status;
        }

        public IssueInfo GetInfo()
        {
            return new IssueInfo()
            {
                Id = Id,
                Summary = Summary,
                Severity = Severity,
                Status = Status
            };
        }
    }
}
