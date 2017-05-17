using System;
using System.Linq;
using System.Timers;
using NLog;
using TrafficSimulation.Common;
using TrafficSimulation.Simulation.Contracts.Exceptions;
using TrafficSimulation.Simulation.Engine.Settings;

namespace TrafficSimulation.Simulation.Engine
{
  /// <summary>
  /// The Main Class of the Simulation. Thats where all the magic happens.
  /// </summary>
  /// <seealso cref="TrafficSimulation.Simulation.Engine.IEngine" />
  public class SimulationEngine : IEngine
  {
    private bool _isInitialized;
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private DataModel _dataModel = new DataModel();
    private Timer _simulationTimer;
    private SimulationSettings _settings;
    private IDataModelInitializer _dataModelInitializer;
    private readonly Random _random;

    internal DataModel DataModel => _dataModel;


    /// <summary>
    /// Initializes a new instance of the <see cref="SimulationEngine"/> class.
    /// </summary>
    public SimulationEngine()
    {
      _settings = new SlowSimulationSettings();
      _random = new Random(1);
      _dataModelInitializer = new SimpleDataModelInitializer();
    }

    ///<inheritdoc />
    public void Start()
    {
      Logger.Trace("Starting Simulation");
      CheckOrThrowInitialization("Start()");
      if (_simulationTimer != null)
      {
        Logger.Error(Strings.Exception_Already_Started);
        throw new EngineStateException(Strings.Exception_Already_Started);
      }
      _simulationTimer = new Timer(_settings.TickRate);
      _simulationTimer.Elapsed += SimulationTimer_Elapsed;
      _simulationTimer.Start();
    }

    private void SimulationTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      DoStep();
    }

    ///<inheritdoc />
    public void Stop()
    {
      CheckOrThrowInitialization("Stop()");
      if (_simulationTimer == null)
      {
        Logger.Error(Strings.Exception_Already_Stopped);
        throw new EngineStateException(Strings.Exception_Already_Stopped);
      }
      Logger.Trace("Stopping Simulation");
      _simulationTimer.Stop();
      _simulationTimer = null;
    }

    ///<inheritdoc />
    public void Step()
    {
      CheckOrThrowInitialization("Step()");
      if (_simulationTimer != null)
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
      CheckVehicleAmount();
      foreach (var vehicle in _dataModel.Routes.SelectMany(x=>x.Vehicles))
      {
        vehicle.Tick(_settings.TickStepSize);
      }
    }

    private void CheckVehicleAmount()
    {
      if (DataModel.Routes.SelectMany(x => x.Vehicles).Count() < _settings.TargetVehicleCount && _random.NextDouble() > 0.8)
      {
          DataModel.Routes[_random.Next(0,DataModel.Routes.Count)].CreateVehicle(); // we randomly select a route
      }
    }

    ///<inheritdoc />
    public void Init()
    {
      if (_isInitialized)
      {
        Logger.Error(Strings.Exception_Already_Initialized);
        throw new EngineInitializationException(Strings.Exception_Already_Initialized);
      }
      Logger.Trace("Init Simulation Engine");
      _dataModelInitializer.Initialize(_dataModel);
      _isInitialized = true;
    }

    /// <inheritdoc />
    public bool IsStarted()
    {
      return _simulationTimer != null;
    }

    private void CheckOrThrowInitialization(string method)
    {
      if (!_isInitialized)
      {
        throw new EngineInitializationException($"Can not execute {method}. Engine is not initialzied.");
      }
    }
  }
}