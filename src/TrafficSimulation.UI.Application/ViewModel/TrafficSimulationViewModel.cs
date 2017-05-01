using System.Collections.Generic;
using Prism.Mvvm;
using TrafficSimulation.Simulation.Contracts.DTO;

namespace TrafficSimulation.UI.Application.ViewModel
{
  class TrafficSimulationViewModel : BindableBase
  {

    public List<Vehicle> Vehicles { get; set; }
    public List<Node> Nodes { get; set; }
    public List<NodeConnection> NodeConnections { get; set; }

    public double CurrPos { get; set; }
    public TrafficSimulationViewModel()
    {
      Vehicles = new List<Vehicle>();
      Nodes = new List<Node>();
      NodeConnections = new List<NodeConnection>();
    }
  }
}
