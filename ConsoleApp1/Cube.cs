using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Block : Model
    {

        public int TILE_SIZE = 2;
        public Block()
        {
            Vertices.Add(new Vertex(new Vector3(-1, -1, -1), Color.Cyan));
            Vertices.Add(new Vertex(new Vector3(1, -1, -1), Color.Cyan));
            Vertices.Add(new Vertex(new Vector3(1, 2, -1), Color.Cyan));
            Vertices.Add(new Vertex(new Vector3(-1, 2, -1), Color.Cyan));

            Vertices.Add(new Vertex(new Vector3(-1, -1, 1), Color.Cyan));
            Vertices.Add(new Vertex(new Vector3(1, -1, 1), Color.Cyan));
            Vertices.Add(new Vertex(new Vector3(1, 2, 1), Color.Cyan));
            Vertices.Add(new Vertex(new Vector3(-1, 2, 1), Color.Cyan));

            Triangles.Add(new Triangle(0, 1, 2));
            Triangles.Add(new Triangle(0, 2, 3));

            Triangles.Add(new Triangle(1, 5, 6));
            Triangles.Add(new Triangle(1, 6, 2));

            Triangles.Add(new Triangle(5, 4, 7));
            Triangles.Add(new Triangle(5, 7, 6));

            Triangles.Add(new Triangle(4, 0, 3));
            Triangles.Add(new Triangle(4, 3, 7));
        }
    }
}
