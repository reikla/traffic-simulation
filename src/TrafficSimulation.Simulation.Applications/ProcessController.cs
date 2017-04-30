using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NLog;

namespace TrafficSimulation.Simulation.Applications
{
  class ProcessController : IController
  {
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private Process _process;
    private bool _forceKillOnExit;
    private string _processName;
    private string _relativePathToProcess;
    private string _startParameter;

    public ProcessController(bool forceKillOnExit, string processName, string relativePathToProcess, string startParameter)
    {
      _forceKillOnExit = forceKillOnExit;
      _processName = processName;
      _relativePathToProcess = relativePathToProcess;
      _startParameter = startParameter;
    }


    public void Start()
    {
      Logger.Trace($"Starting {GetType().Name}.");
      StartOrGetProcess();
    }

    public void Shutdown()
    {
      Logger.Trace($"Shutdown {GetType().Name}. ForceKillOnExit: {_forceKillOnExit}");
      if (_forceKillOnExit && _process != null && !_process.HasExited)
      {
        _process.Kill();
        _process = null;
      }
    }

    private void StartOrGetProcess()
    {
      var sentinelProcess = Process.GetProcessesByName(_processName);
      if (sentinelProcess.Length != 0)
      {
        _process = sentinelProcess[0];
      }
      else
      {
        var workingDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _relativePathToProcess);
        ProcessStartInfo psi =
          new ProcessStartInfo(Path.Combine(workingDir, $"{_processName}.exe"), _startParameter)
          {
            WorkingDirectory = workingDir,
            UseShellExecute = false,
            CreateNoWindow = false
          };
        _process = Process.Start(psi);
        
        //we wait until the process is started.
        Thread.Sleep(3000);
      }
    }
  }
}
