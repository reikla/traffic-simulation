using System;
using System.Collections.Generic;
using System.IO;
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
  internal class XmlGraphReader
  {
    private readonly List<NodeConnection> nodeConnections;
    private readonly Dictionary<string, INode> nodeDictionary;

    /// <summary>
    /// The node Connections extracted from Xml
    /// </summary>
    public List<NodeConnection> NodeConnections => nodeConnections;

    /// <summary>
    /// Gets the nodes.
    /// </summary>
    public List<INode> Nodes => nodeDictionary.Values.ToList();

    private static Logger Logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlGraphReader"/> class.
    /// </summary>
    public XmlGraphReader()
    {
      nodeDictionary = new Dictionary<string, INode>();
      nodeConnections = new List<NodeConnection>();
    }

    /// <summary>
    /// reads the datamodel
    /// </summary>
    public void Read()
    {
      try
      {
        var doc = XDocument.Load("Strassennetz.graphml");

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
          nodeDictionary.Add(nodeInfo.Item1, nodeInfo.Item2);
        }

        foreach (var edge in graph.Descendants(GraphMLNamespaces.nsGraphMl + "edge"))
        {
          var from = edge.Attribute("source").Value;
          var to = edge.Attribute("target").Value;
          var connection = new NodeConnection(nodeDictionary[from], nodeDictionary[to]);
          nodeConnections.Add(connection);
        }
        doc.Save("Strassennetz.graphml");

        Normalize();
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
      var minX = nodeDictionary.Values.Min(x => x.X);
      var minY = nodeDictionary.Values.Min(x => x.Y);

      //because we subtract it from the values later on we have to subtract it from the maximum value
      var xSpan = nodeDictionary.Values.Max(x => x.X) - minX;
      var ySpan = nodeDictionary.Values.Max(x => x.Y) - minY;

      foreach (var nsValue in nodeDictionary.Values)
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