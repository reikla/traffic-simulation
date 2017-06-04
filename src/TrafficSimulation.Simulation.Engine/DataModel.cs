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
      Routes = new List<IRoute>();
    }

    public List<INode> Nodes { get; set; }
    public List<INodeConnection> NodeConnections { get; set; }
    public List<IVehicle> Vehicles => Routes.SelectMany(x => x.Vehicles).ToList();
    public List<IRoute> Routes { get; set; }
    public IEnumerable<INode> StartNodes => Nodes.Where(x => x.NodeType == NodeType.StartNode);
    public IEnumerable<INode> EndNodes => Nodes.Where(x => x.NodeType == NodeType.EndNode);
    public List<ITrafficLight> TrafficLights { get; set; }
  }
}