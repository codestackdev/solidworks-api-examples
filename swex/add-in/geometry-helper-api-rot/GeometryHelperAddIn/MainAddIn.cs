using CodeStack.SwEx.AddIn;
using CodeStack.SwEx.AddIn.Attributes;
using CodeStack.SwEx.Common.Attributes;
using SolidWorks.Interop.swconst;
using System;
using System.Runtime.InteropServices;

namespace CodeStack.GeometryHelper
{
    [AutoRegister("GeometryHelperSwAddIn")]
    [ComVisible(true), Guid("2ACE1951-6182-496C-A643-B5FA6CFDFFD8")]
    public class MainAddIn : SwAddInEx
    {
        private IGeometryHelperApiObject m_ApiObject;
        private GeometryHelperService m_GeometryService;

        [Title("Geometry Helper")]
        public enum Commands_e
        {
            [Title("Get Faces Count")]
            GetFacesCount
        }

        public override bool OnConnect()
        {
            m_GeometryService = new GeometryHelperService(App);

            //option1
            //var proxy = new GeometryHelperApiObjectProxy();
            //proxy.GetFacesCountRequested += OnGetFacesCountRequested;
            //m_ApiObject = proxy;
            //

            //option2
            m_ApiObject = new GeometryHelperApiObject(m_GeometryService);
            //

            RotHelper.Register(m_ApiObject, new GeometryHelperApiObjectFactory().GetName(App.GetProcessID()));

            this.AddCommandGroup<Commands_e>(OnButtonClick);

            return true;
        }

        private void OnButtonClick(Commands_e cmd)
        {
            switch (cmd)
            {
                case Commands_e.GetFacesCount:
                    try
                    {
                        var count = GetFacesCount(0);
                        App.SendMsgToUser2($"Selected body contains: {count} face(s)",
                            (int)swMessageBoxIcon_e.swMbInformation,
                            (int)swMessageBoxBtn_e.swMbOk);
                    }
                    catch(Exception ex)
                    {
                        App.SendMsgToUser2(ex.Message,
                            (int)swMessageBoxIcon_e.swMbStop,
                            (int)swMessageBoxBtn_e.swMbOk);
                    }
                    break;
            }
        }

        //option1
        //private int OnGetFacesCountRequested(double minArea)
        //{
        //    return GetFacesCount(minArea);
        //}

        private int GetFacesCount(double minArea)
        {
            return m_GeometryService.GetFacesCountFromSelectedBody(minArea);
        }
    }
}
