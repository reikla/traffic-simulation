using System.Collections.Generic;
using System.Linq;
using TrafficSimulation.Simulation.Contracts;

namespace TrafficSimulation.Simulation.Environment
{
  public class Route : SimulationBase, IRoute
  {
    private readonly List<INodeConnection> _nodeConnections;
    public Route(params INodeConnection[] nodeConnections)
    {
      _nodeConnections = new List<INodeConnection>();
      foreach (var connection in nodeConnections)
      {
        _nodeConnections.Add(connection);
      }
    }
    public IReadOnlyList<INodeConnection> NodesConnections => _nodeConnections;
    public double Legth => _nodeConnections.Sum(x => x.Length);
  }
}