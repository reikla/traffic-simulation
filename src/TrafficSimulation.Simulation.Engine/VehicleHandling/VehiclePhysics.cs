using System;

namespace TrafficSimulation.Simulation.Engine.VehicleHandling
{


  /// <summary>
  /// The physics of a vehicle
  /// </summary>
  public class VehiclePhysics
  {
    /// <summary>
    /// ctor
    /// </summary>
    public VehiclePhysics()
    {
      MaxVelocity = new Random().Next(10,13);
      //MaxVelocity = 33.33;
    }

    /// <summary>
    /// Gets the maximum accelelration.
    /// </summary>
    public double MaxAccelelration => 4;

    /// <summary>
    /// Gets the maximum velocity.
    /// </summary>
    public double MaxVelocity { get; set; }

    /// <summary>
    /// Gets the length.
    /// </summary>
    public double Length => 4;

    /// <summary>
    /// the maximum deceleration of a vehicle
    /// </summary>
    public double MaxDeceleration => 1.5;
  }
}