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

        LightSource sun1, sun2, sun3, sun4, sun5, currentSun;
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

            foreach (Model m in scene.GetModels())
            {
                objectList.Items.Add(m.name);
            }

            //Model building = Model.LoadModel(@"D:\GitHub\bmstu_CG_CP\ExhibitVisualization\ExhibitVisualization\res\power.obj");
            //scene.Add(building);
            SetSun();
            HandleSceneChange();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            label12.Text = zbuf.GetZ(e.X, e.Y).ToString();
        }
        
        /// <summary>
        /// Функция сравнения времени двух реализаций (Эксперементальная часть)
        /// </summary>
        public void CompareTime()
        {
            double res = AnalyseTime(zbuf.AddShadowsParallel);
            double res2 = AnalyseTime(zbuf.AddShadows);
            ;
        }

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

        #region Установка освещения
        private void button1_Click(object sender, EventArgs e)
        {
            currentSun = sun1;
            HandleSceneChange();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentSun = sun2;
            HandleSceneChange();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentSun = sun3;
            HandleSceneChange();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentSun = sun4;
            HandleSceneChange();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            currentSun = sun5;
            HandleSceneChange();
        }

        private void SetSun()
        {
            sun1 = new LightSource(Color.White, -90, new Vector(1, 0, 0));
            sun2 = new LightSource(Color.White, -110, new Vector(0.4, -0.5, 0));
            sun3 = new LightSource(Color.White, 180, new Vector(0, -1, 0));
            sun4 = new LightSource(Color.White, 110, new Vector(-0.4, -0.5, 0));
            sun5 = new LightSource(Color.White, 90, new Vector(-1, 0, 0));
            currentSun = sun3;
        }
        #endregion

        #region Сцена
        
        private void buttonView_Click(object sender, EventArgs e)
        {
            canvas.Image = zbuf.GetImage();
        }

        private void buttonViewShadows_Click(object sender, EventArgs e)
        {
            canvas.Image = zbuf.AddShadows();
        }

        private void buttonViewSun_Click(object sender, EventArgs e)
        {
            canvas.Image = zbuf.GetSunImage();
        }

        private void HandleSceneChange()
        {
            sceneTurned = scene.GetTurnedScene(tetax, tetay, tetaz);
            zbuf = new Zbuffer(sceneTurned, canvas.Size, currentSun);
            canvas.Image = zbuf.AddShadows();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            tetax += (double)numericUpDown1.Value;
            HandleSceneChange();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            tetay += (double)numericUpDown1.Value;
            HandleSceneChange();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            tetaz += (double)numericUpDown1.Value;
            HandleSceneChange();
        }
        #endregion
        

    }
}
