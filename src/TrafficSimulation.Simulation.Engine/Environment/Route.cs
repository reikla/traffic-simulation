using System.Collections.Generic;
using System.Linq;

namespace TrafficSimulation.Simulation.Engine.Environment
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
    public INodeConnection NextConnection(INodeConnection currentConnection)
    {
      for (var i = 0; i < _nodeConnections.Count; i++)
      {
        if (currentConnection == _nodeConnections[i] && i + 1 < _nodeConnections.Count)
        {
          return _nodeConnections[i + 1];
        }
      }
      return null;
    }
  }
}