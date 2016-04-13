using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.Version;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpGLViewer
{
    public partial class Form1 : Form
    {
        private Dictionary<int, List<Vertex>> verticesByGroup;
        private float rotation = 0;

        public Form1()
        {
            InitializeComponent();
            LoadVertices();
        }

        private void LoadVertices()
        {
            verticesByGroup = verticesByGroup ?? new Dictionary<int, List<Vertex>>();
            verticesByGroup.Clear();

            verticesByGroup.Add(1, new List<Vertex>());
            verticesByGroup.Add(2, new List<Vertex>());
            verticesByGroup.Add(3, new List<Vertex>());

            for (var i = -5.0f; i < 5.0f; i += 0.2f)
            {
                int group;

                if (i < -3) group = 1;
                else if (i >= -3 && i <= 2) group = 2;
                else group = 3;

                for (var j = -5.0f; j < 5.0f; j += 0.2f)
                {
                    for (var k = -1.0f; k < 1.0f; k += 0.2f)
                    {
                        verticesByGroup[group].Add(new Vertex(i, j, k));
                    }
                }
            }
        }

        private Tuple<float, float, float> GetGroupColor(int group)
        {
            switch (group)
            {
                case 1:
                    return new Tuple<float, float, float>(1.0f, 0.0f, 0.0f);
                case 2:
                    return new Tuple<float, float, float>(0.0f, 1.0f, 0.0f);
                case 3:
                    return new Tuple<float, float, float>(0.0f, 0.0f, 1.0f);
                default:
                    return new Tuple<float, float, float>(0.0f, 0.0f, 0.0f);
            }
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;

            // Clear the screen and the depth buffer and resets the view
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            gl.Translate(0.0f, 0.0f, -15.0f);
            gl.Rotate(rotation, 9.0f, 8.0f, 5.0f);
            gl.PointSize(1.0f);

            gl.Begin(OpenGL.GL_POINTS);

            foreach (var key in verticesByGroup.Keys)
            {
                var groupColor = GetGroupColor(key);
                gl.Color(groupColor.Item1, groupColor.Item2, groupColor.Item3);
                verticesByGroup[key].ForEach(coord => gl.Vertex(coord.X, coord.Y, coord.Z));
            }

            gl.End();
            gl.Flush();

            rotation += 3.0f;
        }
    }
}
