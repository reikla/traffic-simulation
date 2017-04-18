using System;
using Contracts;

namespace Server
{
  /// <summary>
  ///   Implementation von ILogService
  /// </summary>
  public class LogService : ILogService
  {
    public void Log(string message)
    {
      var timestamp = DateTime.Now.ToString("O");
      Console.WriteLine($"{timestamp} - {message}");
    }
  }
}