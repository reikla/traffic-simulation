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
    public List<INodeConnection> Sp = new List<INodeConnection>();

    readonly List<NodeForSP> _nodesForSp = new List<NodeForSP>();
    readonly List<NodeConnectionForSp> _nodeConnectionsForSp = new List<NodeConnectionForSp>();

    /// <summary>
    /// Calculates the shortest path between two specified nodes.
    /// </summary>
    /// <param name="AllNodeConnections">The full list of connections.</param>
    /// <param name="AllNodes">The full list of connections.</param>
    /// <param name="StartNodeId">Integer Id of the Start for the path.</param>
    /// <param name="EndNodeId">Integer Id of the End for the path.</param>
    public ShortestPath(List<INodeConnection> AllNodeConnections, List<INode> AllNodes, int StartNodeId, int EndNodeId)
    {
      Graph g = new Graph();

      AllNodes.ForEach(node => _nodesForSp.Add(new NodeForSP(node.Id.ToString())));
      _nodesForSp.ForEach(node => g.AddNode(node));

      foreach (var con in AllNodeConnections)
      {
        NodeForSP nfspStart = _nodesForSp.FirstOrDefault(node => node.Name.Equals(con.StartNode.Id.ToString()));
        NodeForSP nfspTarget = _nodesForSp.FirstOrDefault(node => node.Name.Equals(con.EndNode.Id.ToString()));
        NodeConnectionForSp ncfsp = new NodeConnectionForSp(nfspTarget, con.Length);
        _nodeConnectionsForSp.Add(ncfsp);
        nfspStart.AddConnection(nfspTarget, ncfsp.Distance, false);
        g.AddConnection(nfspStart, ncfsp);
      }

      var calculator = new DistanceCalculator();
      var distances = calculator.CalculateDistances(g, StartNodeId.ToString());
      List<KeyValuePair<string, double>> distances_route = new List<KeyValuePair<string, double>>();
      List<KeyValuePair<string, double>> distances_ordered = new List<KeyValuePair<string, double>>();

      foreach (var d in distances.OrderBy(i => i.Value))
      {
        distances_ordered.Add(d);
      }

      foreach (var d in distances_ordered)
      {
        if (d.Key.Equals(EndNodeId.ToString()))
        {
          distances_route.Add(d);
          break;
        }

        distances_route.Add(d);
      }

      NodeForSP current_node = g.Nodes.FirstOrDefault(n => n.Key.Equals(StartNodeId.ToString())).Value;
      foreach (var d in distances_route)
      {
        if (d.Value > 0)
        {
          NodeConnectionForSp ncsp = current_node?.Connections.FirstOrDefault(nc => nc.Target.Name.Equals(d.Key));
          if (ncsp != null)
          {
            INodeConnection nodecon =
              AllNodeConnections.FirstOrDefault(conn => conn.StartNode.Id.ToString().Equals(current_node?.Name) &&
                                                        conn.EndNode.Id.ToString().Equals(ncsp?.Target.Name));
            current_node = _nodesForSp.FirstOrDefault(no => no.Name.Equals(nodecon?.EndNode.Id.ToString()));

            Sp.Add(nodecon);
          }
        }
      }
    }
  }
}
