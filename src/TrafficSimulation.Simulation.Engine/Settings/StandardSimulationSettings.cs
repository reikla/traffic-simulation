using TrafficSimulation.Common;
using VehicleHandoverLibrary;

namespace TrafficSimulation.Simulation.Engine.Settings
{
  /// <summary>
  /// Quite slow simulation settings for debugging
  /// </summary>
  /// <seealso cref="ISimulationSettings" />
  internal class StandardSimulationSettings : ISimulationSettings
  {
    /// <summary>
    /// Gets or sets the tick rate in ms.
    /// </summary>
    public double TickRate { get; set; }
    /// <summary>
    /// Gets or sets the size of the tick step size in seconds (e.g 0.1 for 100ms).
    /// </summary>
    public double TickStepSize { get; set; }
    /// <summary>
    /// Gets or sets the target vehicle count. That is the total number of cars desired for the simulation
    /// </summary>
    public int TargetVehicleCount { get; set; }

    /// <summary>
    /// Gets or sets own goup.
    /// </summary>
    public Groups OwnGoup { get; set; }

    /// <summary>
    /// Gets or sets the target group.
    /// </summary>
    public Groups TargetGroup { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StandardSimulationSettings"/> class.
    /// </summary>
    public StandardSimulationSettings()
    {
      TickRate = Constants.TickRate;
      TickStepSize = Constants.TickStepSize;
      TargetVehicleCount = Constants.TargetVehicleCount;
      OwnGoup = (Groups) Constants.OwnGoup - 1;
      TargetGroup = (Groups) Constants.TargetGroup - 1;
    }
  }
}