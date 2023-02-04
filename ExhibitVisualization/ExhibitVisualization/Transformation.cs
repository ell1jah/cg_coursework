using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExhibitVisualization
{
    static class Transformation
    {
        static int centerX = 0;
        static int centerY = 0;
        
        public static void SetSize(int w, int h)
        {
            centerX = (int)(w/2);
            centerY = (int)(h/2);
        }
                
        static void RotateX(ref double y, ref double z, double tetax, Point3D centre)
        {
            tetax = tetax * Math.PI / 180;
            double buf = y;
            y = centre.y + Math.Cos(tetax) * (y - centre.y) - Math.Sin(tetax) * z;
            z = centre.z + Math.Cos(tetax) * (z - centre.z) + Math.Sin(tetax) * (buf - centre.y);
        }

        static void RotateX(ref double y, ref double z, double cosTetX, double sinTetX, Point3D centre)
        {
            double buf = y;
            y = centre.y + cosTetX * (y - centre.y) - sinTetX * (z - centre.z);
            z = centre.z + cosTetX * (z - centre.z) + sinTetX * (buf - centre.z);
        }

        static void RotateY(ref double x, ref double z, double tetay, Point3D centre)
        {
            tetay = tetay * Math.PI / 180;
            double buf = x;
            x = centre.x + Math.Cos(tetay) * (x - centre.x) - Math.Sin(tetay) * (z - centre.z);
            z = centre.z + Math.Cos(tetay) * (z - centre.z) + Math.Sin(tetay) * (buf - centre.x);
        }

        static void RotateY(ref double x, ref double z, double cosTetY, double sinTetY, Point3D centre)
        {
            double buf = x;
            x = centre.x + cosTetY * (x - centre.x) - sinTetY * (z - centre.z);
            z = centre.z + cosTetY * (z - centre.z) + sinTetY * (buf - centre.x);
        }

        static void RotateZ(ref double x, ref double y, double tetaz, Point3D centre)
        {
            tetaz = tetaz * Math.PI / 180;
            double buf = x;
            x = centerX + Math.Cos(tetaz) * (x - centerX) - Math.Sin(tetaz) * (y - centerY);
            y = centerY + Math.Cos(tetaz) * (y - centerY) + Math.Sin(tetaz) * (buf - centerX);
        }

        static void RotateZ(ref double x, ref double y, double cosTetZ, double sinTetZ, Point3D centre)
        {
            double buf = x;
            x = centerX + cosTetZ * (x - centerX) - sinTetZ * (y - centerY);
            y = centerY + cosTetZ * (y - centerY) + sinTetZ * (buf - centerX);
        }

        /// <summary>
        /// Поворачивает точку относительно осей x, y, z
        /// </summary>
        /// <param name="x">Координата x точки</param>
        /// <param name="y">Координата y точки</param>
        /// <param name="z">Координата z точки</param>
        /// <param name="tetax">Угол поворота вокруг оси x</param>
        /// <param name="tetay">Угол поворота вокруг оси y</param>
        /// <param name="tetaz">Угол поворота вокруг оси z</param>
        public static void Transform(ref int x, ref int y, ref int z, double tetax, double tetay, double tetaz)
        {
            double x_tmp = x;
            double y_tmp = y;
            double z_tmp = z;
            RotateX(ref y_tmp, ref z_tmp, tetax, new Point3D(centerX, centerY, 0));
            RotateY(ref x_tmp, ref z_tmp, tetay, new Point3D(centerX, centerY, 0));
            RotateZ(ref x_tmp, ref y_tmp, tetaz, new Point3D(centerX, centerY, 0));

            x = (int)x_tmp;
            y = (int)y_tmp;
            z = (int)z_tmp;
        }

        public static void Transform(ref double x, ref double y, ref double z, double cosTetX, double sinTetX, double cosTetY, double sinTetY, double cosTetZ, double sinTetZ, Point3D centre)
        {
            double x_tmp = x;
            double y_tmp = y;
            double z_tmp = z;
            RotateX(ref y_tmp, ref z_tmp, cosTetX, sinTetX, centre);
            RotateY(ref x_tmp, ref z_tmp, cosTetY, sinTetY, centre);
            RotateZ(ref x_tmp, ref y_tmp, cosTetZ, sinTetZ, centre);

            x = (int)x_tmp;
            y = (int)y_tmp;
            z = (int)z_tmp;
        }

        public static Point3D Transform(int x, int y, int z, double tetax, double tetay, double tetaz)
        {
            double x_tmp = x;
            double y_tmp = y;
            double z_tmp = z;
            RotateX(ref y_tmp, ref z_tmp, tetax, new Point3D(centerX, centerY, 0));
            RotateY(ref x_tmp, ref z_tmp, tetay, new Point3D(centerX, centerY, 0));
            RotateZ(ref x_tmp, ref y_tmp, tetaz, new Point3D(centerX, centerY, 0));

            return new Point3D((int)x_tmp, (int)y_tmp, (int)z_tmp);
        }
        
        public static Point3D Transform(Point3D p, double tetax, double tetay, double tetaz)
        {
            double x_tmp = p.x;
            double y_tmp = p.y;
            double z_tmp = p.z;
            RotateX(ref y_tmp, ref z_tmp, tetax, new Point3D(centerX, centerY, 0));
            RotateY(ref x_tmp, ref z_tmp, tetay, new Point3D(centerX, centerY, 0));
            RotateZ(ref x_tmp, ref y_tmp, tetaz, new Point3D(centerX, centerY, 0));

            return new Point3D((int)x_tmp, (int)y_tmp, (int)z_tmp);
        }
        
    }
}
