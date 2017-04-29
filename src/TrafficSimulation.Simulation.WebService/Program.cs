using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSimulation.Simulation.WebService
{
  class Program
  {
    static void Main(string[] args)
    {
      var serviceHost = new ServiceHost(typeof(SimulationService));
      serviceHost.Open();
    }
  }
}
