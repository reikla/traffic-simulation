namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <inheritdoc />>
  public class Distance : IDistance
  {
    /// <inheritdoc />>
    public double DistanceInMeters { get; set; }
    /// <inheritdoc />>
    public IPlaceable NextPlaceable { get; set; }
  }
}
