using System.ServiceModel;

namespace Contracts
{
  /// <summary>
  ///   Service Contract für über WCF bereitgestellten Service.
  /// </summary>
  [ServiceContract]
  public interface ILogService
  {
    /// <summary>
    ///   Methode zum Loggen
    /// </summary>
    /// <param name="message">Nachricht die geloggt werden soll.</param>
    [OperationContract]
    void Log(string message);

    /// <summary>
    /// Log an advanced message including serialization.
    /// </summary>
    /// <param name="message">The message</param>
    [OperationContract]
    void LogAdvanced(LogMessage message);
  }
}