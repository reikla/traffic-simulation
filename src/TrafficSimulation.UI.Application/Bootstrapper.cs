using System;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Timers;
using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;
using System.Windows.Threading;
using NLog;
using TrafficSimulation.Common;
using TrafficSimulation.Simulation.Contracts;
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

    private Timer _drawTimer;
    private MainWindow _view;
    private ChannelFactory<ISimulationService> cf;
    private TrafficSimulationViewModel vm;

    protected override DependencyObject CreateShell()
    {
      _view =  Container.Resolve<MainWindow>();
      return _view;
    }



    protected override void InitializeShell()
    {
      vm = _view.DataContext as TrafficSimulationViewModel;
      _drawTimer = new Timer(Constants.SimulationRedrawSpeed);
      var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.Transport);
      var ep = new EndpointAddress("net.pipe://localhost/Simulation/Engine");
      cf = new ChannelFactory<ISimulationService>(binding, ep);
      _drawTimer.Elapsed += DrawTimer_Elapsed;
      vm.cf = cf;
      vm.DrawTimer = _drawTimer;
      System.Windows.Application.Current.MainWindow.Show();
      
    }

    
    private void DrawTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        System.Windows.Application.Current?.Dispatcher?.BeginInvoke(DispatcherPriority.Normal, new Action(() => _view.Draw()));
    }


  }
}