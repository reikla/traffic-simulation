using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficSimulation.Simulation.Engine;
using System.Collections.Generic;
using System.Linq;
using TrafficSimulation.Simulation.Engine.Environment;
using TrafficSimulation.Simulation.Engine.Xml;

namespace TrafficSimulation.Simulation.Tests
{
  [TestClass]
  public class ShortestPathTests
  {
    [TestMethod]
    public void OutputShortestPath()
    {
    
      XmlGraphReader gr = new XmlGraphReader();

      gr.Read("test1.graphml");

      var startNode = gr.Nodes.First(x => x.NodeType == NodeType.StartNode);

      var endNode = gr.Nodes.First(x => x.NodeType == NodeType.EndNode);

      var sp = new ShortestPath(gr.NodeConnections, gr.Nodes, startNode.Id, endNode.Id);

     
    }
  }
}
