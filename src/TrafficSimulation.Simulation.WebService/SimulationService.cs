using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.Simulation.Contracts.DTO;
using TrafficSimulation.Simulation.Engine;

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
      _engine.DataModel.Vehicles.ForEach(x => vehicles.Add(new Vehicle(x.Id, x.VehicleType, x.Position.NodeConnection.Id, x.Position.PositionOnConnection, x.Id.ToString())));
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
  }
}