//**********************
//Hosting SOLIDWORKS eDrawings control in Windows Presentation Foundation (WPF)
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestack-net-dev/solidworks-api-examples/blob/master/LICENSE
//Product URL: https://www.codestack.net/edrawings-api/gettings-started/wpf/
//**********************

using eDrawings.Interop.EModelViewControl;
using System;
using System.Windows.Forms;

namespace CodeStack.Examples.eDrawings
{
    public class EDrawingsHost : AxHost
    {
        public event Action<EModelViewControl> ControlLoaded;

        private bool m_IsLoaded;

        public EDrawingsHost() : base("22945A69-1191-4DCF-9E6F-409BDE94D101")
        {
            m_IsLoaded = false;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (!m_IsLoaded)
            {
                m_IsLoaded = true;
                var ctrl = this.GetOcx() as EModelViewControl;
                ControlLoaded?.Invoke(this.GetOcx() as EModelViewControl);
            }
        }
    }
}
