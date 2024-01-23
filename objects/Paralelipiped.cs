using OpenTK.Graphics.OpenGL;

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
    class Parallelepiped
    {
        int lengthX;
        int lengthY;
        int lengthZ;
        int[,] faces = new int[,]
         {
            {0, 1, 2, 3},
            {3, 2, 6, 7},
            {7, 6, 5, 4},
            {4, 5, 1, 0},
            {5, 6, 2, 1},
            {7, 4, 0, 3}
         };
        private int textureId;
        // Clasa ColorRandomizer nu este definită aici; presupun că generează o culoare aleatorie
        ColorRandomizer randomColor = new ColorRandomizer();
        private List<VertexPoint> vertices;
        // Other members...

        public Parallelepiped(int lengthX, int lengthY, int lengthZ)
        {
            this.lengthX = lengthX;
            this.lengthY = lengthY;
            this.lengthZ = lengthZ;
            // Initialize the vertices for the parallelepiped
            vertices = new List<VertexPoint>
        {
            new VertexPoint(-lengthX / 2, -lengthY / 2, -lengthZ / 2),
            new VertexPoint(lengthX / 2, -lengthY / 2, -lengthZ / 2),
            new VertexPoint(lengthX / 2, lengthY / 2, -lengthZ / 2),
            new VertexPoint(-lengthX / 2, lengthY / 2, -lengthZ / 2),
            new VertexPoint(-lengthX / 2, -lengthY / 2, lengthZ / 2),
            new VertexPoint(lengthX / 2, -lengthY / 2, lengthZ / 2),
            new VertexPoint(lengthX / 2, lengthY / 2, lengthZ / 2),
            new VertexPoint(-lengthX / 2, lengthY / 2, lengthZ / 2)

        };
            LoadTexture(textureId, "C:\\Users\\Igor\\Downloads\\OpenTK3_StandardTemplate_WinForms\\OpenTK3_StandardTemplate_WinForms\\da.png");

            // Other initialization...
        }
        public void Draw()
        {
            GL.Begin(PrimitiveType.Quads);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int vertexIndex1 = faces[i, j];
                    int vertexIndex2 = faces[i, (j + 1) % 4];

                    // Draw edges by connecting adjacent vertices
                    GL.Color4(Color.Blue);
                    GL.Vertex3(vertices[vertexIndex1].coordX, vertices[vertexIndex1].coordY, vertices[vertexIndex1].coordZ);
                    GL.Vertex3(vertices[vertexIndex2].coordX, vertices[vertexIndex2].coordY, vertices[vertexIndex2].coordZ);
                }
            }
            GL.End();
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
        public void DrawParallelepipedText()
        {
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            GL.Begin(PrimitiveType.Quads);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int vertexIndex = faces[i, j];

                    // Calculate texture coordinates based on vertex position and length of the parallelepiped
                    float u = (vertices[vertexIndex].coordX + lengthX / 2) / lengthX;  // Normalize X
                    float v = (vertices[vertexIndex].coordY + lengthY / 2) / lengthY;  // Normalize Y
                    GL.Color3((Color)Color.White);
                    GL.TexCoord2(u, v);  // Assign texture coordinates
                    GL.Vertex3(vertices[vertexIndex].coordX, vertices[vertexIndex].coordY, vertices[vertexIndex].coordZ);
                }
            }
            GL.End();
        }


        // Other methods...
    }

}
