using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ExhibitVisualization
{
    class LightSource
    {
        public Color color;
        public double tetax = 90, tetay = 0, tetaz = 0;
        public Vector direction;


        public LightSource(Color color, double tetay, Vector direction)
        {
            this.direction = direction;
            this.color = color;
            this.tetay = tetay;
        }

        public void Rotate(double x, double y, double z)
        {
            tetax = (tetax + x) % 360;
            if (tetax < -180)
                tetax += 360;
            else if (tetax > 180)
                tetax -= 360;

            tetay = (tetay + y) % 360;
            if (tetay < -180)
                tetay += 360;
            else if (tetay > 180)
                tetay -= 360;
            
            tetaz = (tetaz + z) % 360;
            if (tetaz < -180)
                tetaz += 360;
            else if (tetaz > 180)
                tetaz -= 360;
        }
    }

}
