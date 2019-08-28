//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using CodeStack.PrimitivesStandardFeatures.Properties;
using CodeStack.SwEx.AddIn;
using CodeStack.SwEx.AddIn.Attributes;
using CodeStack.SwEx.AddIn.Enums;
using CodeStack.SwEx.Common.Attributes;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CodeStack.PrimitivesStandardFeatures
{
    [ComVisible(true), Guid("2A3F6BE8-5D70-4AF2-A72E-0C570FF2B0CA")]
    [AutoRegister("Geometry Primitives", "Create geometrical primitives" )]
    public class AddIn : SwAddInEx
    {
        [Title("Primitives")]
        [Description("Creates geometricas primitives")]
        [Icon(typeof(Resources), nameof(Resources.primitives_icon))]
        private enum Commands_e
        {
            [Title("Create Cylinder")]
            [Description("Creates extruded cylinder on selected face or plane")]
            [Icon(typeof(Resources), nameof(Resources.cylinder_icon))]
            [CommandItemInfo(true, true, swWorkspaceTypes_e.Part, true)]
            CreateCylinder,

            [Title("Create Box")]
            [Description("Creates extruded box on selected face or plane")]
            [Icon(typeof(Resources), nameof(Resources.box_icon))]
            [CommandItemInfo(true, true, swWorkspaceTypes_e.Part, true)]
            CreateBox
        }

        public override bool OnConnect()
        {
            AddCommandGroup<Commands_e>(OnButtonClick, OnButtonEnable);
            return true;
        }

        private void OnButtonClick(Commands_e cmd)
        {
            try
            {
                switch (cmd)
                {
                    case Commands_e.CreateBox:
                        App.IActiveDoc2.CreateBox(0.1, 0.2, 0.3);
                        break;

                    case Commands_e.CreateCylinder:
                        App.IActiveDoc2.CreateCylinder(0.1, 0.2);
                        break;
                }
            }
            catch(Exception ex)
            {
                App.SendMsgToUser2(ex.Message, (int)swMessageBoxIcon_e.swMbStop, (int)swMessageBoxBtn_e.swMbOk);
            }
        }

        private void OnButtonEnable(Commands_e cmd, ref CommandItemEnableState_e state)
        {
            switch (cmd)
            {
                case Commands_e.CreateBox:
                case Commands_e.CreateCylinder:
                    var selType = (swSelectType_e)App.IActiveDoc2.ISelectionManager.GetSelectedObjectType3(1, -1);

                    if (App.IActiveDoc2 is IPartDoc && (selType == swSelectType_e.swSelFACES || selType == swSelectType_e.swSelDATUMPLANES))
                    {
                        if (selType == swSelectType_e.swSelFACES)
                        {
                            var face = App.IActiveDoc2.ISelectionManager.GetSelectedObject6(1, -1) as IFace2;

                            if (!face.IGetSurface().IsPlane())
                            {
                                state = CommandItemEnableState_e.DeselectDisable;
                            }
                        }
                    }
                    else
                    {
                        state = CommandItemEnableState_e.DeselectDisable;
                    }
                    break;
            }
        }
    }
}
