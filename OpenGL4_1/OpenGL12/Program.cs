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
            readonly float lineWidth = 5.0f;
            float[,] defaultArray = { { 0.1f, 0.9f }, { 0.3f, -0.2f }, { -0.4f, -0.2f }, { 0.0f, 0.2f }, { 0.7f, 0.2f } };
            float[,] array = { { 0.1f, 0.9f }, { 0.3f, -0.2f }, { -0.4f, -0.2f }, { 0.0f, 0.2f }, { 0.7f, 0.2f } };

            public Game(int width, int height, GraphicsMode graphicsMode, string title, GameWindowFlags gameWindowFlags, DisplayDevice displayDevice) : base(width, height, GraphicsMode.Default, title, GameWindowFlags.Default, displayDevice) { }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);
                GL.Enable(EnableCap.DepthTest);
                GL.MatrixMode(MatrixMode.Modelview);


                //зсув - 1
                //TranslationMatrix();

                //відображення відносно однієї з осей - 2
                //float[,] vid1 = { { 1, 0 }, { 0, -1 } };
                //MultiplyMatrix2(vid1);

                //float[,] vid2 = { { -1, 0 }, { 0, 1 } };
                //MultiplyMatrix2(vid2);

                float[,] vid3 = { { 0, 1 }, { 1, 0 } };
                MultiplyMatrix2(vid3);

                //маштабування - 4
                //ScalingMatrix();

                //повіріт - 5
                //PoviritMatrix(123);
            }
            private void ScalingMatrix()
            {
                //У якій a=b, b=c=0, виконується пропорційне масштабування;
                //Якщо a≠d, b = c = 0, то масштабування буде непропорційним. У першому випадку
                //для a = d > 1 відбувається розширення, тобто збільшення зображення. Якщо
                //a = d < 1, то відбувається рівномірне стиснення.

                float[,] scaling = { { 1.2f, 0 }, { 0, 1.2f } };

                MultiplyMatrix(scaling);
            }
            private void TranslationMatrix()
            {
                float[,] translation = { { 1f, 0.5f }, { 0, 1f } };

                int rowsA = array.GetLength(0);
                int colsA = array.GetLength(1);
                int rowsB = translation.GetLength(0);
                int colsB = translation.GetLength(1);

                float[,] result = new float[rowsA, colsB];

                float b = -0.3f;

                for (int i = 0; i < rowsA; i++)
                {
                    result[i, 0] = array[i, 0]; 
                    result[i, 1] = array[i, 1] + b; 
                }

                array = result;
            }

            private void PoviritMatrix(double angle)
            {
                double angleRadians = DegreesToRadians(angle);

                float[,] turn = { { (float)Math.Cos(angleRadians), (float)Math.Sin(angleRadians) }, { -(float)Math.Sin(angleRadians), (float)Math.Cos(angleRadians) } };

                MultiplyMatrix(turn);
            }
            private void MultiplyMatrix2(float[,] array2)
            {
                int rowsA = array.GetLength(0);
                int colsA = array.GetLength(1);
                int colsB = array2.GetLength(1);

                float[,] result = new float[rowsA, colsB];

                // Множення матриць
                for (int i = 0; i < rowsA; i++)
                {
                    for (int j = 0; j < colsB; j++)
                    {
                        float sum = 0;
                        for (int k = 0; k < colsA; k++)
                        {
                            sum += array[i, k] * array2[k, j];
                        }
                        result[i, j] = sum;
                    }
                }

                array = result;
            }
            private void MultiplyMatrix(float[,] array2)
            {
                int rowsA = array.GetLength(0);
                int colsA = array.GetLength(1);
                int rowsB = array2.GetLength(0);
                int colsB = array2.GetLength(1);

                float[,] result = new float[rowsA, colsB];

                // Множення матриць
                for (int i = 0; i < rowsA; i++)
                {
                    for (int j = 0; j < colsB; j++)
                    {
                        float sum = 0;
                        for (int k = 0; k < colsA; k++)
                        {
                            sum += array[i, k] * array2[k, j];
                        }
                        result[i, j] = sum;
                    }
                }

                array = result;
            }

            private double DegreesToRadians(double degrees)
            {
                return degrees * Math.PI / 180.0;
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

                DrawPiramide(defaultArray, Color4.Gray);
                DrawPiramide(array, Color4.LightGreen);

                GL.Begin(PrimitiveType.Lines);
                GL.Vertex2(-1, -1);
                GL.Vertex2(1,1);
                GL.End();

                SwapBuffers();
            }


            protected void DrawPiramide(float[,] points, Color4 color4 = default)
            {
                GL.Begin(PrimitiveType.Lines);

                GL.Color4(color4);
                for (int i = 1; i < points.GetLength(0); i++)
                {
                    if (i == points.GetLength(0) - 1)
                    {
                        GL.Vertex2(points[i, 0], points[i, 1]);
                        GL.Vertex2(points[1, 0], points[1, 1]);
                        break;
                    }

                    GL.Vertex2(points[i, 0], points[i, 1]);
                    GL.Vertex2(points[(i + 1), 0], points[(i + 1), 1]);
                }

                for (int i = 1; i < points.GetLength(0); i++)
                {
                    GL.Vertex2(points[0, 0], points[0, 1]);
                    GL.Vertex2(points[i, 0], points[i, 1]);
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
