//**********************
//Connect Web Page To Desktop Application
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/solidworks-api-examples/blob/master/LICENSE
//Product URL: https://blog.codestack.net/connect-web-page-desktop
//**********************

namespace CodeStack.ModelBuilder
{
    public interface IModelBuilderClient
    {
        void SendResult(double mass);
        void UpdateStatus(int totalModelsBuilt);
    }
}
