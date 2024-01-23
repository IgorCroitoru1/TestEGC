using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using OpenTK3_StandardTemplate_WinForms.helpers;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTK3_StandardTemplate_WinForms.objects
{
      
        class Cube
        {
        private bool cubeMoved = false;
        private int textureId;
        // Clasa ColorRandomizer nu este definită aici; presupun că generează o culoare aleatorie
        ColorRandomizer randomColor = new ColorRandomizer();

            // Matricea de culori pentru fețele cubului
            Color4 color = new Color4();

        // Vectorii normali pentru fiecare față a cubului
        float[,] n = new float[,]
                {
            {-1.0f, 0.0f, 0.0f},
            {0.0f, 1.0f, 0.0f},
            {1.0f, 0.0f, 0.0f},
            {0.0f, -1.0f, 0.0f},
            {0.0f, 0.0f, 1.0f},
            {0.0f, 0.0f, -1.0f}
                };

        // Indicii vertecșilor pentru fiecare față a cubului
        int[,] faces = new int[,]
            {
            {0, 1, 2, 3},
            {3, 2, 6, 7},
            {7, 6, 5, 4},
            {4, 5, 1, 0},
            {5, 6, 2, 1},
            {7, 4, 0, 3}
            };

            // Lista de vertecși care definesc cubul
            private List<VertexPoint> vertices;

            public Cube(int size)
            {
            // Inițializarea vertecșilor cubului
            vertices = new List<VertexPoint>
                {
                    new VertexPoint(-size / 2, -size / 2, -size / 2),
                    new VertexPoint(size / 2, -size / 2, -size / 2),
                    new VertexPoint(size / 2, size / 2, -size / 2),
                    new VertexPoint(-size / 2, size / 2, -size / 2),
                    new VertexPoint(-size / 2, -size / 2, size / 2),
                    new VertexPoint(size / 2, -size / 2, size / 2),
                    new VertexPoint(size / 2, size / 2, size / 2),
                    new VertexPoint(-size / 2, size / 2, size / 2)
                };
            LoadTexture(textureId, "C:\\Users\\Igor\\Downloads\\OpenTK3_StandardTemplate_WinForms\\OpenTK3_StandardTemplate_WinForms\\da.png");

            // Obținerea unei culori aleatorii pentru cub
            color = randomColor.GetRandomColor();
            }

            // Metoda care verifică tastatura pentru comenzi
            public void KeyBinds()
            {
                KeyboardState keyboard = Keyboard.GetState();

               
            }
        private void LoadTexture(int textureId, string filename)
        {
            Bitmap bmp = new Bitmap(filename);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                                                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                          bmp.Width, bmp.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                          PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
        }
       
        public void Move(int dist)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                vertices[i].coordX +=dist;
                vertices[i].coordY -= dist;
            }
            cubeMoved = true;
        }
        public bool getMove() { return cubeMoved; }
        public void setMove() {  cubeMoved = !cubeMoved; }
        public void setMove(bool move) { cubeMoved = move; }

        // Metoda care desenează cubul
        public void Draw()
            {
                GL.Begin(PrimitiveType.Quads);
                for (int i = 0; i < 6; i++)
                {
                    //GL.Normal3(n[i, 0], n[i, 1], n[i, 2]);

                    for (int j = 0; j < 4; j++)
                    {
                        int vertexIndex = faces[i, j];
                        GL.Color4(color);

                        GL.Vertex3(vertices[vertexIndex].coordX, vertices[vertexIndex].coordY, vertices[vertexIndex].coordZ);
                    }
                }
                GL.End();
            }
        public void DrawCubeText()
        {
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            GL.Begin(PrimitiveType.Quads);
            for (int i = 0; i < 6; i++)
            {
                // GL.Normal3(n[i, 0], n[i, 1], n[i, 2]);

                for (int j = 0; j < 4; j++)
                {
                    int vertexIndex = faces[i, j];
                    // Culorile nu mai sunt folosite pentru texturare
                    GL.Color3((Color)Color.White);
                    GL.TexCoord2(j % 2, j / 2); // Coordonatele texturii pentru fiecare vârf
                    GL.Vertex3(vertices[vertexIndex].coordX, vertices[vertexIndex].coordY, vertices[vertexIndex].coordZ);
                }
            }
            GL.End();
        }
    }
    }

