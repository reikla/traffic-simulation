namespace TrafficSimulation.Simulation.Contracts
{
  /// <summary>
  /// The interface for all objects that can be placed within a node connection.
  /// </summary>
  public interface IPlaceable
  {
    /// <summary>
    /// The position 
    /// </summary>
    IPosition Position { get; set; }
    /// <summary>
    /// Signals if this placable is blocking the node connection where it lives on.
    /// </summary>
    bool IsConnectionBlocking { get; set; }
  }
}