using System.Diagnostics;
using System.Threading;

namespace TrafficSimulation.Simulation.Applications
{
  class LoggingController : IController
  {
    private Process _sentinelProcess;
    private bool _forceKillOnExit;

    public LoggingController(bool forceKillOnExit)
    {
      _forceKillOnExit = forceKillOnExit;
    }


    public void Start()
    {
      StartOrGetProcess();
    }

    public void Shutdown()
    {
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
