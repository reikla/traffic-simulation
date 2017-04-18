using System;

namespace Contracts
{
  /// <summary>
  /// The time context of a log message
  /// </summary>
  [Serializable]
  public class TimeContext
  {
    /// <summary>
    /// The time when the message was created.
    /// </summary>
    public DateTime Time { get; set; }
  }
}