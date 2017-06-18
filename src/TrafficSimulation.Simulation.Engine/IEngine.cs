namespace TrafficSimulation.Simulation.Engine
{
  /// <summary>
  /// The Simulation Engine. It is responsable for the control of the simulation
  /// </summary>
  public interface IEngine
  {
    /// <summary>
    /// Starts the simulation.
    /// </summary>
    void Start();
    /// <summary>
    /// Stops the simulation.
    /// </summary>
    void Stop();
    /// <summary>
    /// Simulates a single step.
    /// </summary>
    void Step();

    /// <summary>
    /// Initalizes the Simulation.
    /// </summary>
    void Init();

    /// <summary>
    /// Determines whether this instance is started.
    /// </summary>
    bool IsStarted();

    /// <summary>
    /// Sets the car defect.
    /// </summary>
    /// <param name="id">The identifier.</param>
    void SetCarDefect(int id);
  }
}
