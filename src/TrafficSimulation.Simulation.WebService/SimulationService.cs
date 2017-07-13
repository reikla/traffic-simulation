using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.Simulation.Contracts.DTO;
using TrafficSimulation.Simulation.Engine;
using TrafficSimulation.Simulation.Engine.Debugging;
using TrafficLight = TrafficSimulation.Simulation.Contracts.DTO.TrafficLight;
using TrafficLightState = TrafficSimulation.Simulation.Contracts.DTO.TrafficLightState;

namespace TrafficSimulation.Simulation.WebService
{
  /// <summary>
  /// The Webservice of the Simulation
  /// </summary>
  /// <seealso cref="TrafficSimulation.Simulation.Contracts.ISimulationService" />
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
  public class SimulationService : ISimulationService
  {
    private readonly SimulationEngine _engine;

    /// <summary>
    /// Initializes a new instance of the <see cref="SimulationService"/> class.
    /// </summary>
    public SimulationService()
    {
      _engine = new SimulationEngine();
      _engine.Init();
    }

    /// <inheritdoc />
    public void Start()
    {
      _engine.Start();
    }

    /// <inheritdoc />
    public void Stop()
    {
      _engine.Stop();
    }

    /// <inheritdoc />
    public void Step()
    {
      _engine.Step();
    }

    /// <inheritdoc />
    public ReadOnlyCollection<Node> GetNodes()
    {
      var nodes = new List<Node>();
      _engine.DataModel.Nodes.ForEach(x => nodes.Add(new Node(x.Id) { X = x.X, Y = x.Y }));
      return new ReadOnlyCollection<Node>(nodes);
    }

    /// <inheritdoc />
    public List<Vehicle> GetVehicles()
    {
      var vehicles = new List<Vehicle>();
      _engine.DataModel.Vehicles.ForEach(x => vehicles.Add(new Vehicle(x.Id, x.VehicleType, x.Position.NodeConnection.Id, x.Position.PositionOnConnection,x.IsForeignVehicle, DebugPrinter.PrintDebug(x))));
      return vehicles;
    }

    /// <inheritdoc />
    public ReadOnlyCollection<NodeConnection> GetNodeConnections()
    {
      var connections = new List<NodeConnection>();
      _engine.DataModel.NodeConnections.ForEach(
        x => connections.Add(new NodeConnection(x.Id, x.StartNode.Id, x.EndNode.Id, x.Length)));
      return new ReadOnlyCollection<NodeConnection>(connections);
    }

    /// <inheritdoc />
    public bool IsStarted()
    {
      return _engine.IsStarted();
    }

    /// <inheritdoc />
    public ReadOnlyCollection<TrafficLight> GetTrafficLights()
    {
      var trafficLights = _engine.DataModel.TrafficLights.Select(trafficLight => 
        new TrafficLight(trafficLight.Id, 
          ConvertTrafficLightState(trafficLight.TrafficLightState), 
          trafficLight.Position.NodeConnection.Id, 
          trafficLight.Position.PositionOnConnection)).ToList();
      return new ReadOnlyCollection<TrafficLight>(trafficLights);
    }

    private static TrafficLightState ConvertTrafficLightState(Engine.TrafficLightState state)
    {
      switch (state)
      {
        case Engine.TrafficLightState.Red:
          return TrafficLightState.Red;
        case Engine.TrafficLightState.Green:
          return TrafficLightState.Green;
        default:
          throw new ArgumentException();
      }
    }

    /// <inheritdoc />
    public void SetCarDefect(int id, bool isDefect)
    {
      _engine.SetCarDefect(id);
    }
    /// <inheritdoc />
    public void ToggleTrafficLight(int id)
    {
      var trafficLight = _engine.DataModel.TrafficLights.First(x => x.Id == id);
      trafficLight.TrafficLightState = trafficLight.TrafficLightState.Toggle();
    }
  }
}