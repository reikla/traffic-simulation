using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NLog;
using TrafficSimulation.Common;
using TrafficSimulation.Simulation.Contracts.Exceptions;
using TrafficSimulation.Simulation.Engine.Environment;

namespace TrafficSimulation.Simulation.Engine.Xml
{

  /// <summary>
  /// The XML Reader to desirialize the datamodel
  /// </summary>
  public class XmlGraphReader
  {
    private readonly List<INodeConnection> _nodeConnections;
    private readonly Dictionary<string, INode> _nodeDictionary;
    private readonly List<ITrafficLight> _trafficLights;

    /// <summary>
    /// The node Connections extracted from Xml
    /// </summary>
    public List<INodeConnection> NodeConnections => _nodeConnections;

    /// <summary>
    /// Gets the traffic lights.
    /// </summary>
    public List<ITrafficLight> TrafficLights => _trafficLights;

    /// <summary>
    /// Gets the nodes.
    /// </summary>
    public List<INode> Nodes => _nodeDictionary.Values.ToList();

    private static Logger Logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlGraphReader"/> class.
    /// </summary>
    public XmlGraphReader()
    {
      _nodeDictionary = new Dictionary<string, INode>();
      _nodeConnections = new List<INodeConnection>();
      _trafficLights = new List<ITrafficLight>();
    }

    /// <summary>
    /// reads the datamodel
    /// </summary>
    public void Read(string path)
    {
      try
      {
        var doc = XDocument.Load(path);

        var graphml = doc.Element(GraphMLNamespaces.nsGraphMl + "graphml");
        var graph = graphml.Element(GraphMLNamespaces.nsGraphMl + "graph");
        var nodes = graph.Descendants(GraphMLNamespaces.nsGraphMl + "node");
        foreach (var node in nodes)
        {
          var nodeInfo = XmlNodeReader.GetNode(node);
          var nodeLabel = node.Descendants(GraphMLNamespaces.nsY + "NodeLabel").First();
          nodeLabel.Value = nodeInfo.Item1;
          var hasText = nodeLabel.Attribute("hasText");
          if (hasText != null)
          {
            hasText.Value = "true";
          }
          _nodeDictionary.Add(nodeInfo.Item1, nodeInfo.Item2);
        }

        foreach (var edge in graph.Descendants(GraphMLNamespaces.nsGraphMl + "edge"))
        {
          var from = edge.Attribute("source").Value;
          var to = edge.Attribute("target").Value;
          var connection = new NodeConnection(_nodeDictionary[from], _nodeDictionary[to]);
          _nodeConnections.Add(connection);

          //handle traffic lights
          var arrow = edge.Descendants(GraphMLNamespaces.nsY + "Arrows").First();
          if (arrow.Attribute("target").Value == "white_delta")
          {
            _trafficLights.Add(new TrafficLight(connection));
          }
        }
        doc.Save(path);

        Normalize();
        _trafficLights.ForEach(x => ((TrafficLight)x).CalculatePosition());
      }

      //we expect the .graphml file to a fulfill a very special schema so otherwise we will throw an exception.
      catch (Exception e)
      {
        throw new XmlDeserializationException(Strings.Exception_Xml_Could_Not_Deserialize, e);
      }
    }

    /// <summary>
    /// Normalizes the coordinates of X and Y
    /// </summary>
    private void Normalize()
    {
      //we need to know the minimal value
      var minX = _nodeDictionary.Values.Min(x => x.X);
      var minY = _nodeDictionary.Values.Min(x => x.Y);

      //because we subtract it from the values later on we have to subtract it from the maximum value
      var xSpan = _nodeDictionary.Values.Max(x => x.X) - minX;
      var ySpan = _nodeDictionary.Values.Max(x => x.Y) - minY;

      foreach (var nsValue in _nodeDictionary.Values)
      {
        //we want the minimum value to be 0...
        nsValue.X -= minX;
        nsValue.Y -= minY;

        //...and the maximum value to be 1
        nsValue.X /= xSpan;
        nsValue.Y /= ySpan;

        //...so we are independed of the physical size of the network all the time
      }
    }
  }
}