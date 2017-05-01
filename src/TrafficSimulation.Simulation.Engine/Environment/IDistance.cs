namespace TrafficSimulation.Simulation.Engine.Environment
{
  public interface IDistance
  {
    double DistanceInMeters { get; set; }
    IPlaceable NextPlaceable { get; set; }
  }
}