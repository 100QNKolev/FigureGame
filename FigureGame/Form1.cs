using System.Reflection;

namespace FigureGame
{

    public partial class Form1 : Form
    {
        public Random random = new Random();

        public string state = "wokring";
        public const int RectangleSleepTime = 3000;
        public const int CircleSleepTime = 4000;
        public const int TriangleSleepTime = 2000;
        public int FiguresCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(printCircle));
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(printRectangle));
        }

        private void btnTriangle_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(printTriangle));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ResetText();
            this.FiguresCount = 0;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (btnStop.Text == "Stop")
            {
                this.state = "stop";
                btnStop.Text = "Resume";
            }
            else if (btnStop.Text == "Resume") 
            {
                this.state = "working";

                btnCircle.PerformClick();
                btnRectangle.PerformClick();
                btnTriangle.PerformClick();

                btnStop.Text = "Stop";
            }
        }

        public void printRectangle(object obj)
        {
            while (this.state != "stop")
            {
                this.CreateGraphics()
                    .DrawRectangle(new Pen(PickBrush()), new Rectangle(random.Next(0, this.Width), random.Next(0, this.Height), random.Next(0, 70), random.Next(0, 70)));
                this.FiguresCount++;

                Thread.Sleep(RectangleSleepTime);
            }
        }

        public void printCircle(object obj)
        {
            while (this.state != "stop")
            {
                this.CreateGraphics()
                    .DrawEllipse(new Pen(PickBrush()), new Rectangle(random.Next(0, this.Width), random.Next(0, this.Height), random.Next(0,70), random.Next(0, 70)));
                this.FiguresCount++;

                Thread.Sleep(CircleSleepTime);
            }
        }

        public void printTriangle(object obj)
        {
            while (this.state != "stop")
            {
                PointF point1 = new PointF(this.random.Next(200, this.Width) + this.random.Next(20, 170), this.random.Next(0, this.Height - 50) + this.random.Next(20, 170));
                PointF point2 = new PointF(this.random.Next(200, this.Width) + this.random.Next(20, 170), this.random.Next(0, this.Height - 50) + this.random.Next(20, 170));
                PointF point3 = new PointF(this.random.Next(200, this.Width) + this.random.Next(20, 170), this.random.Next(0, this.Height - 50) + this.random.Next(20, 170));

                PointF[] curvePoints =
                {
                    point1,
                    point2,
                    point3,
                };

                this.CreateGraphics().DrawPolygon(new Pen(PickBrush()), curvePoints);
                this.FiguresCount++;

                Thread.Sleep(TriangleSleepTime);
            }
        }

        private Brush PickBrush()
        {
            Brush result = Brushes.Transparent;

            Random rnd = new Random();

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);

            return result;
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"There are {this.FiguresCount}");
        }
    }
}