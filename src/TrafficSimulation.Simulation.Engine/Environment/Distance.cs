namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <seealso cref="IDistance" />
  public class Distance : IDistance
  {

    /// <summary>
    /// Initializes a new instance of the <see cref="IDistance"/> class.
    /// </summary>
    /// <param name="placeable">The placeable.</param>
    /// <param name="distanceInMeters">The distance in meters.</param>
    public Distance(IPlaceable placeable, double distanceInMeters)
    {
      NextPlaceable = placeable;
      DistanceInMeters = distanceInMeters;
    }

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
