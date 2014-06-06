using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EyeTrackingController;

namespace $safeprojectname$
{
    public partial class Form1 : Form
    {
        // Schritt 01: Eye Tracker Controller Anlegen
        EyeTrackingController.EyeTrackingController ETDevice;


        // Schritt 02: SampleCallback anlegen bzw. ggf. andere Callbacks anlegen für 
        private delegate void GetSampleCallback(EyeTrackingController.EyeTrackingController.SampleStruct sampleData);
        //Schritt03: Callback anlegen
        GetSampleCallback m_SampleCallback;

        Graphics paper;
        Paddle paddle = new Paddle();
        Ball ball = new Ball();
        List<Rectangle> brick = new List<Rectangle>();
        private const int WIDTH = 480;
        private const int NBRICKS_PER_ROW = 10;
        private const int NBRICK_ROWS = 10;
        private const int BRICK_SEP = 2;
        private const int BRICK_WIDTH = (WIDTH - (NBRICKS_PER_ROW - 1) * BRICK_SEP) / NBRICKS_PER_ROW;
        private const int BRICK_HEIGHT = 10;
        Rectangle nothing = new Rectangle(0, 0, 0, 0);
        private double GazeLeft;
        private double GazeRight;


        //Schritt04: Callbackmethode anlegen

        void GetSampleCallbackFunction(EyeTrackingController.EyeTrackingController.SampleStruct sampleData)
        {
            GazeLeft = sampleData.leftEye.gazeX;
            GazeRight = sampleData.rightEye.gazeX;
        }


       
        public Form1()
        {
            InitializeComponent();
        }

        private void Brick(Brush b, Pen p, PaintEventArgs e)
        {
            int x = 0, y = 0, count = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i < 2) b = new SolidBrush(Color.Pink);
                    else if (i >= 2 & i < 4) b = new SolidBrush(Color.Purple);
                    else if (i >= 4 & i < 6) b = new SolidBrush(Color.Magenta);
                    else if (i >= 6 & i < 8) b = new SolidBrush(Color.Blue);
                    else b = new SolidBrush(Color.Cyan);
                    Rectangle temp = new Rectangle(x + 3, y + 3, BRICK_WIDTH, BRICK_HEIGHT);
                    brick.Add(temp);
                    x += (BRICK_WIDTH + BRICK_SEP);
                    e.Graphics.DrawRectangle(p, brick[count]);
                    e.Graphics.FillRectangle(b, brick[count]);
                    count++;
                }
                x = 0;
                y += BRICK_HEIGHT + BRICK_SEP;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            paper = e.Graphics;
            paddle.drawPaddle(paper);
            ball.drawBall(paper);

            Pen p = new Pen(Color.Blue, 1.0f);
            Brush b = new SolidBrush(Color.Black);

            Brick(b, p, e);

        }

       /* private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            paddle.movePaddel(e.X);
            this.Invalidate();
        }*/

        private void timer1_Tick(object sender, EventArgs e)
        {
            paddle.movePaddel((int) ((GazeRight + GazeLeft)/2));
            
            ball.moveBall();
            ball.collideWall();
            ball.hitPaddle(paddle.PaddleRec);

           for (int i = 0; i < 100; i++)
            {
                if (ball.BallRec.IntersectsWith(brick[i]))
                {
                    brick.RemoveAt(i);
                    brick.Insert(i, nothing);
                    ball.changeDirection();
                }
            }

            this.Invalidate();
        }
    

        private void Form1_Load(object sender, EventArgs e)
        {
            //Schritt 06:
            ETDevice = new EyeTrackingController.EyeTrackingController();

            //Schritt 07:
            m_SampleCallback = new GetSampleCallback(GetSampleCallbackFunction);


            //Schritt 08: mit EyeTracker verbinden
            ETDevice.iV_ConnectLocal();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ETDevice.iV_Disconnect();
        }
    }
}
