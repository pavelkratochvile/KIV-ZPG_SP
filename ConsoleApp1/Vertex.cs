using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Vertex
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Vector3 Position { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Color Color { get; set; }

        public Vertex(Vector3 position, Color color)
        {
            Position = position;
            Color = color;
        }

        public override string ToString()
        {
            return $"[{Position.X}][{Position.Y}][{Position.Z}]";
        }

    }
}
