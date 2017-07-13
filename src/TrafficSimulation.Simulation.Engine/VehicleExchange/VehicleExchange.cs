using System;
using System.Collections.Generic;
using NLog;
using TrafficSimulation.Simulation.Engine.Settings;
using VehicleHandoverLibrary;

namespace TrafficSimulation.Simulation.Engine.VehicleExchange
{
  class VehicleExchange : IVehicleExchange
  {
    private readonly VehicleReceiver _vehicleReceiver;
    private readonly VehicleSender _vehicleSender;
    private readonly Queue<IVehicle> _receivedVehicles;
    private readonly ISimulationSettings _simulationSettings;

    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


    public VehicleExchange(ISimulationSettings settings)
    {
      _vehicleReceiver = new VehicleReceiver(settings.OwnGoup);
      _vehicleSender = new VehicleSender(settings.TargetGroup);
      _receivedVehicles = new Queue<IVehicle>();
      _simulationSettings = settings;
    }

    private void VehicleReceiver_ReceiveEventHandler(object sender, VehicleEventArgs e)
    {
      Logger.Info("Received a Vehicle from an other group.");
      _receivedVehicles.Enqueue(VehicleConverter.Convert(e.Vehicle));
    }

    public void SendVehicleAway(IVehicle vehicle)
    {
      Logger.Info($"Send Vehicle {vehicle.Id} away to {_simulationSettings.TargetGroup}.");
      var vehicleToSend = VehicleConverter.Convert(vehicle);
      try
      {
        _vehicleSender.PushVehicle(vehicleToSend);
      }
      catch (Exception e)
      {
        Logger.Error(e, $"Could not send vehicle {vehicle.Id} away to {_simulationSettings.TargetGroup}"); 
      }
    }

    public IVehicle ReceiveVehicle()
    {
      Logger.Debug($"Dequeue Vehicle. Currently {_receivedVehicles.Count} vehicles in queue.");
      return _receivedVehicles.Count != 0 ? _receivedVehicles.Dequeue() : null;
    }

    public void Enable()
    {
      _vehicleReceiver.ReceiveEventHandler += VehicleReceiver_ReceiveEventHandler;
    }

    public void Disable()
    {
      _vehicleReceiver.ReceiveEventHandler -= VehicleReceiver_ReceiveEventHandler;
      _receivedVehicles.Clear();
    }
  }
}