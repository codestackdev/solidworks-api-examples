//**********************
//Connect Web Page To Desktop Application
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/solidworks-api-examples/blob/master/LICENSE
//Product URL: https://blog.codestack.net/connect-web-page-desktop
//**********************

using CodeStack.SwEx.AddIn;
using CodeStack.SwEx.AddIn.Attributes;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CodeStack.ModelBuilder
{
    [ComVisible(true), Guid("4511C249-C823-4BAF-9B12-AC646D6925A4")]
    [AutoRegister]
    public class ModelBuilderAddIn : SwAddInEx
    {
        private class Startup
        {
            public void Configuration(IAppBuilder app)
            {
                app.UseCors(CorsOptions.AllowAll);
                app.MapSignalR();
            }
        }

        static ModelBuilderAddIn()
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
        }

        private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var dir = Path.GetDirectoryName(typeof(ModelBuilderAddIn).Assembly.Location);
            var fileName = $"{new AssemblyName(args.Name).Name}.dll";
            return Assembly.LoadFile(Path.Combine(dir, fileName));
        }

        private const string URL = "http://localhost:8080/ModelBuilder/";

        private IDisposable m_SignalRHub;
        private ModelBuilder m_ModelBuilder;

        public override bool OnConnect()
        {
            m_ModelBuilder = new ModelBuilder(App, "D:\\dev\\build");

            GlobalHost.DependencyResolver.Register(
                typeof(ModelBuilderHub),
                () => new ModelBuilderHub(m_ModelBuilder));

            m_SignalRHub = WebApp.Start<Startup>(URL);
            return true;
        }

        public override bool OnDisconnect()
        {
            m_SignalRHub.Dispose();
            return true;
        }
    }
}
