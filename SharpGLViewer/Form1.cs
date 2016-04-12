using SharpGL;
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
        private float rotation = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;

            // Clear the screen and the depth buffer and resets the view
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            gl.Translate(0.0f, 0.0f, -15.0f);
            gl.Rotate(rotation, 9.0f, 8.0f, 5.0f);

            gl.Begin(OpenGL.GL_POINTS);

            for (var i = -5.0f; i < 5.0f; i += 0.1f)
            {
                for (var j = -5.0f; j < 5.0f; j += 0.1f)
                {
                    for (var k = -1.0f; k < 1.0f; k += 0.1f)
                    {
                        if (i < -3)
                        {
                            gl.Color(1.0f, 0.0f, 0.0f);	
                        }
                        else if (i > -3 && i < 2)
                        {
                            gl.Color(0.0f, 1.0f, 0.0f);	
                        }
                        else
                        {
                            gl.Color(0.0f, 0.0f, 1.0f);	
                        }

                        gl.Vertex(i, j, k);
                    }
                }

            }

            gl.End();
            gl.Flush();

            rotation += 3.0f;
        }

    }
}
