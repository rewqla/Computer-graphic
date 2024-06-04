using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;

namespace OpenGL12
{
    internal class Program
    {
        public class Game : GameWindow
        {
            private List<Vector3> _hermiteVertices;
            double[,] points = { { 1, 114 }, { 1.4, 118 }, { 1.8, 70 }, { 2.2, 18 }, { 2.6, 40 } };
            private int _vertexBufferObject;
            private int _vertexArrayObject;

            public Game(int width, int height, GraphicsMode graphicsMode, string title, GameWindowFlags gameWindowFlags, DisplayDevice displayDevice) : base(width, height, graphicsMode, title, gameWindowFlags, displayDevice)
            {

            }

            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                GL.ClearColor(Color4.Beige);
                GL.Enable(EnableCap.DepthTest);
                GL.MatrixMode(MatrixMode.Modelview);

                GenerateHermiteVertices(points);

                _vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, _hermiteVertices.Count * Vector3.SizeInBytes, _hermiteVertices.ToArray(), BufferUsageHint.StaticDraw);

                _vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(_vertexArrayObject);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.EnableVertexAttribArray(0);
            }

            protected override void OnResize(EventArgs e)
            {
                base.OnResize(e);
                GL.Viewport(0, 0, Width, Height);
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(-5, 5, 10, 100, -1, 1);
                //GL.Ortho(-5, 5, 10, 125, -1, 1);
            }

            protected override void OnRenderFrame(FrameEventArgs e)
            {
                base.OnRenderFrame(e);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                GL.BindVertexArray(_vertexArrayObject);

                GL.LineWidth(3.0f);
                GL.Color4(Color4.Red);

                GL.DrawArrays(PrimitiveType.Points, 0, _hermiteVertices.Count);

                GL.Color4(Color4.Firebrick);
                //GL.Begin(PrimitiveType.LineStrip);

                //for (int i = 0; i < points.GetLength(0); i++)
                //{
                //    GL.Vertex2(points[i, 0], points[i, 1]);
                //}

                GL.End();

                SwapBuffers();
            }
            private void GenerateHermiteVertices(double[,] points)
            {
                _hermiteVertices = new List<Vector3>();

                int numPoints = points.GetLength(0);

                for (int i = 0; i < numPoints - 1; i++)
                {
                    Vector3 p0 = new Vector3((float)points[i, 0], (float)points[i, 1], 0.0f);
                    Vector3 p1 = new Vector3((float)points[i + 1, 0], (float)points[i + 1, 1], 0.0f);

                    Vector3 t0, t1;
                    if (i == 0)
                    {
                        t0 = p1 - p0;
                        t1 = new Vector3((float)points[i + 2, 0], (float)points[i + 2, 1], 0.0f) - p0;
                    }
                    else if (i == numPoints - 2)
                    {
                        t0 = p1 - new Vector3((float)points[i - 1, 0], (float)points[i - 1, 1], 0.0f);
                        t1 = p1 - p0;
                    }
                    else
                    {
                        t0 = p1 - new Vector3((float)points[i - 1, 0], (float)points[i - 1, 1], 0.0f);
                        t1 = new Vector3((float)points[i + 2, 0], (float)points[i + 2, 1], 0.0f) - p0;
                    }

                    t0.Normalize();
                    t1.Normalize();

                    for (float u = 0.0f; u <= 1.0f; u += 0.02f)
                    {
                        float h00 = 2 * u * u * u - 3 * u * u + 1;
                        float h10 = -2 * u * u * u + 3 * u * u;
                        float h01 = u * u * (3 - 2 * u);
                        float h11 = u * u * (u - 1);

                        Vector3 point = h00 * p0 + h10 * p1 + h01 * t0 + h11 * t1;
                        _hermiteVertices.Add(point);
                    }
                }
            }

            protected override void OnUpdateFrame(FrameEventArgs e)
            {
                base.OnUpdateFrame(e);
                KeyboardState input = Keyboard.GetState();
                if (input.IsKeyDown(Key.Escape))
                {
                    Exit();
                }
            }
        }

        static void Main(string[] args)
        {
            using (Game game = new Game(900, 600, GraphicsMode.Default, "LearnOpenTK", GameWindowFlags.Default, DisplayDevice.Default))
            {
                game.Run(60.0);
            }
        }
    }
}
