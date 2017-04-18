using System;
using Contracts;

namespace Server
{
  /// <summary>
  ///   Implementation von ILogService
  /// </summary>
  internal class LogService : ILogService
  {
    public void Log(string message)
    {
      var timestamp = DateTime.Now.ToString("O");
      Console.WriteLine($"{timestamp} - {message}");
    }

    public void LogAdvanced(LogMessage message)
    {
      var timestamp = message.TimeContext.Time.ToString("O");
      Console.WriteLine($"Adavanced: {timestamp} - {message.Message}");
    }
  }
}