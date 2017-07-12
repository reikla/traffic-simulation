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
  }
}