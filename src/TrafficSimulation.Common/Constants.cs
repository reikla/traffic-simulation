namespace TrafficSimulation.Common
{

  /// <summary>
  /// A collection of constants for the simulation
  /// </summary>
  public static class Constants
  {
    /// <summary>
    /// The factor beeing used for distances.
    /// </summary>
    public const double SimulationSizeFactor = 100;

    /// <summary>
    /// The simulation update speed
    /// </summary>
    public const double SimulationUpdateSpeed = 10;

    /// <summary>
    /// The simulation redraw speed
    /// </summary>
    public const double SimulationRedrawSpeed = 20;

    /// <summary>
    /// The traffic light toggle interval.
    /// </summary>
    public const int TrafficLightToggleInterval = 15;

    /// <summary>
    /// The target vehicle count.
    /// </summary>
    public const int TargetVehicleCount = 20;

    /// <summary>
    /// The tick rate
    /// </summary>
    public const int TickRate = 10;

    /// <summary>
    /// The tick step size
    /// </summary>
    public const double TickStepSize = 0.01;

    /// <summary>
    /// The own goup
    /// </summary>
    public const int OwnGoup = 2;

    /// <summary>
    /// The target group
    /// </summary>
    public const int TargetGroup = 2;
  }
}