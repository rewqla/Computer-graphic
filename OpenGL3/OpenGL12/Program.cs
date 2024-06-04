using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Threading;

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
            float speedX = 0.01f;
            float speedY = 0.01f;
            float scaleDelta = 0.005f;

            readonly float lineWidth = 5.0f;

            bool isOne = false;
            public Figure figure1 = new Figure
            {
                X = 0.0f,
                Y = 0.0f,
                Scale = 1.0f,
                Color1 = new Color4(0.2f, 0.6f, 0.2f, 1.0f),
                Color2 = new Color4(0.8f, 0.2f, 0.2f, 1.0f),
            };

            int angle = 180;
            float a = 0.6f;
            float b = 0.8f;
            float R = 0.01f;
            public Figure figure2 = new Figure
            {
                X = -0.8f,
                Y = 0.0f,
                Scale = 1.0f,
                Color1 = new Color4(0.2f, 0.6f, 0.2f, 1.0f),
                Color2 = new Color4(0.8f, 0.2f, 0.2f, 1.0f),
            };
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

                GL.LineWidth(lineWidth);

                //Draw1();
                Draw2();

                GL.End();
                SwapBuffers();
            }

            protected void Draw1()
            {
                isOne = true;

                DrawFigure(figure1);
            }
            protected void Draw2()
            {
                isOne = false;

                DrawFigure(figure2);
            }
            protected void DrawFigure(Figure figure)
            {
                GL.Begin(PrimitiveType.Lines);

                GL.Color4(figure.Color1);
                GL.Vertex2(0.1f * figure.Scale + figure.X, 0.9f * figure.Scale + figure.Y);
                GL.Vertex2(0.3f * figure.Scale + figure.X, -0.2f * figure.Scale + figure.Y);

                GL.Color4(figure.Color1);
                GL.Vertex2(-0.4f * figure.Scale + figure.X, -0.2f * figure.Scale + figure.Y);
                GL.Vertex2(0.3f * figure.Scale + figure.X, -0.2f * figure.Scale + figure.Y);

                GL.Color4(figure.Color1);
                GL.Vertex2(0.3f * figure.Scale + figure.X, -0.2f * figure.Scale + figure.Y);
                GL.Vertex2(0.7f * figure.Scale + figure.X, 0.2f * figure.Scale + figure.Y);

                GL.Color4(figure.Color2);
                GL.Vertex2(0.7f * figure.Scale + figure.X, 0.2f * figure.Scale + figure.Y);
                GL.Vertex2(0.0f * figure.Scale + figure.X, 0.2f * figure.Scale + figure.Y);

                GL.Color4(figure.Color2);
                GL.Vertex2(0.0f * figure.Scale + figure.X, 0.2f * figure.Scale + figure.Y);
                GL.Vertex2(-0.4f * figure.Scale + figure.X, -0.2f * figure.Scale + figure.Y);

                GL.Color4(figure.Color1);
                GL.Vertex2(0.1f * figure.Scale + figure.X, 0.9f * figure.Scale + figure.Y);
                GL.Vertex2(-0.4f * figure.Scale + figure.X, -0.2f * figure.Scale + figure.Y);

                GL.Color4(figure.Color2);
                GL.Vertex2(0.1f * figure.Scale + figure.X, 0.9f * figure.Scale + figure.Y);
                GL.Vertex2(0.0f * figure.Scale + figure.X, 0.2f * figure.Scale + figure.Y);

                GL.Color4(figure.Color1);
                GL.Vertex2(0.1f * figure.Scale + figure.X, 0.9f * figure.Scale + figure.Y);
                GL.Vertex2(0.7f * figure.Scale + figure.X, 0.2f * figure.Scale + figure.Y);
            }

            protected override void OnUpdateFrame(FrameEventArgs e)
            {
                base.OnUpdateFrame(e);

                if (isOne)
                {
                    figure1.X += speedX;
                    figure1.Y += speedY;

                    figure1.Scale -= scaleDelta;

                    if (figure1.Scale < 0.1f || figure1.Scale > 1.0f)
                    {
                        scaleDelta = -scaleDelta;
                    }
        
                    if (Math.Abs(figure1.X) >= 1.0f || Math.Abs(figure1.Y) >= 1.0f)
                    {
                        speedX = -speedX;
                        speedY = -speedY;
                    }
                }
                else
                {
                    angle += 1;

                    //R = (float)(a * b / Math.Sqrt(Math.Pow(a * Math.Sin(angle * Math.PI / 180), 2) + Math.Pow(b * Math.Cos(angle * Math.PI / 180), 2)));

                    //figure2.X = R * (float)Math.Cos(angle * Math.PI / 180);
                    //figure2.Y = R * (float)Math.Sin(angle * Math.PI / 180);

                    figure2.X = a * (float)Math.Cos(angle * Math.PI / 180);
                    figure2.Y = b * (float)Math.Sin(angle * Math.PI / 180);

                    figure2.Scale -= scaleDelta;

                    if (figure2.Scale < 0.1f || figure2.Scale > 1.0f)
                    {
                        scaleDelta = -scaleDelta;
                    }
                }

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
