using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    public partial class Form1 : Form
    {
        //MOVEMENT
        enum Position
        {
            Left, Right, Up, Down
        }

        private int x;
        private int y;
        private Position objPosition;

        //DRAWING
        Graphics g;
        Rectangle rectanglePacman;
        Bitmap pacman;
        GraphicsPath gPath = null;
        private PointF[] points;
        private int pointCounter;
        private Bitmap bmp = null;
        int[,] formPoints = new int[,] { {50,40}, {235, 40}, {235, 110}, {290, 110}, {290, 40}, {470, 40}, {470, 155}, {390, 155}, {390, 450},
             {470, 450}, {470, 500}, {50, 500}, {50, 450}, {130, 450}, {130, 155}, {50, 155}, {50, 40}, {313, 44}, {313, 44}, {313, 44},
              {313, 44}, {313, 44}, {313, 44}, {313, 44}, {313, 44}, {313, 44}, {313, 44}, {313, 44}, {313, 44}, {313, 44} };

        //EXTRA
        Random rn = new Random();
        Timer t = new Timer();

        public Form1()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            pacman = new Bitmap("pacman.png");
            x = 45;
            y = 27;
            objPosition = Position.Down;

            this.DoubleBuffered = true;            

            //this.ClientSize = new Size(400, 400);
            this.FormClosing += delegate { if (bmp != null) bmp.Dispose(); if (gPath != null) gPath.Dispose(); };


            gPath = new GraphicsPath();

            List<Point> fList = new List<Point>();

            for (int i = 0; i < 17; i++)
                fList.Add(new Point(formPoints[i, 0], formPoints[i, 1]));

            gPath.AddLines(fList.ToArray());
            //gPath.AddCurve(fList.ToArray());
            gPath.Flatten();

            this.points = gPath.PathPoints;

            this.Paint += new PaintEventHandler(Form1_Paint);

            //start the timer
            t.Tick += new EventHandler(t_Tick);
            t.Interval = 40;
            //t.Start();

        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            if (gPath != null && pacman != null)
            {
                //draw the image
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(Pens.Red, gPath);
                e.Graphics.DrawImage(pacman, (int)points[pointCounter].X - (pacman.Width / 2), (int)points[pointCounter].Y - (pacman.Height / 2));
            }
        }

        void t_Tick(object sender, EventArgs e)
        {
            t.Stop();

            //redraw
            this.Invalidate();

            pointCounter++;
           

            t.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pointCounter >= points.Length)
                pointCounter = 0;

            /*if (objPosition == Position.Right) 
            else if (objPosition == Position.Left) */
            else if (objPosition == Position.Up) y -= 10;
            else if (objPosition == Position.Down) y += 10;

            Invalidate();
            //Console.WriteLine(y + x);
        }

        private void controls(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                pointCounter--;
                objPosition = Position.Left;
                timer1.Start();
            }
            else if (e.KeyCode == Keys.Right)
            {
                pointCounter++;
                objPosition = Position.Right;
                timer1.Start();
            }
            else if (e.KeyCode == Keys.Up)
            {
                objPosition = Position.Up;
                timer1.Start();
            }
            else if (e.KeyCode == Keys.Down)
            {
                objPosition = Position.Down;
                timer1.Start();
            }
        }

        private void ohno(object sender, PaintEventArgs e)
        {
           
        }
    }
}
