namespace TrafficSimulation.Simulation.Engine.VehicleHandling
{
  /// <summary>
  /// Implementation of <see cref="ILaneChangeStrategy" /> that doesn't change lanes;
  ///  </summary>
  /// 
  internal class NoLaneChangeStrategy : ILaneChangeStrategy
  {
    public bool ShouldChangeLange()
    {
      return false;
    }

    public void ChangeLane()
    {
      throw new System.NotImplementedException();
    }
  }
}