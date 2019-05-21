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

namespace CodeStack.SwEx.AddIn.Examples.IssuesManager
{
    [ComVisible(false)]
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
