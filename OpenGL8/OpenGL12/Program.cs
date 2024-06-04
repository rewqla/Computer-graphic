using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;

namespace OpenGL12
{

    internal class Program
    {
        public class Game : GameWindow
        {
            private double[,] plasmaFractal;
            private Random rnd = new Random();
            private int width = 900;
            private int height = 600;
            public Game(int width, int height, GraphicsMode graphicsMode, string title, GameWindowFlags gameWindowFlags, DisplayDevice displayDevice) : base(width, height, graphicsMode, title, gameWindowFlags, displayDevice)
            {
                plasmaFractal = GeneratePlasmaFractal(width, height, 1.0);
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
                GL.Enable(EnableCap.DepthTest);
                GL.MatrixMode(MatrixMode.Modelview);
            }

            protected override void OnResize(EventArgs e)
            {
                base.OnResize(e);

                GL.Viewport(0, 0, Width, Height);
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
            }

            protected override void OnRenderFrame(FrameEventArgs e)
            {
                base.OnRenderFrame(e);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();

                GL.Begin(PrimitiveType.Points);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        double posX = Map(x, 0, width, -1, 1);
                        double posY = Map(y, 0, height, -1, 1);

                        double value = plasmaFractal[x, y];
                        GL.Color3(value, value, value);
                        GL.Vertex2(posX, posY);

                    }
                }

                GL.End();

                SwapBuffers();
            }
            // Method to map a value from one range to another
            private double Map(double value, double fromSource, double toSource, double fromTarget, double toTarget)
            {
                return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
            }
            private double[,] GeneratePlasmaFractal(int width, int height, double roughness)
            {
                double c1, c2, c3, c4;
                double[,] points = new double[width + 1, height + 1];

                // Assign the four corners of the initial grid random color values
                c1 = rnd.NextDouble();
                c2 = rnd.NextDouble();
                c3 = rnd.NextDouble();
                c4 = rnd.NextDouble();
                // Recursively generate plasma fractal
                DivideGrid(ref points, 0, 0, width, height, c1, c2, c3, c4, roughness);

                return points;
            }
            // Method to recursively divide grid for plasma fractal generation
            private void DivideGrid(ref double[,] points, double x, double y, double width, double height, double c1, double c2, double c3, double c4, double roughness)
            {
                double Edge1, Edge2, Edge3, Edge4, Middle;

                double newWidth = Math.Floor(width / 2);
                double newHeight = Math.Floor(height / 2);

                if (width > 1 || height > 1)
                {
                    Middle = ((c1 + c2 + c3 + c4) / 4) + Displace(newWidth + newHeight, roughness); // Randomly displace the midpoint!

                    Edge1 = ((c1 + c2) / 2); // Calculate the edges by averaging the two corners of each edge.
                    Edge2 = ((c2 + c3) / 2);
                    Edge3 = ((c3 + c4) / 2);
                    Edge4 = ((c4 + c1) / 2);

                    // Make sure that the midpoint doesn't accidentally "randomly displaced" past the boundaries!
                    Middle = Rectify(Middle);
                    Edge1 = Rectify(Edge1);
                    Edge2 = Rectify(Edge2);
                    Edge3 = Rectify(Edge3);
                    Edge4 = Rectify(Edge4);

                    // Do the operation over again for each of the four new grids.
                    DivideGrid(ref points, x, y, newWidth, newHeight, c1, Edge1, Middle, Edge4, roughness);
                    DivideGrid(ref points, x + newWidth, y, width - newWidth, newHeight, Edge1, c2, Edge2, Middle, roughness);
                    DivideGrid(ref points, x + newWidth, y + newHeight, width - newWidth, height - newHeight, Middle, Edge2, c3, Edge3, roughness);
                    DivideGrid(ref points, x, y + newHeight, newWidth, height - newHeight, Edge4, Middle, Edge3, c4, roughness);
                }
                else // This is the "base case," where each grid piece is less than the size of a pixel.
                {
                    // The four corners of the grid piece will be averaged and drawn as a single pixel.
                    double c = (c1 + c2 + c3 + c4) / 4;
                    points[(int)(x), (int)(y)] = c;
                    if (width == 2)
                    {
                        points[(int)(x + 1), (int)(y)] = c;
                    }
                    if (height == 2)
                    {
                        points[(int)(x), (int)(y + 1)] = c;
                    }
                    if ((width == 2) && (height == 2))
                    {
                        points[(int)(x + 1), (int)(y + 1)] = c;
                    }
                }
            }
            // Method to ensure value stays within bounds [0, 1]
            private double Rectify(double num)
            {
                if (num < 0)
                {
                    num = 0;
                }
                else if (num > 1.0)
                {
                    num = 1.0;
                }
                return num;
            }

            private double Displace(double SmallSize, double roughness)
            {
                double Max = SmallSize / (width + height) * roughness;
                return (rnd.NextDouble() - 0.5) * Max;
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
