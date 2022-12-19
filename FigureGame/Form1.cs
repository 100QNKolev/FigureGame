using System.Reflection;

namespace FigureGame
{

    public partial class Form1 : Form
    {
        public Random random = new Random();

        public const int RectangleSleepTime = 4000;

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

        private void btnSquare_Click(object sender, EventArgs e)
        {

        }

        private void btnTriangle_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnRestart_Click(object sender, EventArgs e)
        {

        }

        private void btnShoot_Click(object sender, EventArgs e)
        {

        }

        public void printRectangle(object obj)
        {
            while (true)
            {
                this.CreateGraphics()
                    .DrawRectangle(new Pen(PickBrush()), new Rectangle(random.Next(0, this.Width), random.Next(0, this.Height), 20, 20));
                Thread.Sleep(RectangleSleepTime);
            }
        }

        public void printCircle(object obj)
        {
            while (true)
            {
                this.CreateGraphics()
                    .DrawEllipse(new Pen(PickBrush()), new Rectangle(random.Next(0, this.Width), random.Next(0, this.Height), 20, 20));
                Thread.Sleep(RectangleSleepTime);
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