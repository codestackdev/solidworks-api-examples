﻿//**********************
//Hosting eDrawings control in Windows Forms
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestack-net-dev/solidworks-api-examples/blob/master/LICENSE
//Product URL: https://www.codestack.net/edrawings-api/gettings-started/winforms/
//**********************

using eDrawings.Interop.EModelViewControl;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CodeStack.Examples.eDrawingsApi
{
    public partial class MainForm : Form
    {
        private EModelViewControl m_EDrawingsCtrl;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            ctrlEDrw.LoadEDrawings();
        }

        private void OnControlLoaded(EModelViewControl ctrl)
        {
            m_EDrawingsCtrl = ctrl;

            m_EDrawingsCtrl.OnFinishedLoadingDocument += OnFinishedLoadingDocument;
            m_EDrawingsCtrl.OnFailedLoadingDocument += OnFailedLoadingDocument;
        }

        private void OnFailedLoadingDocument(string fileName, int errorCode, string errorString)
        {
            Trace.WriteLine($"{fileName} failed to loaded: {errorString}");
        }

        private void OnFinishedLoadingDocument(string fileName)
        {
            Trace.WriteLine($"{fileName} loaded");
        }

        private void OnOpen(object sender, EventArgs e)
        {
            var filePath = txtFilePath.Text;

            if (!string.IsNullOrEmpty(filePath))
            {
                if (m_EDrawingsCtrl == null)
                {
                    throw new NullReferenceException("eDrawings control is not loaded");
                }

                m_EDrawingsCtrl.CloseActiveDoc("");
                m_EDrawingsCtrl.OpenDoc(filePath, false, false, false, "");
            }
        }
    }
}
