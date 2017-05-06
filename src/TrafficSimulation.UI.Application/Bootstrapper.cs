﻿using System;
using System.ComponentModel;
using System.ServiceModel;
using System.Timers;
using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;
using System.Windows.Threading;
using Prism.Commands;
using TrafficSimulation.Common;
using TrafficSimulation.Simulation.Contracts;
using TrafficSimulation.UI.Application;
using TrafficSimulation.UI.Application.ViewModel;

namespace TrafficSimulation.UI.Application
{
  /// <summary>
  /// The Bootstrapper for the application: 
  /// Connects to the simulationService, updates the service via timer and re-draws via timer.
  /// Implements Prism.Unity.UnityBootstrapper, which is a base class that provides a basic bootstrapping sequence that registers most of
  ///  the Prism Library assets in a Microsoft.Practices.Unity.IUnityContainer.
  /// </summary>
  class Bootstrapper : UnityBootstrapper
  {

    private Timer serviceUpdateTimer;
    private Timer drawTimer;
    private ISimulationService simulationService;
    private MainWindow view;

    private TrafficSimulationViewModel vm;

  protected override DependencyObject CreateShell()
    {
      view =  Container.Resolve<MainWindow>();
      return view;
    }



    protected override void InitializeShell()
    {
      vm = view.DataContext as TrafficSimulationViewModel;
      serviceUpdateTimer = new Timer(Constants.SimulationUpdateSpeed);
      drawTimer = new Timer(Constants.SimulationRedrawSpeed);

      var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.Transport);
      var ep = new EndpointAddress("net.pipe://localhost/Simulation/Engine");
      simulationService = ChannelFactory<ISimulationService>.CreateChannel(binding, ep);
      serviceUpdateTimer.Elapsed += ServiceUpdateTimer_Elapsed;
      drawTimer.Elapsed += DrawTimer_Elapsed;
      serviceUpdateTimer.Start();
      drawTimer.Start();
     
      System.Windows.Application.Current.MainWindow.Show();
    }



    private void DrawTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => view.Draw()));
    }

    private void ServiceUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
    {

      lock (vm)
      {
        vm.Nodes.Clear();
        vm.Nodes.AddRange(simulationService.GetNodes());
        vm.NodeConnections.Clear();
        vm.NodeConnections.AddRange(simulationService.GetNodeConnections());
        vm.Vehicles.Clear();
        vm.Vehicles.AddRange(simulationService.GetVehicles());
        vm.SimulationService = simulationService;
      }
      
    }
  }
}