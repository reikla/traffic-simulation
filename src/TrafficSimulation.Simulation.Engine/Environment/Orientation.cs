using System;

namespace TrafficSimulation.Simulation.Engine.Environment
{
  /// <summary>
  /// The orientation of a Connection
  /// </summary>
  public enum Orientation
  {
    /// <summary>
    /// North
    /// </summary>
    North,
    /// <summary>
    /// East
    /// </summary>
    East,
    /// <summary>
    /// South
    /// </summary>
    South,
    /// <summary>
    /// West
    /// </summary>
    West
  }
  /// <summary>
  /// Class for extension Methods of Orientation
  /// </summary>
  public static class OrientationExtension
  {
    /// <summary>
    /// Gets the priority orientation.
    /// </summary>
    public static Orientation GetPriorityOrientation(this Orientation orientation)
    {
      switch (orientation)
      {
        case Orientation.North:
          return Orientation.West;
        case Orientation.East:
          return Orientation.North;
        case Orientation.South:
          return Orientation.East;
        case Orientation.West:
          return Orientation.South;
        default:
          throw new ArgumentException();
      }
    }
  }
}