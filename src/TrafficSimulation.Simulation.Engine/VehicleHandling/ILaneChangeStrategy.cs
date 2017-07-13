namespace TrafficSimulation.Simulation.Engine.VehicleHandling
{
  /// <summary>
  /// Strategy to encapsulate the lane changing of a vehicle
  /// </summary>
  public interface ILaneChangeStrategy
  {

    /// <summary>
    /// Determines if a vehicle shoud change the lane.
    /// </summary>
    /// <returns></returns>
    bool ShouldChangeLange();

    /// <summary>
    /// Changes the lane.
    /// </summary>
    void ChangeLane();
  }
}