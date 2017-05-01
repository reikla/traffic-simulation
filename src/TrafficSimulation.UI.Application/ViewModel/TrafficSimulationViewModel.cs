using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Prism.Mvvm;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.Simulation.Contracts.DTO;

namespace TrafficSimulation.UI.Application.ViewModel
{
  class TrafficSimulationViewModel : BindableBase
  {
    
    public List<Vehicle> Vehicles { get; set; }
    public IReadOnlyCollection<Node> Nodes { get; set; }
    public IReadOnlyCollection<NodeConnection> NodeConnections { get; set; }

    public double  CurrPos { get; set; }
    public TrafficSimulationViewModel()
   {
     var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.Transport);
     var ep = new EndpointAddress("net.pipe://localhost/Simulation/Engine");
     var simService = ChannelFactory<ISimulationService>.CreateChannel(binding, ep);

     GetData(simService);

    }

    void GetData(ISimulationService simService)
    {
      Vehicles = simService.GetVehicles();
      Nodes = simService.GetNodes();
      NodeConnections = simService.GetNodeConnections();
      CurrPos = Vehicles.ElementAt(0).PositionOnConnection;

    }

 


 
  }
}
