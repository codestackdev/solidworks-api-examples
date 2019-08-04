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
    public enum Severity_e
    {
        [EnumMember]
        Low,

        [EnumMember]
        Medium,

        [EnumMember]
        High
    }
}
