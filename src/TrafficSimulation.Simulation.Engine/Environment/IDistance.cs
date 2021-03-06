﻿namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <summary>
  /// An interface to encapsulate the distance to the next <see cref="IPlaceable"/>
  /// </summary>
  public interface IDistance<T>
  {

    /// <summary>
    /// The distance in meters.
    /// </summary>
    double DistanceInMeters { get; set; }

    /// <summary>
    /// The placeable were the distance is measured to.
    /// </summary>
    T NextPlaceable { get; set; }
  }
}