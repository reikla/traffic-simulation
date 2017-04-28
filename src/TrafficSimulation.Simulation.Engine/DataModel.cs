using System.Collections.Generic;
using System.Linq;
using TrafficSimulation.Simulation.Engine.Environment;

namespace TrafficSimulation.Simulation.Engine
{
  internal class DataModel
  {
    public DataModel()
    {
      Nodes = new List<INode>();
      NodeConnections = new List<INodeConnection>();
      Vehicles = new List<IVehicle>();
      Routes = new List<IRoute>();
    }

    public List<INode> Nodes { get; set; }
    public List<INodeConnection> NodeConnections { get; set; }
    public List<IVehicle> Vehicles { get; set; }
    public List<IRoute> Routes { get; set; }
    internal IEnumerable<IStartNode> StartNodes => Nodes.Where(x => x is IStartNode) as IEnumerable<IStartNode>;
    internal IEnumerable<IEndNode> EndNodes => Nodes.Where(x => x is IEndNode) as IEnumerable<IEndNode>;
  }
}