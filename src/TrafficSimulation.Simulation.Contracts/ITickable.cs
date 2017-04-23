namespace TrafficSimulation.Simulation.Contracts
{
  /// <summary>
  /// The interface of all objects int the simulation that changes within a tick.
  /// </summary>
  public interface ITickable
  {
    /// <summary>
    /// A single tick
    /// </summary>
    /// <param name="timespan">the amount of time the simulation advances in seconds.</param>
    void Tick(double timespan);
  }
}