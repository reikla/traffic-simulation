using System.ServiceModel;
using Contracts;

namespace Server
{
    public class Server
    {
        private ServiceHost serviceHost;

        public Server()
        {
            serviceHost = new ServiceHost(typeof(LogService));
            NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            serviceHost.AddServiceEndpoint(typeof(ILogService), binding, Constants.Address);
        }

        public void Start()
        {
            serviceHost.Open();
        }

        public void Stop()
        {
            serviceHost.Close();
        }
    }
}