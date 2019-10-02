using CodeStack.SwEx.AddIn.Attributes;
using CodeStack.SwEx.AddIn.Enums;
using SolidWorks.Interop.sldworks;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace CodeStack.SwEx.AddIn.Examples.SwExportComponent
{
    [AutoRegister("Export Components (C#)", "Exports selected component to neutral format", true)]
    [Guid("1440137A-4A88-433F-92CB-4120BBBD1F8B")]
    [ComVisible(true)]
    public class SwExportComponentAddIn : SwAddInEx
    {
        public override bool OnConnect()
        {
            //TODO: init the resources
            //TODO: validate license

            AddCommandGroup<ExportCommands_e>(OnCommand, OnEnableCommand);

            //TODO: return false if error occurs (e.g. license is not validated)
            return true;
        }

        public override bool OnDisconnect()
        {
            //TODO: clean the resources
            //TODO: delete temp files
            return true;
        }

        private void OnCommand(ExportCommands_e cmd)
        {
            var ext = "";

            switch (cmd)
            {
                case ExportCommands_e.Parasolid:
                    ext = ".x_t";
                    break;
                case ExportCommands_e.Iges:
                    ext = ".igs";
                    break;
                case ExportCommands_e.Step:
                    ext = ".step";
                    break;
                default:
                    throw new NotSupportedException();
            }

            var comp = App.IActiveDoc2.ISelectionManager.GetSelectedObjectsComponent4(1, -1) as IComponent2;

            if (comp != null)
            {
                var filePath = Path.Combine(
                        Path.GetDirectoryName(App.IActiveDoc2.GetPathName()),
                        "Export",
                        Path.GetFileNameWithoutExtension(comp.GetPathName()) + ext);

                comp.Export(filePath);
            }
            else
            {
                Debug.Assert(false, "Command should be disabled");
                throw new NullReferenceException("Component");
            }
        }

        private void OnEnableCommand(ExportCommands_e cmd, ref CommandItemEnableState_e state)
        {
            //state is already calculated based on swWorkspaceTypes_e value specified for the
            //command (i.e. in this case if the active model is no an assembly the state of the button will be
            //DeselectDisable. So we only need to verify if the state is DeselectEnable
            if (state == CommandItemEnableState_e.DeselectEnable)
            {
                if (App.IActiveDoc2.ISelectionManager.GetSelectedObjectsComponent4(1, -1) == null)
                {
                    //if no components selected deselect and disable the command
                    state = CommandItemEnableState_e.DeselectDisable;
                }
            }
        }
    }
}
