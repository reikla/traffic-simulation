using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.ShortestPath;
using TrafficSimulation.Simulation.Engine.Environment;

namespace TrafficSimulation.Simulation.Engine.PathCalculation
{
  internal class QuickShortestPath : IShortestPath
  {
    public IRoute GetRoute(List<INode> nodes, List<INodeConnection> connections, INode startNode, INode endNode)
    {
      var g = new AdjacencyGraph<int, TaggedEdge<int, int>>();
      foreach (var node in nodes)
      {
        g.AddVertex(node.Id);
      }
      foreach (var nodeConnection in connections)
      {
        g.AddEdge(new TaggedEdge<int, int>(nodeConnection.StartNode.Id, nodeConnection.EndNode.Id,
          nodeConnection.Id));
      }

      Func<TaggedEdge<int, int>, double> edgeCost = edge => nodes.First(x => x.Id == edge.Source)
        .NodeConnections.First(x => x.EndNode.Id == edge.Target)
        .Cost;

      DijkstraShortestPathAlgorithm<int,TaggedEdge<int,int>> a = new DijkstraShortestPathAlgorithm<int, TaggedEdge<int, int>>(g,edgeCost);
      //IEnumerable<TaggedEdge<int, int>> path;
      var tryGet = g.ShortestPathsDijkstra(edgeCost, startNode.Id);
      var conns = new List<INodeConnection>();

      if (tryGet(endNode.Id, out var path))
      {
        foreach (var taggedEdge in path)
        {
          var c = connections.First(x => x.Id == taggedEdge.Tag);
          conns.Add(c);
        }
      }
      return new Route(conns.ToArray());
    }
  }
}