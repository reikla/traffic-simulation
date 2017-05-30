using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using TrafficSimulation.Common;
using TrafficSimulation.Simulation.Contracts.Exceptions;
using TrafficSimulation.Simulation.Engine.Environment;

namespace TrafficSimulation.Simulation.Engine.Xml
{
  internal static class XmlNodeReader
  {
    public static Tuple<string, INode> GetNode(XElement element)
    {
      var id = element.Attribute("id").Value;

      //process node data
      var data = element.Descendants(GraphMLNamespaces.nsGraphMl + "data").First(att => att.HasElements);
      var shapeNode = data.Descendants(GraphMLNamespaces.nsY + "ShapeNode").First();
      var geometry = shapeNode.Element(GraphMLNamespaces.nsY + "Geometry");
      var x = geometry.Attribute("x").Value;
      var y = geometry.Attribute("y").Value;
      var shape = data.Descendants(GraphMLNamespaces.nsY + "Shape").First();
      var shapeType = shape.Attribute("type").Value;
      INode n = null;
      
      switch (shapeType)
      {
        case "star8":
          n = new Node(double.Parse(x, CultureInfo.InvariantCulture), double.Parse(y, CultureInfo.InvariantCulture), NodeType.Intersection);
          break;
        case "ellipse":
          n = new Node(double.Parse(x, CultureInfo.InvariantCulture), double.Parse(y, CultureInfo.InvariantCulture));
          break;
        default:
          throw new XmlDeserializationException(Strings.Exception_Xml_Node_Has_Unexpected_Type);
      }
      return new Tuple<string,INode>(id, n);
    }
  }
}