using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NLog;

namespace TrafficSimulation.Simulation.Applications
{
  /// <summary>
  /// The Baseclass of Process controllers. It is responsible to start external processes.
  /// </summary>
  /// <seealso cref="TrafficSimulation.Simulation.Applications.IController" />
  public class ProcessController : IController
  {
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    private Process _process;
    private readonly bool _forceKillOnExit;
    private readonly bool _createWindow;
    private readonly string _processName;
    private readonly string _relativePathToProcess;
    private readonly string _startParameter;


    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessController"/> class.
    /// </summary>
    /// <param name="forceKillOnExit">if set to <c>true</c> [force kill on exit].</param>
    /// <param name="processName">Name of the process.</param>
    /// <param name="relativePathToProcess">The relative path to exe.</param>
    /// <param name="startParameter">The start parameter.</param>
    /// <param name="createWindow">if set to <c>true</c> [create window].</param>
    protected ProcessController(bool forceKillOnExit, string processName, string relativePathToProcess, string startParameter, bool createWindow = false)
    {
      _forceKillOnExit = forceKillOnExit;
      _processName = processName;
      _relativePathToProcess = relativePathToProcess;
      _startParameter = startParameter;
      _createWindow = createWindow;
    }

    /// <inheritdoc />
    public void Start()
    {
      Logger.Trace($"Starting {GetType().Name}.");
      StartOrGetProcess();
    }

    /// <inheritdoc />
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
            UseShellExecute = !_createWindow,
            CreateNoWindow = !_createWindow
          };
        _process = Process.Start(psi);
        
        //we wait to make sure the process is started.
        Thread.Sleep(3000);
      }
    }
  }
}
