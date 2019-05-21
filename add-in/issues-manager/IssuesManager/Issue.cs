using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeStack.SwEx.AddIn.Examples.IssuesManager
{
    public enum Severity_e
    {
        Low,
        Medium,
        High
    }

    public class Issue
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public Severity_e Severity { get; set; }
    }
}
