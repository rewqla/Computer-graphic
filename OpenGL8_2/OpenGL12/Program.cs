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
            public Game(int width, int height, GraphicsMode graphicsMode, string title, GameWindowFlags gameWindowFlags, DisplayDevice displayDevice) : base(width, height, graphicsMode, title, gameWindowFlags, displayDevice)
            {

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
                GL.Ortho(0, Width, Height, 0, -1, 1);
            }

            protected override void OnRenderFrame(FrameEventArgs e)
            {
                base.OnRenderFrame(e);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();

                GL.Color3(0.0f, 0.0f, 0.0f); // Чорний колір

                DrawStarFractal(Width / 2, Height / 2, Math.Max(Width, Height) / 4, 5);

                SwapBuffers();
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

            private void DrawSquare(float x, float y, float size)
            {
                float halfSize = size / 2;
                GL.Begin(PrimitiveType.LineLoop);
                GL.Vertex2(x - halfSize, y - halfSize);
                GL.Vertex2(x + halfSize, y - halfSize);
                GL.Vertex2(x + halfSize, y + halfSize);
                GL.Vertex2(x - halfSize, y + halfSize);
                GL.End();
            }

            private void DrawStarFractal(float x, float y, float size, int depth, int omittedSide = -1)
            {
                if (depth <= 0)
                    return;

                float halfSize = size / 2;

                // Draw main square
                DrawSquare(x, y, size);

                // Recursive calls for smaller squares at each corner
                for (int i = 0; i < 4; i++)
                {
                    if (i != omittedSide)
                    {
                        float offsetX = (i % 2 == 0) ? -halfSize : halfSize;
                        float offsetY = (i < 2) ? -halfSize : halfSize;

                        int nextOmittedSide = 3 - i; ;

                       DrawStarFractal(x + offsetX, y + offsetY, size / 2, depth - 1, nextOmittedSide);
                    }
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
