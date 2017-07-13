namespace TrafficSimulation.Common
{
  /// <summary>
  /// A collection of common shared Strings
  /// </summary>
  public static class Strings
  {
    /// <summary>
    /// Exception Message that is shown when a simulation is already started.
    /// </summary>
    public static readonly string Exception_Already_Started = "Can't start simulation. Its already started.";

    /// <summary>
    /// The simulation already stopped
    /// </summary>
    public static readonly string Exception_Already_Stopped = "Can't stop Simulation because its not running.";

    /// <summary>
    /// The Simulation cant step cant step
    /// </summary>
    public static readonly string Exception_Cant_Step = "Can't single step because its allready running";

    /// <summary>
    /// The simulation is already initialized
    /// </summary>
    public static readonly string Exception_Already_Initialized = "Cant init engine, its already initialized";

    /// <summary>
    /// The placable is not on route
    /// </summary>
    public static readonly string Exception_Placable_Not_On_Route = "The placable {0} is not on the route";

    /// <summary>
    /// Could not deserialize GraphXml
    /// </summary>
    public static readonly string Exception_Xml_Could_Not_Deserialize = "Could not deserialize the GraphXml.";

    /// <summary>
    /// XML node contains no data.
    /// </summary>
    public static readonly string Exception_Xml_Node_Contains_No_Data = "The Node {0} contains no data.";

    /// <summary>
    /// XML node has unexpected type
    /// </summary>
    public static readonly string Exception_Xml_Node_Has_Unexpected_Type = "The Node {0} has an unexpected format.";
  }
}
