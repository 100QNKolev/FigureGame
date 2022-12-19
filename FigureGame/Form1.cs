using System.Reflection;

namespace FigureGame
{

    public partial class Form1 : Form
    {
        public Random random = new Random();

        public const int RectangleSleepTime = 3000;
        public const int CircleSleepTime = 4000;
        public const int TriangleSleepTime = 2000;

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
            
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private void btnShoot_Click(object sender, EventArgs e)
        {

        }

        public void printRectangle(object obj)
        {
            while (true)
            {
                this.CreateGraphics()
                    .DrawRectangle(new Pen(PickBrush()), new Rectangle(random.Next(0, this.Width), random.Next(0, this.Height), random.Next(0, 100), random.Next(0, 100)));
                Thread.Sleep(RectangleSleepTime);
            }
        }

        public void printCircle(object obj)
        {
            while (true)
            {
                this.CreateGraphics()
                    .DrawEllipse(new Pen(PickBrush()), new Rectangle(random.Next(0, this.Width), random.Next(0, this.Height), random.Next(0,100), random.Next(0, 100)));
                Thread.Sleep(CircleSleepTime);
            }
        }

        public void printTriangle(object obj)
        {
            while (true)
            {
                PointF point1 = new PointF(new Random().Next(200, this.Width) + new Random().Next(20, 170), new Random().Next(0, this.Height - 50) + new Random().Next(20, 170));
                PointF point2 = new PointF(new Random().Next(200, this.Width) + new Random().Next(20, 170), new Random().Next(0, this.Height - 50) + new Random().Next(20, 170));
                PointF point3 = new PointF(new Random().Next(200, this.Width) + new Random().Next(20, 170), new Random().Next(0, this.Height - 50) + new Random().Next(20, 170));

                PointF[] curvePoints =
                {
                    point1,
                    point2,
                    point3,
                };

                this.CreateGraphics().DrawPolygon(new Pen(PickBrush()), curvePoints);

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
    }
}