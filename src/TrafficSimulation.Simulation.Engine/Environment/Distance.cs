namespace TrafficSimulation.Simulation.Engine.Environment
{
  public class Distance : IDistance
  {
    public double DistanceInMeters { get; set; }
    public IPlaceable NextPlaceable { get; set; }
  }
}
