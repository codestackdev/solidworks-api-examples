//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using CodeStack.PrimitivesStandardFeaturesPMPage.Properties;
using CodeStack.SwEx.AddIn;
using CodeStack.SwEx.AddIn.Attributes;
using CodeStack.SwEx.AddIn.Enums;
using CodeStack.SwEx.Common.Attributes;
using CodeStack.SwEx.PMPage;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CodeStack.PrimitivesStandardFeaturesPMPage
{
    [ComVisible(true), Guid("F563ED8C-B293-4EAF-AFCC-C6730478F54F")]
    [AutoRegister("Geometry Primitives With PMPage", "Create geometrical primitives with Property Manager Page" )]
    public class AddIn : SwAddInEx
    {
        [Title("Primitives")]
        [Description("Creates geometrical primitives")]
        [Icon(typeof(Resources), nameof(Resources.primitives_icon))]
        private enum Commands_e
        {
            [Title(typeof(Resources), nameof(Resources.CommandTitleCreateCylinder))]
            [Description("Creates extruded cylinder on selected face or plane")]
            [Icon(typeof(Resources), nameof(Resources.cylinder_icon))]
            [CommandItemInfo(true, true, swWorkspaceTypes_e.Part, true)]
            CreateCylinder,

            [Title(typeof(Resources), nameof(Resources.CommandTitleCreateBox))]
            [Description("Creates extruded box on selected face or plane")]
            [Icon(typeof(Resources), nameof(Resources.box_icon))]
            [CommandItemInfo(true, true, swWorkspaceTypes_e.Part, true)]
            CreateBox
        }

        private BoxData m_CurBoxData;
        private CylinderData m_CurCylData;

        private PropertyManagerPageEx<PropertyPageHandler, BoxData> m_BoxPmPage;
        private PropertyManagerPageEx<PropertyPageHandler, CylinderData> m_CylPmPage;

        public override bool OnConnect()
        {
            AddCommandGroup<Commands_e>(OnButtonClick);

            m_CurBoxData = new BoxData()
            {
                Height = 0.1,
                Length = 0.2,
                Width = 0.3
            };

            m_CurCylData = new CylinderData()
            {
                Diameter = 0.1,
                Height = 0.2
            };

            m_BoxPmPage = new PropertyManagerPageEx<PropertyPageHandler, BoxData>(App);
            m_BoxPmPage.Handler.Closing += OnBoxPageClosing;
            m_BoxPmPage.Handler.Closed += OnBoxPageClosed;

            m_CylPmPage = new PropertyManagerPageEx<PropertyPageHandler, CylinderData>(App);
            m_CylPmPage.Handler.Closing += OnCylPageClosing;
            m_CylPmPage.Handler.Closed += OnCylPageClosed;

            return true;
        }

        private void OnBoxPageClosing(swPropertyManagerPageCloseReasons_e reason, SwEx.PMPage.Base.ClosingArg arg)
        {
            ValidateReference(m_CurBoxData.Reference, reason, arg);
        }

        private void OnCylPageClosing(swPropertyManagerPageCloseReasons_e reason, SwEx.PMPage.Base.ClosingArg arg)
        {
            ValidateReference(m_CurCylData.Reference, reason, arg);
        }

        private void ValidateReference(IEntity reference, swPropertyManagerPageCloseReasons_e reason, SwEx.PMPage.Base.ClosingArg arg)
        {
            if (reason == swPropertyManagerPageCloseReasons_e.swPropertyManagerPageClose_Okay)
            {
                if (reference == null)
                {
                    arg.ErrorMessage = "Select reference";
                    arg.Cancel = true;
                }
            }
        }

        private void OnBoxPageClosed(swPropertyManagerPageCloseReasons_e reason)
        {
            if (reason == swPropertyManagerPageCloseReasons_e.swPropertyManagerPageClose_Okay)
            {
                App.IActiveDoc2.CreateBox(m_CurBoxData.Reference, m_CurBoxData.Width, m_CurBoxData.Length, m_CurBoxData.Height);
            }
        }

        private void OnCylPageClosed(swPropertyManagerPageCloseReasons_e reason)
        {
            if (reason == swPropertyManagerPageCloseReasons_e.swPropertyManagerPageClose_Okay)
            {
                App.IActiveDoc2.CreateCylinder(m_CurCylData.Reference, m_CurCylData.Diameter, m_CurCylData.Height);
            }
        }

        private void OnButtonClick(Commands_e cmd)
        {
            try
            {
                switch (cmd)
                {
                    case Commands_e.CreateBox:
                        m_BoxPmPage.Show(m_CurBoxData);
                        break;

                    case Commands_e.CreateCylinder:
                        m_CylPmPage.Show(m_CurCylData);
                        break;
                }
            }
            catch(Exception ex)
            {
                App.SendMsgToUser2(ex.Message, (int)swMessageBoxIcon_e.swMbStop, (int)swMessageBoxBtn_e.swMbOk);
            }
        }
    }
}
