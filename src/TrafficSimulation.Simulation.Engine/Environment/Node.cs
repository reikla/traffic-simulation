namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <summary>
  /// A Node in the Simulation is a point that is connected with other points with <see cref="INodeConnection"/>
  /// </summary>
  /// <seealso cref="TrafficSimulation.Simulation.Engine.SimulationBase" />
  /// <seealso cref="TrafficSimulation.Simulation.Engine.Environment.INode" />
  public class Node : SimulationBase, INode
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Node"/> class.
    /// </summary>
    /// <param name="x">The x coordinate</param>
    /// <param name="y">The y coordinate</param>
    public Node(double x, double y)
    {
      X = x;
      Y = y;
    }

    /// <summary>
    /// X coordinate of a node
    /// </summary>
    public double X { get; set; }
    /// <summary>
    /// Y coordinate of a node
    /// </summary>
    public double Y { get; set; }
  }
}
