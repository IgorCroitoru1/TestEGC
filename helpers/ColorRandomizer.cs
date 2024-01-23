using OpenTK.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTK3_StandardTemplate_WinForms.helpers
{
  
    public class ColorRandomizer
    {
        private Random random;

        public ColorRandomizer()
        {
            random = new Random();
        }

        public Color4 GetRandomColor()
        {
            float r = (float)random.NextDouble();
            float g = (float)random.NextDouble();
            float b = (float)random.NextDouble();
            return new Color4(r, g, b, 1.0f);
        }
    }
}
