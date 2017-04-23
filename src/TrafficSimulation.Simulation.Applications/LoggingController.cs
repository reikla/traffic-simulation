using System.Diagnostics;
using System.Threading;
using NLog;

namespace TrafficSimulation.Simulation.Applications
{
  class LoggingController : IController
  {
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private Process _sentinelProcess;
    private bool _forceKillOnExit;

    public LoggingController(bool forceKillOnExit)
    {
      _forceKillOnExit = forceKillOnExit;
    }


    public void Start()
    {
      Logger.Trace("Starting LoggingController");
      StartOrGetProcess();
    }

    public void Shutdown()
    {
      Logger.Trace($"Shutdown Logging Controller. ForceKillOnExit: {_forceKillOnExit}");
      if (_forceKillOnExit)
      {
        _sentinelProcess.Kill();
      }
    }

    private void StartOrGetProcess()
    {
      var sentinelProcess = Process.GetProcessesByName("Sentinel");
      if (sentinelProcess.Length != 0)
      {
        _sentinelProcess = sentinelProcess[0];
      }
      else
      {
        ProcessStartInfo psi = new ProcessStartInfo(@"Sentinel\Sentinel.exe", @"Sentinel\TrafficSimulation.sntl");
        _sentinelProcess = Process.Start(psi);
        
        //we wait until the process is started.
        Thread.Sleep(3000);
      }
    }
  }
}
