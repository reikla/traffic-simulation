using System.Collections.Generic;
using Prism.Mvvm;
using TrafficSimulation.Simulation.Contracts.DTO;

namespace TrafficSimulation.UI.Application.ViewModel
{
  /// <summary>
  /// Implements Prism.Mvvm.BindableBase, which is the implementation of System.ComponentModel.INotifyPropertyChanged to simplify models.
  /// </summary>
  class TrafficSimulationViewModel : BindableBase
  {
    /// <summary>
    /// Contains the vehicles for the simulation (received via WCF)
    /// </summary>
    public List<Vehicle> Vehicles { get; set; }
    /// <summary>
    /// Contains the nodes for the simulation (received via WCF)
    /// </summary>
    public List<Node> Nodes { get; set; }
    /// <summary>
    /// Contains the NodeConnections for the simulation (received via WCF)
    /// </summary>
    public List<NodeConnection> NodeConnections { get; set; }

    /// <summary>
    /// Constructor for the TrafficSimulationViewModel - initializes the Lists Vehicles, Nodes and NodeConnections
    /// </summary>
    public TrafficSimulationViewModel()
    {
      Vehicles = new List<Vehicle>();
      Nodes = new List<Node>();
      NodeConnections = new List<NodeConnection>();
    }
  }
}
