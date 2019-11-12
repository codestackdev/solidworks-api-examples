//**********************
//Connect Web Page To Desktop Application
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/solidworks-api-examples/blob/master/LICENSE
//Product URL: https://blog.codestack.net/connect-web-page-desktop
//**********************

using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace CodeStack.ModelBuilder
{
    [HubName("ModelBuilder")]
    public class ModelBuilderHub : Hub<IModelBuilderClient>
    {
        private readonly ModelBuilder m_ModelBuilder;

        public ModelBuilderHub(ModelBuilder modelBuilder)
        {
            m_ModelBuilder = modelBuilder;
        }

        public void Build(double width, double length, double height)
        {
            int totalModelsBuilt;
            var mass = m_ModelBuilder.Build(width, height, length, out totalModelsBuilt);
            Clients.All.UpdateStatus(totalModelsBuilt);
            Clients.Caller.SendResult(mass);
        }
    }
}
