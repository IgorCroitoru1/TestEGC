using OpenTK.Graphics.OpenGL;

using OpenTK3_StandardTemplate_WinForms.helpers;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace OpenTK3_StandardTemplate_WinForms.objects
{
    class Pyramid
    {
        private List<VertexPoint> vertices;
        private int textureId; // Texture ID for the pyramid

        public Pyramid(int size)
        {
            vertices = new List<VertexPoint>
            {
                // Define the vertices of the pyramid
                new VertexPoint(0, 0, 0, Color.Red),        // Vertex 0 (base)
                new VertexPoint(size, 0, 0, Color.Green),   // Vertex 1 (base)
                new VertexPoint(size, 0, size, Color.Blue),  // Vertex 2 (base)
                new VertexPoint(0, 0, size, Color.Yellow),   // Vertex 3 (base)
                new VertexPoint(size / 2, size, size / 2, Color.Purple) // Apex (top)
            };

            // Load texture here (replace "path_to_texture.jpg" with the actual path)
            LoadTexture("C:\\Users\\Igor\\Downloads\\OpenTK3_StandardTemplate_WinForms\\OpenTK3_StandardTemplate_WinForms\\da.png");
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Triangles);

            // Draw the base of the pyramid
            DrawTriangle(vertices[0], vertices[1], vertices[2]);
            DrawTriangle(vertices[0], vertices[2], vertices[3]);

            // Draw the sides of the pyramid
            DrawTriangle(vertices[0], vertices[1], vertices[4]);
            DrawTriangle(vertices[1], vertices[2], vertices[4]);
            DrawTriangle(vertices[2], vertices[3], vertices[4]);
            DrawTriangle(vertices[3], vertices[0], vertices[4]);

            GL.End();
        }

        public void DrawPyramidText()
        {
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            GL.Begin(PrimitiveType.Triangles);

            // Draw the base of the pyramid
            DrawTexturedTriangle(vertices[0], vertices[1], vertices[2]);
            DrawTexturedTriangle(vertices[0], vertices[2], vertices[3]);

            // Draw the sides of the pyramid
            DrawTexturedTriangle(vertices[0], vertices[1], vertices[4]);
            DrawTexturedTriangle(vertices[1], vertices[2], vertices[4]);
            DrawTexturedTriangle(vertices[2], vertices[3], vertices[4]);
            DrawTexturedTriangle(vertices[3], vertices[0], vertices[4]);

            GL.End();
        }

        private void DrawTriangle(VertexPoint v1, VertexPoint v2, VertexPoint v3)
        {
            GL.Color3(v1.pointColor);
            GL.Vertex3(v1.coordX, v1.coordY, v1.coordZ);

            GL.Color3(v2.pointColor);
            GL.Vertex3(v2.coordX, v2.coordY, v2.coordZ);

            GL.Color3(v3.pointColor);
            GL.Vertex3(v3.coordX, v3.coordY, v3.coordZ);
        }

        private void DrawTexturedTriangle(VertexPoint v1, VertexPoint v2, VertexPoint v3)
        {
            GL.TexCoord2(0, 0); // Texture coordinates for v1
            GL.Color3(v1.pointColor);
            GL.Vertex3(v1.coordX, v1.coordY, v1.coordZ);

            GL.TexCoord2(1, 0); // Texture coordinates for v2
            GL.Color3(v2.pointColor);
            GL.Vertex3(v2.coordX, v2.coordY, v2.coordZ);

            GL.TexCoord2(0.5, 1); // Texture coordinates for v3
            GL.Color3(v3.pointColor);
            GL.Vertex3(v3.coordX, v3.coordY, v3.coordZ);
        }

        private void LoadTexture(string filename)
        {
            Bitmap bmp = new Bitmap(filename);

            GL.GenTextures(1, out textureId);
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
        }
    }
}
