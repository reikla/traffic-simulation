namespace TrafficSimulation.Simulation.Applications
{
  /// <summary>
  /// The controller of a part of the application
  /// </summary>
  internal interface IController
  {
    /// <summary>
    /// Starts a part of the application
    /// </summary>
    void Start();
    /// <summary>
    /// Stops a part of the Application
    /// </summary>
    void Shutdown();
  }
}