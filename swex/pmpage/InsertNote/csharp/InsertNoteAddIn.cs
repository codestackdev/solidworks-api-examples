using CodeStack.SwEx.AddIn;
using CodeStack.SwEx.AddIn.Attributes;
using CodeStack.SwEx.AddIn.Enums;
using CodeStack.SwEx.PMPage;
using CodeStack.SwEx.PMPage.Attributes;
using InsertNote.Properties;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace InsertNote
{
    [Icon(typeof(Resources), nameof(Resources.insert_note_icon))]
    [Title("Insert Note")]
    public enum Commands_e
    {
        [Title("Insert Note")]
        [CommandItemInfo(true, true, swWorkspaceTypes_e.Drawing)]
        [Icon(typeof(Resources), nameof(Resources.insert_note_icon))]
        InsertNote
    }
        
    [ComVisible(true), Guid("69D797EC-6613-44E1-9EDC-D5CC45936656")]
    [AutoRegister("Insert Note", "Inserts note into drawing", true)]
    public class InsertNoteAddIn : SwAddInEx
    {
        private PropertyManagerPageEx<InsertNotePMPageHandler, NoteData> m_Page;
        private NoteData m_Data;

        public override bool OnConnect()
        {
            AddCommandGroup<Commands_e>(OnCommandClick);
            return true;
        }

        private void OnCommandClick(Commands_e cmd)
        {
            switch (cmd)
            {
                case Commands_e.InsertNote:
                    m_App.IActiveDoc2.ClearSelection2(true);
                    m_Page = new PropertyManagerPageEx<InsertNotePMPageHandler, NoteData>(
                        m_Data ?? (m_Data = new NoteData()), m_App);
                    m_Page.Handler.Closing += (r, a) =>
                    {
                        if (r == swPropertyManagerPageCloseReasons_e.swPropertyManagerPageClose_Okay)
                        {
                            if (string.IsNullOrEmpty(m_Data.Text))
                            {
                                a.Cancel = true;
                                a.ErrorTitle = "Insert Note Error";
                                a.ErrorMessage = "Please specify the note text";
                            }
                        }
                    };
                    m_Page.Handler.Closed += r =>
                    {
                        if (r == swPropertyManagerPageCloseReasons_e.swPropertyManagerPageClose_Okay)
                        {
                            NoteHelper.InsertNote(m_App.IActiveDoc2 as IDrawingDoc,
                                m_Data.Text, m_Data.Size, m_Data.Position.X, m_Data.Position.Y,
                                m_Data.Position.AttachedEntity);
                        }
                    };
                    m_Page.Show();
                    break;
            }
        }
    }
}
