using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using OpenTK3_StandardTemplate_WinForms.helpers;
using OpenTK3_StandardTemplate_WinForms.objects;

namespace OpenTK3_StandardTemplate_WinForms
{
    public partial class MainForm : Form
    {
        private bool lightingEnabled = true;
        private float lightIntensity = 100.0f;
        private float lightSourceX = 0.0f;
        private float lightSourceY = 0.0f;
        private float lightSourceZ = 0.0f;
        float[] lightPosition = { 0.0f, 20.0f, 0.0f, 1.0f };
        private Axes mainAxis;
        private Rectangles re;
        private Camera cam;
        private Scene scene;
        private Cube cube;
        private Parallelepiped par;
        private Pyramid pyramid;
        private int[] textures = new int[2];

        private Point mousePosition;

        public MainForm()
        {   
            // general init
            InitializeComponent();

            // init VIEWPORT
            scene = new Scene();

            scene.GetViewport().Load += new EventHandler(this.mainViewport_Load);
            scene.GetViewport().Paint += new PaintEventHandler(this.mainViewport_Paint);
            scene.GetViewport().MouseMove += new MouseEventHandler(this.mainViewport_MouseMove);

            this.Controls.Add(scene.GetViewport());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // init RNG
            Randomizer.Init();

            // init CAMERA/EYE
            cam = new Camera(scene.GetViewport());
            cube = new Cube(30);
            par = new Parallelepiped(20,30,30);
            pyramid = new Pyramid(50);
            // init AXES
            mainAxis = new Axes(showAxes.Checked);
            re = new Rectangles();
        }

        private void showAxes_CheckedChanged(object sender, EventArgs e)
        {
            mainAxis.SetVisibility(showAxes.Checked);

            scene.Invalidate();
        }

        private void changeBackground_Click(object sender, EventArgs e)
        {
            GL.ClearColor(Randomizer.GetRandomColor());

            scene.Invalidate();
        }

        private void resetScene_Click(object sender, EventArgs e)
        {
            showAxes.Checked = true;
            mainAxis.SetVisibility(showAxes.Checked);
            scene.Reset();
            cam.Reset();

            scene.Invalidate();
        }

        //private void LoadTexture(int textureId, string filename)
        //{
        //    Bitmap bmp = new Bitmap(filename);

        //    BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
        //                                            System.Drawing.Imaging.ImageLockMode.ReadOnly,
        //                                            System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        //    GL.BindTexture(TextureTarget.Texture2D, textureId);
        //    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
        //                  bmp.Width, bmp.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
        //                  PixelType.UnsignedByte, data.Scan0);

        //    bmp.UnlockBits(data);

        //    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
        //    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
        //}

        private void mainViewport_Load(object sender, EventArgs e)
        {
            SetupLighting();

            scene.Reset();
        }

        private void mainViewport_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = new Point(e.X, e.Y);
            scene.Invalidate();
        }

        //private void BtnToggleLighting_Click(object sender, EventArgs e)
        //{
        //    ToggleLighting();
        //    UpdateLighting();
        //    scene.Invalidate(); // Trigger a redraw of your scene
        //}

        private void ToggleLighting()
        {
            lightingEnabled = !lightingEnabled;
        }

        private void IncreaseLightIntensity()
        {
            lightIntensity += 0.1f; // You can adjust the step as needed
            if (lightIntensity > 1.0f)
                lightIntensity = 1.0f;
        }

        private void DecreaseLightIntensity()
        {
            lightIntensity -= 0.1f; // You can adjust the step as needed
            if (lightIntensity < 0.0f)
                lightIntensity = 0.0f;
        }

        private void UpdateLighting()
        {
          
                float[] ambient = { lightIntensity, lightIntensity, lightIntensity, 1.0f };
                 float[] diffuse = { lightIntensity, lightIntensity, lightIntensity, 1.0f };
                 float[] specular = { lightIntensity, lightIntensity, lightIntensity, 1.0f };
                Color color = Color.Red;
              GL.Light(LightName.Light0, LightParameter.Ambient, ambient);
            //GL.Light(LightName.Light0, LightParameter.Position, lightPosition);
            GL.Light(LightName.Light0, LightParameter.Diffuse, diffuse);
                GL.Light(LightName.Light0, LightParameter.Specular, specular);
               
               // scene.Invalidate();
               
        }

        private void DrawLightSource()
        {
            GL.PointSize(15.0f); // Adjust the size as needed
            GL.Begin(PrimitiveType.Points);
            GL.Color3(Color.Yellow);
            GL.Vertex3(lightPosition[0], lightPosition[1], lightPosition[2]);
            GL.End();
            scene.Invalidate();
        }
        public void SetupLighting()
        {



            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            //float[] lightPosition = { 15.0f, 20.0f, 30.0f, 1.0f };
            GL.Light(LightName.Light0, LightParameter.Position, lightPosition);
            float[] ambient = { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] diffuse = { lightIntensity, lightIntensity, lightIntensity, 1.0f };
            float[] specular = { lightIntensity, lightIntensity, lightIntensity, 1.0f };
            //  GL.Light(LightName.Light0, LightParameter.Ambient, ambient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, diffuse);
            GL.Light(LightName.Light0, LightParameter.Specular, specular);


            // Enable lighting
            // GL.Enable(EnableCap.Lighting);

            // // Enable the light source
            // GL.Enable(EnableCap.Light0);

            // // Set the light position (e.g., positional light at coordinates x, y, z, w)
            // float[] lightPosition = { 60.0f, 30.0f, 20.0f, 1.0f };
            // GL.Light(LightName.Light0, LightParameter.Position, lightPosition);

            // // Set other light properties (e.g., ambient, diffuse, specular)
            //float[] ambient = { 0.2f, 0.2f, 0.2f, 1.0f };
            // float[] diffuse = { 1.0f, 1.0f, 1.0f, 1.0f };
            // float[] specular = { 1.0f, 1.0f, 1.0f, 1.0f };
            // Color color = Color.Red;
            //// float[] ambient = { color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, 1.0f };
            //// float[] diffuse = { color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, 1.0f };
            //// float[] specular = { color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, 1.0f };
            //// GL.Light(LightName.Light0, LightParameter.Ambient, ambient);
            //// GL.Light(LightName.Light0, LightParameter.Diffuse, diffuse);
            //// GL.Light(LightName.Light0, LightParameter.Specular, specular);

            // Set the light attenuation (optional)
            //  GL.Light(LightName.Light0, LightParameter.ConstantAttenuation, 1.0f);
            // GL.Light(LightName.Light0, LightParameter.LinearAttenuation, 0.05f);
            //  GL.Light(LightName.Light0, LightParameter.QuadraticAttenuation, 0.0f);
        }

        private void mainViewport_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            cam.SetView();

            if (enableRotation.Checked == true)
            {
                // Doar după axa Ox.
                GL.Rotate(Math.Max(mousePosition.X, mousePosition.Y), 1, 1, 1);
            }

            // GRAPHICS PAYLOAD
            mainAxis.Draw();

            if (enableObjectRotation.Checked == true)
            {
                // Rotatie a obiectului
                GL.PushMatrix();
                GL.Rotate(Math.Max(mousePosition.X, mousePosition.Y), 1, 1, 1);
                re.Draw();
                GL.PopMatrix();
            } else
            {
                //pyramid.DrawPyramidText();
                //r.DrawParallelepipedText();
               // cube.Draw();
                cube.DrawCubeText();
            }
            if (cube.getMove())
            {
                // Redraw the cube after moving
                cube.Draw();
                cube.setMove(false);  // Reset the flag after redrawing
            }
            DrawLightSource();
            scene.GetViewport().SwapBuffers();
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            cube.Move(10);
           // GlControl.Invalidate();
          //  GLControl1.Invalidate();
        }

        private void enableRotation_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnLight_Click(object sender, EventArgs e)
        {
            //ToggleLighting();
          //    UpdateLighting();
          //  scene.Invalidate();
            //GlControl.Invalidate(); // Trigger a redraw of your scene
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
           // lightPosition[0] += lightPosition[0] + 3;
           // lightPosition[1] += lightPosition[1] + 3;
           // lightPosition[2] += lightPosition[2] + 3;
            lightIntensity = trackBar1.Value/10 ; // Adjust the divisor based on your requirements
            UpdateLighting();
            DrawLightSource();
            scene.Invalidate();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
             lightPosition[0] += lightPosition[0] + 1;
             lightPosition[1] += lightPosition[1] + 1;
             lightPosition[2] += lightPosition[2] + 1;
           // UpdateLighting();
            DrawLightSource();
            scene.Invalidate();
        }
    }
}
