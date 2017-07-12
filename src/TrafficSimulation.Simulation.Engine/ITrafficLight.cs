using TrafficSimulation.Simulation.Engine.Environment;

namespace TrafficSimulation.Simulation.Engine
{
  /// <summary>
  /// Interface to encapsulate a traffic light
  /// </summary>
  /// <seealso cref="TrafficSimulation.Simulation.Engine.Environment.IPlaceable" />
  public interface ITrafficLight : IPlaceable
  {
    /// <summary>
    /// Gets or sets the state of the traffic light.
    /// </summary>
    TrafficLightState TrafficLightState { get; set; }

    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    int Id { get; set; }
  }

  /// <summary>
  /// Extension class to toggle a enum
  /// </summary>
  public static class TrafficLightStateExtension
  {
    /// <summary>
    /// Toggles an enum value
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public static TrafficLightState Toggle(this TrafficLightState state)
    {
      return (TrafficLightState)(((int)state + 1) % 3);
    }
  }
}