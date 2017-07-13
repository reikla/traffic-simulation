namespace TrafficSimulation.Simulation.Engine
{
  /// <summary>
  /// Represents the state of a traffic light
  /// </summary>
  public enum TrafficLightState
  {
    /// <summary>
    /// Traffic light is red
    /// </summary>
    Red,
    /// <summary>
    /// Traffic light is green
    /// </summary>
    Green,
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
      return (TrafficLightState)(((int)state + 1) % 2);
    }
  }
}