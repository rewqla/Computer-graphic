using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;

namespace OpenGL12
{
    public class Figure
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Scale { get; set; }
        public Color4 Color1 { get; set; }
        public Color4 Color2 { get; set; }
    }
    internal class Program
    {
        public class Game : GameWindow
        {
            readonly float x0 = 0.0f;
            readonly float y0 = 0.0f;
            readonly float scale = 0.8f;
            readonly float width = 5.0f;

            private List<Figure> figures = new List<Figure>();
            public Game(int width, int height, GraphicsMode graphicsMode, string title, GameWindowFlags gameWindowFlags, DisplayDevice displayDevice) : base(width, height, GraphicsMode.Default, title, GameWindowFlags.Default, displayDevice) { }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                Random random = new Random();

                for (int i = 0; i < 10; i++)
                {
                    figures.Add(new Figure
                    {
                        X = (float)random.NextDouble() * 2 - 1, //  -1 and 1
                        Y = (float)random.NextDouble() * 1.5f - 1, //  -1 and 0.5
                        Scale = (float)random.NextDouble() * 0.8f + 0.2f, // 0.2 and 1
                        Color1 = new Color4((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), 1.0f),
                        Color2 = new Color4((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), 1.0f),
                    });
                }

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

                Draw1();

                //Draw2();

                GL.End();
                SwapBuffers();
            }

            protected void Draw1()
            {
                foreach (var figure in figures)
                {
                    DrawFigure(figure);
                }
            }
            protected void Draw2()
            {
                List<Figure> figures = new List<Figure>()
                { 
                    new Figure
                    {
                        X = 0.0f,
                        Y = 0.0f,
                        Scale = 1.0f,
                        Color1 = new Color4(0.8f, 0.2f, 0.2f, 1.0f),
                        Color2 = new Color4(0.2f, 0.6f, 0.2f, 1.0f)
                    },
                    new Figure
                    {
                        X = 0.0f,
                        Y = 0.0f,
                        Scale = 0.7f,
                        Color1 = new Color4(0.2f, 0.2f, 0.8f, 1.0f),
                        Color2 = new Color4(0.6f, 0.2f, 0.2f, 1.0f)
                    },
                    new Figure
                    {
                        X = 0.0f,
                        Y = 0.0f,
                        Scale = 0.4f,
                        Color1 = new Color4(0.4f, 0.7f, 0.2f, 1.0f), 
                        Color2 = new Color4(0.7f, 0.2f, 0.5f, 1.0f)
                    },
               };

               foreach (var figure in figures)
               {
                   DrawFigure(figure);
               }

            }
            protected void DrawFigure(Figure figure)
            {
                GL.Begin(PrimitiveType.Lines);

                GL.Color4(figure.Color1);
                GL.Vertex2((0.1f * figure.Scale + figure.X) * scale + x0, (0.9f * figure.Scale + figure.Y) * scale + y0);
                GL.Vertex2((0.3f * figure.Scale + figure.X) * scale + x0, (-0.2f * figure.Scale + figure.Y) * scale + y0);

                GL.Color4(figure.Color1);
                GL.Vertex2((-0.4f * figure.Scale + figure.X) * scale + x0, (-0.2f * figure.Scale + figure.Y) * scale + y0);
                GL.Vertex2((0.3f * figure.Scale + figure.X) * scale + x0, (-0.2f * figure.Scale + figure.Y) * scale + y0);

                GL.Color4(figure.Color1);
                GL.Vertex2((0.3f * figure.Scale + figure.X) * scale + x0, (-0.2f * figure.Scale + figure.Y) * scale + y0);
                GL.Vertex2((0.7f * figure.Scale + figure.X) * scale + x0, (0.2f * figure.Scale + figure.Y) * scale + y0);

                GL.Color4(figure.Color2);
                GL.Vertex2((0.7f * figure.Scale + figure.X) * scale + x0, (0.2f * figure.Scale + figure.Y) * scale + y0);
                GL.Vertex2((-0f * figure.Scale + figure.X) * scale + x0, (0.2f * figure.Scale + figure.Y) * scale + y0);

                GL.Color4(figure.Color2);
                GL.Vertex2((-0f * figure.Scale + figure.X) * scale + x0, (0.2f * figure.Scale + figure.Y) * scale + y0);
                GL.Vertex2((-0.4f * figure.Scale + figure.X) * scale + x0, (-0.2f * figure.Scale + figure.Y) * scale + y0);

                GL.Color4(figure.Color1);
                GL.Vertex2((0.1f * figure.Scale + figure.X) * scale + x0, (0.9f * figure.Scale + figure.Y) * scale + y0);
                GL.Vertex2((-0.4f * figure.Scale + figure.X) * scale + x0, (-0.2f * figure.Scale + figure.Y) * scale + y0);

                GL.Color4(figure.Color2);
                GL.Vertex2((0.1f * figure.Scale + figure.X) * scale + x0, (0.9f * figure.Scale + figure.Y) * scale + y0);
                GL.Vertex2((-0f * figure.Scale + figure.X) * scale + x0, (0.2f * figure.Scale + figure.Y) * scale + y0);

                GL.Color4(figure.Color1);
                GL.Vertex2((0.1f * figure.Scale + figure.X) * scale + x0, (0.9f * figure.Scale + figure.Y) * scale + y0);
                GL.Vertex2((0.7f * figure.Scale + figure.X) * scale + x0, (0.2f * figure.Scale + figure.Y) * scale + y0);
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
