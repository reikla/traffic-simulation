using System.Collections.Generic;
using TrafficSimulation.Simulation.Contracts;

namespace TrafficSimulation.Simulation.Engine
{
  internal class DataModel
  {
    public DataModel()
    {
      Nodes = new List<INode>();
      NodeConnections = new List<INodeConnection>();
      Vehicles = new List<IVehicle>();
    }

    public List<INode> Nodes { get; set; }
    public List<INodeConnection> NodeConnections { get; set; }
    public List<IVehicle> Vehicles { get; set; }
  }
}