using TrafficSimulation.Simulation.Engine.Environment;

namespace TrafficSimulation.Simulation.Engine
{
  /// <summary>
  /// Interface to encapsulate a traffic light
  /// </summary>
  /// <seealso cref="TrafficSimulation.Simulation.Engine.Environment.IPlaceable" />
  internal interface ITrafficLight : IPlaceable
  {
    TrafficLightState TrafficLightState { get; set; }
  }
}