//**********************
//Hosting eDrawings control in Windows Forms
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestack-net-dev/solidworks-api-examples/blob/master/LICENSE
//Product URL: https://www.codestack.net/edrawings-api/gettings-started/winforms/
//**********************

using System;
using System.Windows.Forms;
using eDrawings.Interop.EModelViewControl;

namespace CodeStack.Examples.eDrawingsApi
{
    public partial class EDrawingsUserControl : UserControl
    {
        public event Action<EModelViewControl> EDrawingsControlLoaded;

        public EDrawingsUserControl()
        {
            InitializeComponent();
        }

        public void LoadEDrawings()
        {
            var host = new EDrawingsHost();
            host.ControlLoaded += OnControlLoaded;
            this.Controls.Add(host);
            host.Dock = DockStyle.Fill;
        }
        
        private void OnControlLoaded(EModelViewControl ctrl)
        {
            EDrawingsControlLoaded?.Invoke(ctrl);
        }
    }
}
