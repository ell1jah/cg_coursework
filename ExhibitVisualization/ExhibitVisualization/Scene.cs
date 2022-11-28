using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace ExhibitVisualization
{
    class Scene
    {
        List<Model> scene;
        Size size;
        static int ground = 400;

        private const string helicopterPath =
            "C:\\Bmstu\\5sem\\cursache\\cg_coursework\\ExhibitVisualization\\ExhibitVisualization\\models\\Seahawk.obj";
        private const string spiderPath =
            "C:\\Bmstu\\5sem\\cursache\\cg_coursework\\ExhibitVisualization\\ExhibitVisualization\\models\\Only_Spider_with_Animations_Export.obj";

        public Scene(Size size)
        {
            scene = new List<Model>();
            this.size = size;
        }

        public void AddModel(Model m)
        {
            scene.Add(m);
        }
        
        public List<Model> GetModels()
        {
            return scene;
        }

        public void CreateScene()
        {
            CreateGround(Color.CadetBlue, size.Width / 2, 400, 0, 500, 5);
            CreateCube(Color.DarkOrange, 300, 100, 0, 150, 300);
            CreateHelicopter(Color.DarkGreen, size.Width / 2, 400, 500);
            CreateSpider(Color.Black, size.Width / 2, 400, 500);
        }
        
        public Scene GetTurnedScene(double tetax, double tetay, double tetaz)
        {
            Scene s = new Scene(size);

            foreach (Model m in scene)
            {
                s.AddModel(m.GetTurnedModel(tetax, tetay, tetaz));
            }

            return s;
        }

        #region Создание Параллелепипедов
        public void CreateCube(Color color, int xCent, int dx, int zCent, int dz, int height, bool roof = false)
        {
            Model m = new Model(color, "Куб");
            
            //передняя
            m.AddVertex(new Point3D(xCent - dx, ground, zCent + dz)); // левая нижняя
            m.AddVertex(new Point3D(xCent + dx, ground, zCent + dz)); // правая нижняя
            m.AddVertex(new Point3D(xCent + dx, ground - height, zCent + dz)); // правая верхняя
            m.AddVertex(new Point3D(xCent - dx, ground - height, zCent + dz)); // левая верхняя

            // задняя
            m.AddVertex(new Point3D(xCent - dx, ground, zCent - dz)); // левая нижняя
            m.AddVertex(new Point3D(xCent + dx, ground, zCent - dz)); // правая нижняя
            m.AddVertex(new Point3D(xCent + dx, ground - height, zCent - dz)); // правая верхняя
            m.AddVertex(new Point3D(xCent - dx, ground - height, zCent - dz)); // левая верхняя

            m.CreatePolygon(3, 2, 6, 7); // верхняя
            m.CreatePolygon(0, 1, 2, 3); // передняя
            m.CreatePolygon(0, 3, 7, 4); // левая
            m.CreatePolygon(4, 7, 6, 5); // задняя
            m.CreatePolygon(1, 5, 6, 2); // правая
            m.CreatePolygon(0, 4, 5, 1); // нижняя

            if (roof)
            {
                m.AddVertex(new Point3D(xCent, ground - height - 40, zCent)); // Верхушка i = 8
                m.CreatePolygon(3, 2, 8);
                m.CreatePolygon(7, 3, 8);
                m.CreatePolygon(6, 7, 8);
                m.CreatePolygon(2, 6, 8);
            }

            scene.Add(m);
        }

        private void LoadModel(string path, Color color, int xCent, int yCent, int zCent, string name)
        {
            Model m = Model.LoadModel(path, color, xCent, yCent, zCent, name);
            if (m == null)
            {
                Debug.WriteLine("ERROR: Object file does not exist: " + path);
            }
            else
            {
                scene.Add(m);
            }
        }

        public void CreateHelicopter(Color color, int xCent, int yCent, int zCent)
        {
            LoadModel(helicopterPath, color, xCent, yCent, zCent, "Вертолет");
        }
        
        public void CreateSpider(Color color, int xCent, int yCent, int zCent)
        {
            LoadModel(spiderPath, color, xCent, yCent, zCent, "Самолет");
        }

        private void CreateGround(Color color, int xCent, int dx, int zCent, int dz)
        {
            Model m = new Model(color, "Пол");

            m.AddVertex(new Point3D(xCent + dx, ground, zCent + dz));
            m.AddVertex(new Point3D(xCent - dx, ground, zCent + dz));
            m.AddVertex(new Point3D(xCent - dx, ground, zCent - dz));
            m.AddVertex(new Point3D(xCent + dx, ground, zCent - dz));

            m.CreatePolygon(0, 3, 2, 1);

            scene.Add(m);
        }

        private void CreateGround(Color color, int xCent, int dx, int zCent, int dz, int height)
        {
            Model m = new Model(color, "Пол");

            //передняя
            m.AddVertex(new Point3D(xCent - dx, ground + height, zCent + dz)); // левая нижняя
            m.AddVertex(new Point3D(xCent + dx, ground + height, zCent + dz)); // правая нижняя
            m.AddVertex(new Point3D(xCent + dx, ground, zCent + dz)); // правая верхняя
            m.AddVertex(new Point3D(xCent - dx, ground, zCent + dz)); // левая верхняя

            // задняя
            m.AddVertex(new Point3D(xCent - dx, ground + height, zCent - dz)); // левая нижняя
            m.AddVertex(new Point3D(xCent + dx, ground + height, zCent - dz)); // правая нижняя
            m.AddVertex(new Point3D(xCent + dx, ground, zCent - dz)); // правая верхняя
            m.AddVertex(new Point3D(xCent - dx, ground, zCent - dz)); // левая верхняя

            m.CreatePolygonSpecial(3, 2, 6, 7); // верхняя
            m.CreatePolygonSpecial(0, 1, 2, 3); // передняя
            m.CreatePolygonSpecial(0, 3, 7, 4); // левая
            m.CreatePolygonSpecial(4, 7, 6, 5); // задняя
            m.CreatePolygonSpecial(1, 5, 6, 2); // правая
            m.CreatePolygonSpecial(0, 4, 5, 1); // нижняя

            scene.Add(m);
        }
        #endregion
    }
}
