using CodeStack.SwEx.AddIn;
using CodeStack.SwEx.AddIn.Attributes;
using System;
using System.Runtime.InteropServices;
using SolidWorks.Interop.sldworks;
using System.Windows.Forms;
using CodeStack.SwEx.Examples.LinkFeatureToExternalFile.Properties;
using CodeStack.SwEx.Common.Attributes;
using System.ComponentModel;

namespace CodeStack.SwEx.Examples.LinkFeatureToExternalFile
{
    [ComVisible(true), Guid("C5136EE6-EFD8-4F2C-AF5A-18B47FBA36EA")]
    [AutoRegister("Link To External File")]
    public class AddIn : SwAddInEx
    {
        [Title("Linked To File (C#)")]
        [Icon(typeof(Resources), nameof(Resources.linked_part_icon))]
        private enum Commands_e
        {
            [Icon(typeof(Resources), nameof(Resources.linked_part_icon))]
            [Title("Insert link to file")]
            [Description("Inserts bodies linked to external file")]
            InsertLinkToFile
        }

        public override bool OnConnect()
        {
            this.AddCommandGroup<Commands_e>(OnButtonClick);
            return true;
        }

        private void OnButtonClick(Commands_e cmd)
        {
            switch (cmd)
            {
                case Commands_e.InsertLinkToFile:
                    InsertLinkToFileMacroFeature();
                    break;
            }
        }

        private void InsertLinkToFileMacroFeature()
        {
            using (var openFileDlg = new OpenFileDialog())
            {
                openFileDlg.Filter = "SOLIDWORKS Part Files (*.sldprt)|*.sldprt|All Files (*.*)|*.*";
                openFileDlg.RestoreDirectory = true;

                if (openFileDlg.ShowDialog() == DialogResult.OK)
                {
                    var linkedFilePath = openFileDlg.FileName;

                    App.IActiveDoc2.FeatureManager.InsertComFeature<LinkFileMacroFeature, LinkFileMacroFeatureParameters>(
                        new LinkFileMacroFeatureParameters()
                        {
                            LinkedFilePath = linkedFilePath
                        });
                }
            }
        }
    }
}
