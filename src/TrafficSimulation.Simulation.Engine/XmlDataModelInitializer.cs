using TrafficSimulation.Simulation.Engine.Xml;

namespace TrafficSimulation.Simulation.Engine
{
  /// <summary>
  /// 
  /// </summary>
  class XmlDataModelInitializer : IDataModelInitializer
  {

    /// <inheritdoc />
    public void Initialize(DataModel dataModel)
    {
      XmlGraphReader reader = new XmlGraphReader();
      reader.Read();

      dataModel.Nodes.AddRange(reader.Nodes);
      dataModel.NodeConnections.AddRange(reader.NodeConnections);
    }
  }
}