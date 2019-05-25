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
using System.Threading.Tasks;

namespace CodeStack.SwEx.AddIn.Examples.IssuesManager.Models
{
    [DataContract]
    public class IssueInfo
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Severity_e Severity { get; set; }

        [DataMember]
        public string Summary { get; set; }

        [DataMember]
        public Status_e Status { get; set; }
    }
}
