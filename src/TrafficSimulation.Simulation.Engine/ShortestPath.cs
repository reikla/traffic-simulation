using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficSimulation.Simulation.Engine.Environment;

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
        throw new ArgumentException("INode may not connect to itself.");
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
      var INode = _node;
      Nodes.Add(INode.Name, INode);
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
        throw new ArgumentException("Starting INode must be in graph.");
      InitialiseGraph(graph, startingNode);
      ProcessGraph(graph, startingNode);
      return ExtractDistances(graph);

    }
    private void InitialiseGraph(Graph graph, string startingNode)
    {
      foreach (NodeForSP INode in graph.Nodes.Values)
        INode.DistanceFromStart = double.PositiveInfinity;
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

    private void ProcessNode(NodeForSP INode, List<NodeForSP> queue)
    {
      var connections = INode.Connections.Where(c => queue.Contains(c.Target));
      foreach (var connection in connections)
      {
        double distance = INode.DistanceFromStart + connection.Distance;
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
    /// <summary>
    /// Returns a List of NodeConnections, which represent the shortest path.
    /// </summary>
    public List<INodeConnection> SP = new List<INodeConnection>();
    List<NodeForSP> NodesForSP = new List<NodeForSP>();
    List<NodeConnectionForSP> NodeConnectionsForSP = new List<NodeConnectionForSP>();

    /// <summary>
    /// Calculates the shortest path between two specified nodes.
    /// </summary>
    /// <param name="AllNodeConnections">The full list of connections.</param>
    /// <param name="AllNodes">The full list of connections.</param>
    /// <param name="StartNodeId">Integer Id of the Start for the path.</param>
    /// <param name="EndNodeId">Integer Id of the End for the path.</param>
    public ShortestPath( List<INodeConnection> AllNodeConnections,List<INode> AllNodes, int StartNodeId, int EndNodeId)
    {
      Graph g = new Graph();

      AllNodes.ForEach(INode => NodesForSP.Add(new NodeForSP(INode.Id.ToString())));
      NodesForSP.ForEach(INode => g.AddNode(INode));

      foreach(var con in AllNodeConnections)
      {
        NodeForSP nfspStart = NodesForSP.Where(node => node.Name.Equals(con.StartNode.Id.ToString())).FirstOrDefault();
        NodeForSP nfspTarget = NodesForSP.Where(node => node.Name.Equals(con.EndNode.Id.ToString())).FirstOrDefault();
        NodeConnectionForSP ncfsp = new NodeConnectionForSP(nfspTarget, con.Length );
        NodeConnectionsForSP.Add(ncfsp);
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

      foreach(var d in distances_ordered)
      {
        if(d.Key.Equals(EndNodeId.ToString()))
        {
          distances_route.Add(d);
          break;         
        }
        distances_route.Add(d);
      }

      NodeForSP current_node = g.Nodes.Where(n => n.Key.Equals(StartNodeId.ToString())).FirstOrDefault().Value;
      foreach(var d in distances_route)
      {
        if (d.Value > 0)
        {
          NodeConnectionForSP ncsp = current_node.Connections.Where(nc => nc.Target.Name.Equals(d.Key)).FirstOrDefault();
          INodeConnection nodecon = AllNodeConnections.Where(conn => conn.StartNode.Id.ToString().Equals(current_node.Name) && conn.EndNode.Id.ToString().Equals(ncsp.Target.Name)).FirstOrDefault();
          current_node = NodesForSP.Where(no => no.Name.Equals(nodecon.EndNode.Id.ToString())).FirstOrDefault();
          SP.Add(nodecon);
        }
      }


      foreach (var d in distances)
      {
        Console.WriteLine("{0}, {1}", d.Key, d.Value);


      }

      




    }


  }



}
