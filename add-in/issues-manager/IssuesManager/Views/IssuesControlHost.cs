//**********************
//Examples for SwEx Framework
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestack-net-dev/swex-examples/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex/add-in/
//**********************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CodeStack.SwEx.AddIn.Examples.IssuesManager.Views
{
    [ComVisible(true)]
    public partial class IssuesControlHost : UserControl
    {
        public IssuesControlHost()
        {
            InitializeComponent();
        }

        internal IssuesControl IssuesControl
        {
            get
            {
                return ctrlIssues;
            }
        }
    }
}
