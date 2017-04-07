using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for server. Press button to start.");
            Console.ReadKey();

            NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            EndpointAddress ep = new EndpointAddress(Constants.Address);
            ILogService logService = ChannelFactory<ILogService>.CreateChannel(binding, ep);

            Console.WriteLine("Connected to Log Server.");
            Console.WriteLine("Enter empty line to exit.");

            try
            {
                while (true)
                {
                    Console.WriteLine("Enter Log Message: ");
                    var message = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(message))
                    {
                        throw new Exception("Exit");
                    }
                    logService.Log(message);
                }
            }
            catch (Exception)
            {
                //exit
            }
        }
    }
}