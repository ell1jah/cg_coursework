using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;

namespace ExhibitVisualization
{

    public partial class Form1 : Form
    {
        Bitmap img;
        Graphics g;
        Scene scene, sceneTurned;
        double tetax, tetay, tetaz;

        LightSource sun1, sun2;
        Zbuffer zbuf;
        ParticleSystem rain;
        
                
        public Form1()
        {
            InitializeComponent();
            Transformation.SetSize(canvas.Width, canvas.Height);

            img = new Bitmap(canvas.Width, canvas.Height);
            g = canvas.CreateGraphics();
            scene = new Scene(canvas.Size);
            scene.CreateScene();
            
            objectList.Items.Add("Камера");
            
            listBox1.Items.Add("1");
            listBox1.Items.Add("2");

            foreach (Model m in scene.GetModels())
            {
                objectList.Items.Add(m.name);
            }

            objectList.SetSelected(0, true);
            listBox1.SetSelected(0, true);

            //Model building = Model.LoadModel(@"D:\GitHub\bmstu_CG_CP\ExhibitVisualization\ExhibitVisualization\res\power.obj");
            //scene.Add(building);
            SetSun();
            HandleSceneChange();
        }

        /// <summary>
        /// Функция сравнения времени двух реализаций (Эксперементальная часть)
        /// </summary>
        // public void CompareTime()
        // {
        //     double res = AnalyseTime(zbuf.AddShadowsParallel);
        //     double res2 = AnalyseTime(zbuf.AddShadows);
        //     ;
        // }

        public long AnalyseTime(Func<Bitmap> act)
        {
            int n = 20;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i < n; i++)
                act();

            stopWatch.Stop();
            return (long)(stopWatch.Elapsed.Ticks)/(long)n;
        }

        private void SetSun()
        {
            sun1 = new LightSource(Color.White, -90, new Vector(0, 0, -1));
            sun2 = new LightSource(Color.White, -110, new Vector(0.4, -0.5, 0));
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string curName = objectList.SelectedItem.ToString();

            if (curName == "Камера")
            {
                scene.MoveScene(-(double)numericUpDown2.Value, 0, 0);
            }
            else
            {
                var m = scene.GetModelByName(curName);
                m.MoveModel((double)numericUpDown2.Value, 0, 0);
            }

            HandleSceneChange();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string curName = objectList.SelectedItem.ToString();

            if (curName == "Камера")
            {
                scene.MoveScene(0, -(double)numericUpDown2.Value, 0);
            }
            else
            {
                var m = scene.GetModelByName(curName);
                m.MoveModel(0, (double)numericUpDown2.Value, 0);
            }

            HandleSceneChange();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string curName = objectList.SelectedItem.ToString();

            if (curName == "Камера")
            {
                scene.MoveScene(0, 0, -(double)numericUpDown2.Value);
            }
            else
            {
                var m = scene.GetModelByName(curName);
                m.MoveModel(0, 0, (double)numericUpDown2.Value);
            }

            HandleSceneChange();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void objectList_DoubleClick(object sender, EventArgs e)
        {
            string curName = objectList.SelectedItem.ToString();

            if (curName == "Камера")
            {
                Point3D cent = scene.GetCentre();
                numericUpDown3.Value = (decimal)cent.x;
                numericUpDown4.Value = (decimal)cent.y;
                numericUpDown5.Value = (decimal)cent.z;
            }
            else
            {
                var m = scene.GetModelByName(curName);
                Point3D cent = m.GetCentre();
                numericUpDown3.Value = (decimal)cent.x;
                numericUpDown4.Value = (decimal)cent.y;
                numericUpDown5.Value = (decimal)cent.z;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string curName = objectList.SelectedItem.ToString();

            if (curName == "Камера")
            {
                scene.ScaleScene((double)numericUpDown6.Value, scene.GetCentre());
            }
            else
            {
                var m = scene.GetModelByName(curName);
                m.ScaleModel((double)numericUpDown6.Value, m.GetCentre());
            }

            HandleSceneChange();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string curName = listBox1.SelectedItem.ToString();

            double t1, t2, t3;
            
            if (curName == "1")
            {
                t1 = sun1.direction.x;
                t2 = sun1.direction.y;
                t3 = sun1.direction.z;
            }
            else
            {
                t1 = sun2.direction.x;
                t2 = sun2.direction.y;
                t3 = sun2.direction.z;
            }
            
            numericUpDown9.Value = (decimal)t1;
            numericUpDown8.Value = (decimal)t2;
            numericUpDown7.Value = (decimal)t3;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string curName = listBox1.SelectedItem.ToString();

            double t1, t2, t3;
            
            if (curName == "1")
            {
                sun1.direction.x = (double)numericUpDown9.Value;
                sun1.direction.y = (double)numericUpDown8.Value;
                sun1.direction.z = (double)numericUpDown7.Value;
                t1 = sun1.direction.x;
                t2 = sun1.direction.y;
                t3 = sun1.direction.z;
            }
            else
            {
                sun2.direction.x = (double)numericUpDown9.Value;
                sun2.direction.y = (double)numericUpDown8.Value;
                sun2.direction.z = (double)numericUpDown7.Value;
                t1 = sun2.direction.x;
                t2 = sun2.direction.y;
                t3 = sun2.direction.z;
            }
            
            numericUpDown9.Value = (decimal)t1;
            numericUpDown8.Value = (decimal)t2;
            numericUpDown7.Value = (decimal)t3;
            
            HandleSceneChange();
        }

        private void HandleSceneChange()
        {
            zbuf = new Zbuffer(scene, canvas.Size, sun1, sun2);
            canvas.Image = zbuf.AddShadows();
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string curName = objectList.SelectedItem.ToString();

            if (curName == "Камера")
            {
                scene.TurnScene(-(double)numericUpDown1.Value, 0, 0, new Point3D((int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value));
            }
            else
            {
                int m = scene.GetModelIndexByName(curName);
                scene.GetModels()[m] = scene.GetModels()[m].GetTurnedModel((double)numericUpDown1.Value, 0, 0, new Point3D((int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value));
            }

            HandleSceneChange();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string curName = objectList.SelectedItem.ToString();
            
            if (curName == "Камера")
            {
                scene.TurnScene(0, -(double)numericUpDown1.Value, 0, new Point3D((int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value));
            }
            else
            {
                int m = scene.GetModelIndexByName(curName);
                scene.GetModels()[m] = scene.GetModels()[m].GetTurnedModel(0,(double)numericUpDown1.Value ,0, new Point3D((int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value));
            }
            
            HandleSceneChange();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string curName = objectList.SelectedItem.ToString();
            
            if (curName == "Камера")
            {
                scene.TurnScene(0, 0, -(double)numericUpDown1.Value, new Point3D((int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value));
            }
            else
            {
                int m = scene.GetModelIndexByName(curName);
                scene.GetModels()[m] = scene.GetModels()[m].GetTurnedModel(0, 0,(double)numericUpDown1.Value, new Point3D((int)numericUpDown3.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value));
            }

            HandleSceneChange();
        }
    }
}
