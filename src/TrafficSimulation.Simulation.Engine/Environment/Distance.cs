namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <seealso cref="IDistance{T}" />
  public class Distance<T> : IDistance<T>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="IDistance{T}"/> class.
    /// </summary>
    /// <param name="placeable">The placeable.</param>
    /// <param name="distanceInMeters">The distance in meters.</param>
    private Distance(T placeable, double distanceInMeters)
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
    public T NextPlaceable { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="placeable"></param>
    /// <param name="distanceInMeters"></param>
    /// <returns></returns>
    public static Distance<T> CreateDistance(T placeable, double distanceInMeters)
    {
      return new Distance<T>(placeable, distanceInMeters);
    }
  }
}