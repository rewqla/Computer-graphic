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
            readonly float x0 = 0.0f;
            readonly float y0 = 0.0f;
            readonly float scale = 0.8f;
            readonly float width = 8.0f;
            Color4 clvis = new Color4(0.8f, 0.2f, 0.2f, 1.0f); 
            Color4 clunvis = new Color4(0.2f, 0.6f, 0.2f, 1.0f);

            public Game(int width, int height, GraphicsMode graphicsMode, string title, GameWindowFlags gameWindowFlags, DisplayDevice displayDevice) : base(width, height, GraphicsMode.Default, title, GameWindowFlags.Default, displayDevice) { }
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

                float lineWidth = width;
                GL.LineWidth(lineWidth);

                GL.Begin(PrimitiveType.Lines);

                GL.Color4(clunvis);
                GL.Vertex2(0.1f * scale + x0, 0.9f * scale + y0);
                GL.Vertex2(0.3f * scale + x0, -0.2f * scale + y0);

                GL.Color4(clunvis);
                GL.Vertex2(-0.4f * scale + x0, -0.2f * scale + y0);
                GL.Vertex2(0.3f * scale + x0, -0.2f * scale + y0);

                GL.Color4(clunvis);
                GL.Vertex2(0.3f * scale + x0, -0.2f * scale + y0);
                GL.Vertex2(0.7f * scale + x0, 0.2f * scale + y0);

                GL.Color4(clvis);
                GL.Vertex2(0.7f * scale + x0, 0.2f * scale + y0);
                GL.Vertex2(-0f * scale + x0, 0.2f * scale + y0);

                GL.Color4(clvis);
                GL.Vertex2(-0f * scale + x0, 0.2f * scale + y0);
                GL.Vertex2(-0.4f * scale + x0, -0.2f * scale + y0);

                GL.Color4(clunvis);
                GL.Vertex2(0.1f * scale + x0, 0.9f * scale + y0);
                GL.Vertex2(-0.4f * scale + x0, -0.2f * scale + y0);

                GL.Color4(clvis);
                GL.Vertex2(0.1f * scale + x0, 0.9f * scale + y0);
                GL.Vertex2(-0f * scale + x0, 0.2f * scale + y0);

                GL.Color4(clunvis);
                GL.Vertex2(0.1f * scale + x0, 0.9f * scale + y0);
                GL.Vertex2(0.7f * scale + x0, 0.2f * scale + y0);

                GL.End();
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
