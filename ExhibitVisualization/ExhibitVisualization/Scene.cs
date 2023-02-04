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
        private const string mask1 =
            "C:\\Bmstu\\5sem\\cursache\\cg_coursework\\ExhibitVisualization\\ExhibitVisualization\\models\\Mask.obj";
        private const string pyramid =
            "C:\\Bmstu\\5sem\\cursache\\cg_coursework\\ExhibitVisualization\\ExhibitVisualization\\models\\pyramid.obj";

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

        public Model GetModelByName(string name)
        {
            foreach (Model m in scene)
            {
                if (m.name == name)
                    return m;
            }

            return null;
        }
        
        public int GetModelIndexByName(string name)
        {
            for (int i = 0; i < scene.Count; i++)
            {
                if (scene[i].name == name)
                    return i;
            }

            return -1;
        }

        public void CreateScene()
        {
            CreateGround(Color.Gray, size.Width / 2, 900, 500, 900, 5);
            // CreateCube(Color.DarkOrange, 300, 100, 0, 150, 300);
            // CreateHelicopter(Color.DarkGreen, size.Width / 2, 400, 500);
            
            CreateSpider(Color.Orange, size.Width / 2, 400, 500);
            var s = GetModelByName("Паук");
            s.MoveModel(-300, 0, 300);
            
            CreateMask(Color.YellowGreen, size.Width / 2, 400, 500);
            var m = GetModelByName("Маска");
            m.ScaleModel(600.0, m.GetCentre());
            int mi = GetModelIndexByName("Маска");
            scene[mi] = scene[mi].GetTurnedModel(90, 0, 0, new Point3D(size.Width / 2, 400, 500));
            m = GetModelByName("Маска");
            m.MoveModel(0, -12, 0);
            
            CreatePyramid(Color.CadetBlue, size.Width / 2, 400, 500);
            var p = GetModelByName("Пирамида");
            p.ScaleModel(20, p.GetCentre());
            p.MoveModel(300, -105, 300);

            // var m = this.GetModelByName("Маска");
            // m.ScaleModel(1500, m.GetCentre());
        }
        
        public Scene GetTurnedScene(double tetax, double tetay, double tetaz)
        {
            Scene s = new Scene(size);

            foreach (Model m in scene)
            {
                s.AddModel(m.GetTurnedModel(tetax, tetay, tetaz, new Point3D(0, 0, 0)));
            }

            return s;
        }

        public void TurnScene(double tetax, double tetay, double tetaz, Point3D cent)
        {
            for (int i = 0; i < scene.Count; i++)
            {
                scene[i] = scene[i].GetTurnedModel(tetax, tetay, tetaz, cent);
            }
        }

        public Point3D GetCentre()
        {
            Point3D cent = new Point3D(0, 0, 0);
            
            foreach (var m in scene)
            {
                cent = cent + m.GetCentre();
            }

            cent = cent / new Point3D(scene.Count, scene.Count, scene.Count);

            return cent;
        }
        
        public void ScaleScene(double k, Point3D centre)
        {
            foreach (var model in scene)
            {
                model.ScaleModel(k, centre);
            }
        }

        public void MoveScene(double tetax, double tetay, double tetaz)
        {
            foreach (var model in scene)
            {
                model.MoveModel(tetax, tetay, tetaz);
            }
        }

        #region Создание Параллелепипедов
        public void CreateCube(Color color, int xCent, int dx, int zCent, int dz, int height, bool roof = false)
        {
            Model m = new Model(color, "Куб", new Point3D(xCent, ground - height / 2, zCent));
            
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

            m.CreatePolygon(3, 2, 7); // верхняя
            m.CreatePolygon(2, 6, 7); // верхняя
            
            m.CreatePolygon(1, 2, 3); // передняя
            m.CreatePolygon(0, 1, 3); // передняя
            
            m.CreatePolygon(0, 3, 4); // левая
            m.CreatePolygon( 3, 7, 4); // левая
            
            m.CreatePolygon(4, 7, 5); // задняя
            m.CreatePolygon(7, 6, 5); // задняя
            
            m.CreatePolygon(1, 5, 2); // правая
            m.CreatePolygon(5, 6, 2); // правая
            
            m.CreatePolygon(0, 4, 1); // нижняя
            m.CreatePolygon(4, 5, 1); // нижняя

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
        
        public void CreatePyramid(Color color, int xCent, int yCent, int zCent)
        {
            LoadModel(pyramid, color, xCent, yCent, zCent, "Пирамида");
        }
        
        public void CreateSpider(Color color, int xCent, int yCent, int zCent)
        {
            LoadModel(spiderPath, color, xCent, yCent, zCent, "Паук");
        }
        
        public void CreateMask(Color color, int xCent, int yCent, int zCent)
        {
            LoadModel(mask1, color, xCent, yCent, zCent, "Маска");
        }

        private void CreateGround(Color color, int xCent, int dx, int zCent, int dz)
        {
            Model m = new Model(color, "Пол", new Point3D(xCent, ground, zCent));

            m.AddVertex(new Point3D(xCent + dx, ground, zCent + dz));
            m.AddVertex(new Point3D(xCent - dx, ground, zCent + dz));
            m.AddVertex(new Point3D(xCent - dx, ground, zCent - dz));
            m.AddVertex(new Point3D(xCent + dx, ground, zCent - dz));

            m.CreatePolygon(0, 3, 2, 1);

            scene.Add(m);
        }

        private void CreateGround(Color color, int xCent, int dx, int zCent, int dz, int height)
        {
            Model m = new Model(color, "Пол", new Point3D(xCent, ground + height / 2, zCent));

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
