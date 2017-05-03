namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <seealso cref="TrafficSimulation.Simulation.Engine.Environment.IDistance" />
  public class Distance : IDistance
  {
    /// <summary>
    /// The distance in meters.
    /// </summary>
    public double DistanceInMeters { get; set; }
    /// <summary>
    /// The placeable were the distance is measured to.
    /// </summary>
    public IPlaceable NextPlaceable { get; set; }
  }
}
