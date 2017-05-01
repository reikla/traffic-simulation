namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <summary>
  /// Represents a node in the simulation.
  /// </summary>
  public interface INode : ISimulationBase
  {
    /// <summary>
    /// X coordinate of a node
    /// </summary>
    double X { get; set; }
    /// <summary>
    /// Y coordinate of a node
    /// </summary>
    double Y { get; set; }
  }
}