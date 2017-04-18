using System;

namespace Contracts

{
  /// <summary>
  /// A log message including a time context object
  /// </summary>
  [Serializable]
  public class LogMessage
  {
    /// <summary>
    /// The message
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// The time context
    /// </summary>
    public TimeContext TimeContext { get; set; }

  }
}