using CodeStack.ProgressHandler.Properties;
using CodeStack.SwEx.AddIn;
using CodeStack.SwEx.AddIn.Attributes;
using CodeStack.SwEx.AddIn.Enums;
using CodeStack.SwEx.Common.Attributes;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeStack.ProgressHandler
{
    [ComVisible(true), Guid("E6D0A834-3869-4796-A3A8-B5CBBC432842")]
    [AutoRegister("ProgressHandlerAddIn")]
    public class AddIn : SwAddInEx
    {
        private const int ITERATIONS_COUNT = 1000;

        [Title("ProgressHandler")]
        private enum Commands_e
        {
            [CommandItemInfo(swWorkspaceTypes_e.Part)]
            [Title("Report Each Step")]
            [Icon(typeof(Resources), nameof(Resources.toggle))]
            ReportEachStep,

            [CommandItemInfo(swWorkspaceTypes_e.Part)]
            [Title("BL - No Progress Capturing (Baseline)")]
            [Icon(typeof(Resources), nameof(Resources.bl))]
            NoProgress,

            [CommandItemInfo(swWorkspaceTypes_e.Part)]
            [Title("A - User Form In Main Thread")]
            [Icon(typeof(Resources), nameof(Resources.a))]
            UserForm,

            [CommandItemInfo(swWorkspaceTypes_e.Part)]
            [Title("B - User Progress Bar")]
            [Icon(typeof(Resources), nameof(Resources.b))]
            UserProgressBar,

            [CommandItemInfo(swWorkspaceTypes_e.Part)]
            [Title("C - Operation In Background Thread (Task)")]
            [Icon(typeof(Resources), nameof(Resources.c))]
            Task,

            [CommandItemInfo(swWorkspaceTypes_e.Part)]
            [Title("D - Do Events In Main Thread")]
            [Icon(typeof(Resources), nameof(Resources.d))]
            DoEvents,

            [CommandItemInfo(swWorkspaceTypes_e.Part)]
            [Title("E - User Form In Background Thread")]
            [Icon(typeof(Resources), nameof(Resources.e))]
            Thread
        }

        private bool m_ReportEachStep;

        public override bool OnConnect()
        {
            AddCommandGroup<Commands_e>(OnButtonClick, OnButtonEnable);
            return true;
        }

        private void OnButtonClick(Commands_e cmd)
        {
            switch (cmd)
            {
                case Commands_e.ReportEachStep:
                    m_ReportEachStep = !m_ReportEachStep;
                    break;

                case Commands_e.NoProgress:
                    DoNoProgress();
                    break;

                case Commands_e.UserForm:
                    DoUserForm();
                    break;

                case Commands_e.UserProgressBar:
                    DoUserProgressBar();
                    break;

                case Commands_e.Task:
                    DoTask();
                    break;

                case Commands_e.DoEvents:
                    DoDoEvents();
                    break;

                case Commands_e.Thread:
                    DoThread();
                    break;
            }
        }

        private void OnButtonEnable(Commands_e cmd, ref CommandItemEnableState_e state)
        {
            if (state == CommandItemEnableState_e.DeselectEnable || state == CommandItemEnableState_e.SelectEnable)
            {
                if (!GetAllBodies().Any())
                {
                    state = CommandItemEnableState_e.DeselectDisable;
                }
                else
                {
                    if (cmd == Commands_e.ReportEachStep)
                    {
                        state = m_ReportEachStep ? CommandItemEnableState_e.SelectEnable : CommandItemEnableState_e.DeselectEnable;
                    }
                }
            }
        }

        private void DoNoProgress()
        {
            DoWork(null, null, null);
        }

        private void DoUserForm()
        {
            var form = new ProgressForm();

            form.Show();

            DoWork(
                (title, upperBound) => form.Init(title, upperBound),
                title => form.SetTitle(title),
                progress => form.SetProgress(progress));

            form.Close();
        }

        private void DoUserProgressBar()
        {
            UserProgressBar prgBar;
            App.GetUserProgressBar(out prgBar);

            DoWork(
                (title, upperBound) => prgBar.Start(0, upperBound, title),
                title => prgBar.UpdateTitle(title),
                progress => prgBar.UpdateProgress(progress));

            prgBar.End();
        }

        private async void DoTask()
        {
            var form = new ProgressForm();

            form.Show();

            await Task.Run(() => DoWork(
                (title, upperBound) => form.Init(title, upperBound),
                title => form.SetTitle(title),
                progress => form.SetProgress(progress)));

            form.Close();
        }

        private void DoDoEvents()
        {
            var form = new ProgressForm();

            form.Show();

            DoWork(
                (title, upperBound) => form.Init(title, upperBound),
                title =>
                {
                    form.SetTitle(title);
                    Application.DoEvents();
                },
                progress =>
                {
                    form.SetProgress(progress);
                    Application.DoEvents();
                });

            form.Close();
        }

        private void DoThread()
        {
            ProgressForm form = null;
            
            var th = new Thread(()=> 
            {
                form = new ProgressForm();
                form.ShowDialog();
            });

            th.SetApartmentState(ApartmentState.STA);

            th.Start();

            while (form == null || !form.IsHandleCreated)
            {
                Thread.Sleep(100);
            }

            DoWork(
                (title, upperBound) => form.Invoke(new Action(() => form.Init(title, upperBound))),
                title => form.Invoke(new Action(() => form.SetTitle(title))),
                progress => form.Invoke(new Action(() => form.SetProgress(progress))));

            form.Invoke(new Action(() => form.Close()));
        }

        private void DoWork(Action<string, int> startedCallback, Action<string> msgChangedCallback, Action<int> progressChangedCallback)
        {
            var start = DateTime.Now;

            var bodies = GetAllBodies().ToArray();

            var totalIterations = bodies.Sum(b => b.GetFaceCount()) * ITERATIONS_COUNT;

            startedCallback?.Invoke($"Processing {bodies.Length} body(s)",
                m_ReportEachStep ? totalIterations : 100);

            int pos = 0;
            int prevRepPrg = 0;

            foreach (var body in bodies)
            {
                var faces = body.GetFaces() as object[];

                msgChangedCallback?.Invoke($"Processing {body.Name} with {faces.Length} face(s)");

                foreach (IFace2 face in faces)
                {
                    for (int i = 0; i < ITERATIONS_COUNT; i++)
                    {
                        pos++;

                        face.GetArea();
                        var surf = face.IGetSurface();
                        surf.GetClosestPointOn(0, 0, 0);

                        if (m_ReportEachStep)
                        {
                            progressChangedCallback?.Invoke(pos);
                        }
                        else
                        {
                            var prg = (int)(((double)pos / (double)totalIterations) * 100);

                            if (prg != prevRepPrg)
                            {
                                progressChangedCallback?.Invoke(prg);
                                prevRepPrg = prg;
                            }
                        }
                    }
                }
            }

            App.SendMsgToUser($"Operation completed in {(DateTime.Now - start).TotalSeconds} second(s)");
        }

        private IEnumerable<IBody2> GetAllBodies()
        {
            var part = App.IActiveDoc2 as IPartDoc;
            var bodies = part.GetBodies2((int)swBodyType_e.swAllBodies, false) as object[];

            if (bodies != null)
            {
                return bodies.Cast<IBody2>();
            }
            else
            {
                return Enumerable.Empty<IBody2>();
            }
        }
    }
}
