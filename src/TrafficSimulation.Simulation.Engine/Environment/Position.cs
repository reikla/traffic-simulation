namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <summary>
  ///  A Position is a point on a <see cref="INodeConnection"/> where <see cref="IPlaceable"/> are placed
  /// </summary>
  /// <seealso cref="TrafficSimulation.Simulation.Engine.Environment.IPosition" />
  public class Position : IPosition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Position"/> class.
    /// </summary>
    /// <param name="connection">The connection.</param>
    public Position(INodeConnection connection)
    {
      NodeConnection = connection;
      PositionOnConnection = 0;
    }
    /// <summary>
    /// The connection where the position is
    /// </summary>
    public INodeConnection NodeConnection { get; set; }
    /// <summary>
    /// The position on the connection.
    /// </summary>
    public double PositionOnConnection { get; set; }
  }
}