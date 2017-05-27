using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficSimulation.Simulation.Contracts.DTO;

namespace TrafficSimulation.Simulation.Engine
{
  internal class NodeConnectionForSP
  {
    internal NodeForSP Target { get; private set; }
    internal double Distance { get; private set; }

    internal NodeConnectionForSP(NodeForSP target, double distance)
    {
      Target = target;
      Distance = distance;
    }
  }
  internal class NodeForSP
  {
    IList<NodeConnectionForSP> _connections;

    internal string Name { get; private set; }

    internal double DistanceFromStart { get; set; }

    internal IEnumerable<NodeConnectionForSP> Connections
    {
      get { return _connections; }
    }

    internal NodeForSP(string name)
    {
      Name = name;
      _connections = new List<NodeConnectionForSP>();
    }

    internal void AddConnection(NodeForSP targetNode, double distance, bool twoWay)
    {
      if (targetNode == null) throw new ArgumentNullException("targetNode");
      if (targetNode == this)
        throw new ArgumentException("Node may not connect to itself.");
      if (distance <= 0) throw new ArgumentException("Distance must be positive.");

      _connections.Add(new NodeConnectionForSP(targetNode, distance));
      if (twoWay) targetNode.AddConnection(this, distance, false);
    }
  }

  internal class Graph
  {
    internal IDictionary<string, NodeForSP> Nodes { get; private set; }

    public Graph()
    {
      Nodes = new Dictionary<string, NodeForSP>();
    }

    public void AddNode(NodeForSP _node)
    {
      var node = _node;
      Nodes.Add(node.Name, node);
    }

    public void AddConnection(NodeForSP FromNode, NodeConnectionForSP _nodeconnection)
    {
      Nodes[FromNode.Name].AddConnection(Nodes[_nodeconnection.Target.Name], _nodeconnection.Distance, false);
    }
  }

  internal class DistanceCalculator
  {
    internal IDictionary<string, double> CalculateDistances(Graph graph, string startingNode)
    {
      if (!graph.Nodes.Any(n => n.Key == startingNode))
        throw new ArgumentException("Starting node must be in graph.");
      InitialiseGraph(graph, startingNode);
      ProcessGraph(graph, startingNode);
      return ExtractDistances(graph);

    }
    private void InitialiseGraph(Graph graph, string startingNode)
    {
      foreach (NodeForSP node in graph.Nodes.Values)
        node.DistanceFromStart = double.PositiveInfinity;
      graph.Nodes[startingNode].DistanceFromStart = 0;
    }

    private void ProcessGraph(Graph graph, string startingNode)
    {
      bool finished = false;
      var queue = graph.Nodes.Values.ToList();
      while (!finished)
      {
        NodeForSP nextNode = queue.OrderBy(n => n.DistanceFromStart).FirstOrDefault(
            n => !double.IsPositiveInfinity(n.DistanceFromStart));
        if (nextNode != null)
        {
          ProcessNode(nextNode, queue);
          queue.Remove(nextNode);
        }
        else
        {
          finished = true;
        }
      }
    }

    private void ProcessNode(NodeForSP node, List<NodeForSP> queue)
    {
      var connections = node.Connections.Where(c => queue.Contains(c.Target));
      foreach (var connection in connections)
      {
        double distance = node.DistanceFromStart + connection.Distance;
        if (distance < connection.Target.DistanceFromStart)
          connection.Target.DistanceFromStart = distance;
      }
    }

    private IDictionary<string, double> ExtractDistances(Graph graph)
    {
      return graph.Nodes.ToDictionary(n => n.Key, n => n.Value.DistanceFromStart);
    }
  }

  /// <summary>
  /// Calculates the shortest path between two specified nodes.
  /// </summary>
  public class ShortestPath
  {
    List<NodeForSP> NodesForSP = new List<NodeForSP>();
    List<NodeConnectionForSP> NodeConnectionsForSP = new List<NodeConnectionForSP>();

    /// <summary>
    /// Calculates the shortest path between two specified nodes.
    /// </summary>
    /// <param name="AllNodeConnections">The full list of connections.</param>
    /// <param name="AllNodes">The full list of connections.</param>
    /// <param name="StartNodeId">Integer Id of the Start for the path.</param>
    /// <param name="EndNodeId">Integer Id of the End for the path.</param>
    public ShortestPath( List<NodeConnection> AllNodeConnections,List<Node> AllNodes, int StartNodeId, int EndNodeId)
    {
      Graph g = new Graph();

      AllNodes.ForEach(node => NodesForSP.Add(new NodeForSP(node.Id.ToString())));
      NodesForSP.ForEach(node => g.AddNode(node));

      foreach(var con in AllNodeConnections)
      {
        NodeForSP nfspStart = NodesForSP.Where(node => node.Name.Equals(con.StartNodeId.ToString())).FirstOrDefault();
        NodeForSP nfspTarget = NodesForSP.Where(node => node.Name.Equals(con.EndNodeId.ToString())).FirstOrDefault();
        NodeConnectionForSP ncfsp = new NodeConnectionForSP(nfspTarget, con.Length );
        nfspStart.AddConnection(nfspTarget, ncfsp.Distance, false);
        g.AddConnection(nfspStart, ncfsp);
      }

      var calculator = new DistanceCalculator();
      var distances = calculator.CalculateDistances(g, StartNodeId.ToString());

      foreach (var d in distances)
      {
        Console.WriteLine("{0}, {1}", d.Key, d.Value);
      }


    }


  }



}
