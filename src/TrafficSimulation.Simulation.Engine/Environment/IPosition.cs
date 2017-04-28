namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <summary>
  /// Represents a position on a node connection
  /// </summary>
  public interface IPosition
  {
    /// <summary>
    /// The connection where the position is
    /// </summary>
    INodeConnection NodeConnection { get; set; }

    /// <summary>
    /// The position on the connection.
    /// </summary>
    double PositionOnConnection { get; set; }
  }
}