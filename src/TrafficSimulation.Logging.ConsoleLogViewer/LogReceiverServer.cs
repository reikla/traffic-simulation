using NLog;
using NLog.LogReceiverService;

namespace TrafficSimulation.Logging.ConsoleLogViewer
{
  public class LogReceiverServer : ILogReceiverServer
  {
    public void ProcessLogMessages(NLogEvents nevents)
    {
      var events = nevents.ToEventInfo();
      foreach (var eachEvent in events)
      {
        var logger = LogManager.GetLogger(eachEvent.LoggerName);
        logger.Log(eachEvent);
      }
    }
  }
}