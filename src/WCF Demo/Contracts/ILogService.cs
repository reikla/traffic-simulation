using System.ServiceModel;

namespace Contracts
{
    [System.ServiceModel.ServiceContract]
    public interface ILogService
    {
        [OperationContract]
        void Log(string message);
    }
}
