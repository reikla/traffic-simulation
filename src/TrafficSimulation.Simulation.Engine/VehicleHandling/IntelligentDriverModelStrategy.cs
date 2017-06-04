namespace TrafficSimulation.Simulation.Engine.VehicleHandling
{
  /// <summary>
  /// An implementation of the Intelligent Driver Model (IDM)
  /// 
  /// </summary>
  public class IntelligentDriverModelStrategy : IAccelerationStrategy
  {
    private readonly Vehicle _vehicle;
    /// <summary>
    /// ctor
    /// </summary>
    public IntelligentDriverModelStrategy(Vehicle vehicle)
    {
      _vehicle = vehicle;
    }

    /// <summary>
    /// Calculates the acceleration of a vehicle
    /// </summary>
    public void CalculateAcceleration()
    {
      throw new System.NotImplementedException();
    }
  }
}