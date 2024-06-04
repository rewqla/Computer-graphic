using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;

namespace OpenGL12
{
    internal class Program
    {
        public class Game : GameWindow
        {
            public Game(int width, int height, GraphicsMode graphicsMode, string title, GameWindowFlags gameWindowFlags, DisplayDevice displayDevice) : base(width, height, graphicsMode, title, gameWindowFlags, displayDevice) { }

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

                DrawEllipse(0.0f, 0.0f, 0.4f, 0.8f, Color4.Blue);

                SwapBuffers();
            }

            void DrawEllipse(float centerX, float centerY, float a, float b, Color4 color)
            {
                GL.Color4(color);
                GL.Begin(PrimitiveType.Points);

                for (float theta = 0; theta < 360; theta += 2f)
                {
                    float x = a * (float)Math.Cos(theta);
                    float y = b * (float)Math.Sin(theta);

                    GL.Vertex2(centerX + x, centerY + y);
                }

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