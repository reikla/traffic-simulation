using System.Collections.Generic;
using TrafficSimulation.Simulation.Engine.Environment;

namespace TrafficSimulation.Simulation.Engine.PathCalculation
{
  /// <summary>
  /// Interface to calculate the shortest between two nodes.
  /// </summary>
  public interface IShortestPath
  {
    /// <summary>
    /// Gets the shortest route.
    /// </summary>
    /// <param name="nodes">The nodes.</param>
    /// <param name="connections">The connections.</param>
    /// <param name="startNode">The start node.</param>
    /// <param name="endNode">The end node.</param>
    /// <returns></returns>
    IRoute GetRoute(List<INode> nodes, List<INodeConnection> connections, INode startNode, INode endNode);
  }
}