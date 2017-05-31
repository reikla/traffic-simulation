using System.Collections.Generic;
using System.Linq;
using TrafficSimulation.Simulation.Engine.Environment;

namespace TrafficSimulation.Simulation.Engine.PathCalculation
{
  /// <summary>
  /// Calculates the shortest path between two specified nodes.
  /// </summary>
  public class ShortestPath
  {
    /// <summary>
    /// Returns a List of NodeConnections, which represent the shortest path.
    /// </summary>
    public readonly List<INodeConnection> Sp = new List<INodeConnection>();

    private readonly List<NodeForSp> _nodesForSp = new List<NodeForSp>();
    private readonly List<NodeConnectionForSp> _nodeConnectionsForSp = new List<NodeConnectionForSp>();

    /// <summary>
    /// Calculates the shortest path between two specified nodes.
    /// </summary>
    /// <param name="allNodeConnections">The full list of connections.</param>
    /// <param name="allNodes">The full list of connections.</param>
    /// <param name="startNodeId">Integer Id of the Start for the path.</param>
    /// <param name="endNodeId">Integer Id of the End for the path.</param>
    public ShortestPath(List<INodeConnection> allNodeConnections, List<INode> allNodes, int startNodeId, int endNodeId)
    {
      var g = new Graph();

      allNodes.ForEach(node => _nodesForSp.Add(new NodeForSp(node.Id.ToString())));
      _nodesForSp.ForEach(node => g.AddNode(node));

      foreach (var con in allNodeConnections)
      {
        var nfspStart = _nodesForSp.FirstOrDefault(node => node.Name.Equals(con.StartNode.Id.ToString()));
        var nfspTarget = _nodesForSp.FirstOrDefault(node => node.Name.Equals(con.EndNode.Id.ToString()));
        var ncfsp = new NodeConnectionForSp(nfspTarget, con.Length);
        _nodeConnectionsForSp.Add(ncfsp);
        nfspStart.AddConnection(nfspTarget, ncfsp.Distance);
        g.AddConnection(nfspStart, ncfsp);
      }

      var calculator = new DistanceCalculator();
      var distances = calculator.CalculateDistances(g, startNodeId.ToString());
      var distancesRoute = new List<KeyValuePair<string, double>>();
      var distancesOrdered = distances.OrderBy(i => i.Value).ToList();

      foreach (var d in distancesOrdered)
      {
        if (d.Key.Equals(endNodeId.ToString()))
        {
          distancesRoute.Add(d);
          break;
        }

        distancesRoute.Add(d);
      }

      var currentNode = g.Nodes.FirstOrDefault(n => n.Key.Equals(startNodeId.ToString())).Value;
      foreach (var d in distancesRoute)
      {
        if (d.Value > 0)
        {
          var ncsp = currentNode?.Connections.FirstOrDefault(nc => nc.Target.Name.Equals(d.Key));
          if (ncsp != null)
          {
            var nodecon =
              allNodeConnections.FirstOrDefault(conn => conn.StartNode.Id.ToString().Equals(currentNode?.Name) &&
                                                        conn.EndNode.Id.ToString().Equals(ncsp?.Target.Name));
            currentNode = _nodesForSp.FirstOrDefault(no => no.Name.Equals(nodecon?.EndNode.Id.ToString()));

            Sp.Add(nodecon);
          }
        }
      }
    }
  }
}
