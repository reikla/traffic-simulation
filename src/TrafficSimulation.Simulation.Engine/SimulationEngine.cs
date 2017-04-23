using System.Timers;
using NLog;
using TrafficSimulation.Common;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.Simulation.Contracts.Exceptions;
using TrafficSimulation.Simulation.Environment;
using TrafficSimulation.Simulation.Settings;
using TrafficSimulation.Simulation.SimulationObjects;

namespace TrafficSimulation.Simulation.Engine
{
  public class SimulationEngine : IEngine
  {
    private bool _isInitialized;
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private DataModel _dataModel = new DataModel();
    private Timer SimulationTimer;
    private SimulationSettings _settings; 


    public SimulationEngine()
    {
      _settings = new SlowSimulationSettings();
    }


    public void Start()
    {
      Logger.Trace("Starting Simulation");
      CheckOrThrowInitialization("Start()");
      if (SimulationTimer != null)
      {
        Logger.Error(Strings.Exception_Already_Started);
        throw new EngineStateException(Strings.Exception_Already_Started);
      }
      SimulationTimer = new Timer(_settings.TickRate);
      SimulationTimer.Elapsed += SimulationTimer_Elapsed;
      SimulationTimer.Start();
    }

    private void SimulationTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      DoStep();
    }

    public void Stop()
    {
      CheckOrThrowInitialization("Stop()");
      if (SimulationTimer == null)
      {
        Logger.Error(Strings.Exception_Already_Stopped);
        throw new EngineStateException(Strings.Exception_Already_Stopped);
      }
      Logger.Trace("Stopping Simulation");
      SimulationTimer.Stop();
      SimulationTimer = null;
    }

    public void Step()
    {
      CheckOrThrowInitialization("Step()");
      if (SimulationTimer != null)
      {
        Logger.Error(Strings.Exception_Cant_Step);
        throw new EngineStateException(Strings.Exception_Cant_Step);
      }
      Logger.Trace("Simulating a step.");
      DoStep();
    }

    private void DoStep()
    {
      Logger.Trace($"Do Step. Size: {_settings.TickStepSize}");
      foreach (var dataModelVehicle in _dataModel.Vehicles)
      {
        dataModelVehicle.Tick(_settings.TickStepSize);
      }
    }

    public void Init()
    {
      if (_isInitialized)
      {
        Logger.Error(Strings.Exception_Already_Initialized);
        throw new EngineInitializationException(Strings.Exception_Already_Initialized);
      }
      Logger.Trace("Init Simulation Engine");
      CreateNodes();
      CreateVehicle();

      _isInitialized = true;
    }

    private void CheckOrThrowInitialization(string method)
    {
      if (!_isInitialized)
      {
        throw new EngineInitializationException($"Can not execute {method}. Engine is not initialzied.");
      }
    }

    private void CreateNodes()
    {
      Logger.Trace("Creating Nodes");
      var startNode = new StartNode();
      var endNode = new EndNode();
      var node = new Node();

      var connection1 = new NodeConnection(startNode, node, 100);
      var connection2 = new NodeConnection(node, endNode, 100);

      _dataModel.Nodes.Add(startNode);
      _dataModel.Nodes.Add(node);
      _dataModel.Nodes.Add(endNode);

      _dataModel.NodeConnections.Add(connection1);
      _dataModel.NodeConnections.Add(connection2);
    }

    private void CreateVehicle()
    {
      Logger.Trace("Creating Vehicles");

      var vehicle = new Vehicle(VehicleType.Car);
      _dataModel.Vehicles.Add(vehicle);

    }
  }
}