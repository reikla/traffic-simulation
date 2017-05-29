using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficSimulation.Simulation.Engine.Environment;

namespace TrafficSimulation.Simulation.Tests
{
  [TestClass]
  public class NodeTests
  {
    [TestMethod]
    public void NodeTests_TestOrientation_WestOk()
    {
      Node start = new Node(1305,15);
      Node end = new Node(795, 15);
      NodeConnection c = new NodeConnection(start, end);
      Assert.AreEqual(Orientation.West, c.ConnectionOrientation);
    }

    [TestMethod]
    public void South()
    {
      Node start = new Node(1305, -135);
      Node end = new Node(1305, 15);
      NodeConnection c = new NodeConnection(start, end);
      Assert.AreEqual(Orientation.South, c.ConnectionOrientation);
    }
  }
}