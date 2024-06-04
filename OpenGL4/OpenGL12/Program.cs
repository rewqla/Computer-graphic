using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing.Drawing2D;

namespace OpenGL4
{
    internal class Program
    {
        public class Game : GameWindow
        {
            double theta = 0;
            static float sqrt3over2 = (float)(Math.Sqrt(3) / 2);
            public Game(int width, int height, GraphicsMode graphicsMode, string title, GameWindowFlags gameWindowFlags, DisplayDevice displayDevice) : base(width, height, graphicsMode, title, gameWindowFlags, displayDevice)
            {

            }

            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                GL.ClearColor(Color4.White);
                GL.Enable(EnableCap.DepthTest);
            }

            protected override void OnResize(EventArgs e)
            {
                base.OnResize(e);
                GL.Viewport(0, 0, Width, Height);
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();

                Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), (float)Width / (float)Height, 1.0f, 100.0f);
                GL.LoadMatrix(ref perspective);

                GL.MatrixMode(MatrixMode.Modelview);
            }
            protected override void OnRenderFrame(FrameEventArgs e)
            {
                base.OnRenderFrame(e);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();

                GL.Translate(0, 0, -10.0);
                GL.Rotate(theta, 1.0, 1.0, 1.0);

                GL.LineWidth(3f);
                GL.Begin(PrimitiveType.Lines);

                //DrawBase();
                //DrawBase2();
                //DrawBase2_2();
                DrawBase3();

                SwapBuffers();

                theta += 1;

                if (theta == 360)
                    theta = 0;
            }

            private void DrawBase()
            {
                //нижня грань зад
                if (theta >= 270)
                    GL.Color4(Color4.Beige);
                else
                    GL.Color4(Color4.Red);

                GL.Vertex3(1, 0, 0);
                GL.Vertex3(0.5, sqrt3over2, 0);


                GL.Vertex3(0.5, sqrt3over2, 0);
                GL.Vertex3(-0.5, sqrt3over2, 0);

                GL.Vertex3(-0.5, sqrt3over2, 0);
                GL.Vertex3(-1, 0, 0);

                //нижня грань перед
                if (theta <= 90)
                    GL.Color4(Color4.Beige);
                else
                    GL.Color4(Color4.Red);


                GL.Vertex3(-1, 0, 0);
                GL.Vertex3(-0.5, -sqrt3over2, 0);

                GL.Vertex3(-0.5, -sqrt3over2, 0);
                GL.Vertex3(0.5, -sqrt3over2, 0);

                GL.Vertex3(0.5, -sqrt3over2, 0);
                GL.Vertex3(1, 0, 0);

                //--------------------------------

                //бокові зад
                if (theta >= 180)
                    GL.Color4(Color4.Beige);
                else
                    GL.Color4(Color4.Red);

                GL.Vertex3(0.5, sqrt3over2, 3);
                GL.Vertex3(0.5, sqrt3over2, 0);

                GL.Vertex3(-0.5, sqrt3over2, 0);
                GL.Vertex3(-0.5, sqrt3over2, 3);

                //бокові боки
                GL.Color4(Color4.Red);

                GL.Vertex3(-1, 0, 0);
                GL.Vertex3(-1, 0, 3);

                GL.Vertex3(1, 0, 0);
                GL.Vertex3(1, 0, 3);

                //бокові переди
                if (theta <= 180)
                    GL.Color4(Color4.Beige);
                else
                    GL.Color4(Color4.Red);

                GL.Vertex3(0.5, -sqrt3over2, 0);
                GL.Vertex3(0.5, -sqrt3over2, 3);

                GL.Vertex3(-0.5, -sqrt3over2, 0);
                GL.Vertex3(-0.5, -sqrt3over2, 3);

                //--------------------------------

                //верхня грань зад
                if (theta >= 180 && theta <= 300)
                    GL.Color4(Color4.Beige);
                else
                    GL.Color4(Color4.Red);

                GL.Vertex3(1, 0, 3);
                GL.Vertex3(0.5, sqrt3over2, 3);

                GL.Vertex3(0.5, sqrt3over2, 3);
                GL.Vertex3(-0.5, sqrt3over2, 3);

                GL.Vertex3(-0.5, sqrt3over2, 3);
                GL.Vertex3(-1, 0, 3);

                //верхня грань перед
                if (theta >= 60 && theta <= 180)
                    GL.Color4(Color4.Beige);
                else
                    GL.Color4(Color4.Red);

                GL.Vertex3(-1, 0, 3);
                GL.Vertex3(-0.5, -sqrt3over2, 3);

                GL.Vertex3(-0.5, -sqrt3over2, 3);
                GL.Vertex3(0.5, -sqrt3over2, 3);

                GL.Vertex3(0.5, -sqrt3over2, 3);
                GL.Vertex3(1, 0, 3);

                GL.End();
            }
            private void DrawBase2()
            {
                GL.Color4(Color4.Red);

                GL.Vertex3(1, 0, 0);
                GL.Vertex3(0.5, sqrt3over2, 0);


                GL.Vertex3(0.5, sqrt3over2, 0);
                GL.Vertex3(-0.5, sqrt3over2, 0);

                GL.Vertex3(-0.5, sqrt3over2, 0);
                GL.Vertex3(-1, 0, 0);


                GL.Vertex3(-1, 0, 0);
                GL.Vertex3(-0.5, -sqrt3over2, 0);

                GL.Vertex3(-0.5, -sqrt3over2, 0);
                GL.Vertex3(0.5, -sqrt3over2, 0);

                GL.Vertex3(0.5, -sqrt3over2, 0);
                GL.Vertex3(1, 0, 0);

                GL.End();
            }
            private void DrawBase2_2()
            {
                GL.Color4(Color4.Red);

                GL.Begin(PrimitiveType.Polygon);

                GL.Vertex3(-1, 0, 0);
                GL.Vertex3(-0.5, sqrt3over2, 0);
                GL.Vertex3(0.5, sqrt3over2, 0);
                GL.Vertex3(1, 0, 0);
                GL.Vertex3(0.5, -sqrt3over2, 0);
                GL.Vertex3(-0.5, -sqrt3over2, 0);

                GL.End();
            }
            private void DrawBase3()
            {
                GL.Color4(Color4.RosyBrown);

                // нижня грань 
                GL.Begin(PrimitiveType.Polygon);
                GL.Vertex3(-1, 0, 0);
                GL.Vertex3(-0.5, sqrt3over2, 0);
                GL.Vertex3(0.5, sqrt3over2, 0);
                GL.Vertex3(1, 0, 0);
                GL.Vertex3(0.5, -sqrt3over2, 0);
                GL.Vertex3(-0.5, -sqrt3over2, 0);
                GL.End();

                //--------------+
                GL.Begin(PrimitiveType.Polygon);

                GL.Vertex3(-1, 0, 0);
                GL.Vertex3(-0.5, sqrt3over2, 0);
                GL.Vertex3(-0.5, sqrt3over2, 3);
                GL.Vertex3(-1, 0, 3);

                GL.End();

                //--------------+
                GL.Begin(PrimitiveType.Polygon);

                GL.Vertex3(-0.5, sqrt3over2, 0);
                GL.Vertex3(0.5, sqrt3over2, 0);
                GL.Vertex3(0.5, sqrt3over2, 3);
                GL.Vertex3(-0.5, sqrt3over2, 3);

                GL.End();

                //--------------+
                GL.Begin(PrimitiveType.Polygon);

                GL.Vertex3(0.5, sqrt3over2, 0);
                GL.Vertex3(1, 0, 0);
                GL.Vertex3(1, 0, 3);
                GL.Vertex3(0.5, sqrt3over2, 3);

                GL.End();

                //--------------+
                GL.Begin(PrimitiveType.Polygon);

                GL.Vertex3(1, 0, 0);
                GL.Vertex3(0.5, -sqrt3over2, 0);
                GL.Vertex3(0.5, -sqrt3over2, 3);
                GL.Vertex3(1, 0, 3);

                GL.End();

                //--------------+
                GL.Begin(PrimitiveType.Polygon);

                GL.Vertex3(0.5, -sqrt3over2, 0);
                GL.Vertex3(-0.5, -sqrt3over2, 0);
                GL.Vertex3(-0.5, -sqrt3over2, 3);
                GL.Vertex3(0.5, -sqrt3over2, 3);

                GL.End();

                //--------------+
                GL.Begin(PrimitiveType.Polygon);

                GL.Vertex3(-1, 0, 0);
                GL.Vertex3(-0.5, -sqrt3over2, 0);
                GL.Vertex3(-0.5, -sqrt3over2, 3);
                GL.Vertex3(-1, 0, 3);

                GL.End();

                GL.Begin(PrimitiveType.Polygon);
                // верхня грань 
                GL.Vertex3(-1, 0, 3);
                GL.Vertex3(-0.5, sqrt3over2, 3);
                GL.Vertex3(0.5, sqrt3over2, 3);
                GL.Vertex3(1, 0, 3);
                GL.Vertex3(0.5, -sqrt3over2, 3);
                GL.Vertex3(-0.5, -sqrt3over2, 3);
                GL.End();
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
