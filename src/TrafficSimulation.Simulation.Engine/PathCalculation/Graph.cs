using System.Collections.Generic;

namespace TrafficSimulation.Simulation.Engine.PathCalculation
{
  internal class Graph
  {
    internal IDictionary<string, NodeForSp> Nodes { get; private set; }

    public Graph()
    {
      Nodes = new Dictionary<string, NodeForSp>();
    }

    public void AddNode(NodeForSp node)
    {
      Nodes.Add(node.Name, node);
    }

    public void AddConnection(NodeForSp fromNode, NodeConnectionForSp nodeconnection)
    {
      Nodes[fromNode.Name].AddConnection(Nodes[nodeconnection.Target.Name], nodeconnection.Distance);
    }
  }
}