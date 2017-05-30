using System.Collections.Generic;
using TrafficSimulation.Simulation.Engine.Environment;

namespace TrafficSimulation.Simulation.Engine.PathCalculation
{
  class TobisShortestPath : IShortestPath
  {
    public IRoute GetRoute(List<INode> nodes, List<INodeConnection> connections, INode startNode, INode endNode)
    {
      var sp = new ShortestPath(connections, nodes, startNode.Id, endNode.Id);
      return new Route(sp.Sp.ToArray());
    }
  }
}