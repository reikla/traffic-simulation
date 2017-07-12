using System;
using TrafficSimulation.Simulation.Engine.Environment;

namespace TrafficSimulation.Simulation.Engine
{
  /// <summary>
  /// Implementation of a traffic light
  /// </summary>
  public class TrafficLight : ITrafficLight
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="TrafficLight"/> class.
    /// </summary>
    /// <param name="nodeConnection">The node connection on which end the traffic light is placed.</param>
    public TrafficLight(INodeConnection nodeConnection)
    {
      Position = new Position(nodeConnection);

      //we want the traffic light to be placed 2 meters before end node
      var posOfTrafficlightInMeters = nodeConnection.Length - 2;
      Position.PositionOnConnection = posOfTrafficlightInMeters / nodeConnection.Length;
    }

    /// <summary>
    /// The position
    /// </summary>
    public IPosition Position { get; set; }
    /// <summary>
    /// Signals if this placable is blocking the node connection where it lives on.
    /// </summary>
    public bool IsConnectionBlocking {
      get => TrafficLightState == TrafficLightState.Red;
      set => throw new Exception("We don't set this property!");
    }
    /// <summary>
    /// Gets or sets the state of the traffic light.
    /// </summary>
    public TrafficLightState TrafficLightState { get; set; }
  }
}