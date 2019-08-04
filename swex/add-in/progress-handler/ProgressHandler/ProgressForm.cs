using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeStack.ProgressHandler
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        public void Init(string title, int upperBound)
        {
            lblMessage.Text = title;
            prgProgress.Maximum = upperBound;
        }

        public void SetTitle(string title)
        {
            lblMessage.Text = title;
        }

        public void SetProgress(int pos)
        {
            prgProgress.Value = pos;
        }
    }
}
