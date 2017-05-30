using System.Collections.Generic;

namespace TrafficSimulation.Simulation.Engine.PathCalculation
{
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

    public void AddConnection(NodeForSP FromNode, NodeConnectionForSp _nodeconnection)
    {
      Nodes[FromNode.Name].AddConnection(Nodes[_nodeconnection.Target.Name], _nodeconnection.Distance, false);
    }
  }
}