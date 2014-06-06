using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace $safeprojectname$
{
    public class Ball
    {
        private int x, y, width, height;

        private Random randSpeed;

        private int speedX, speedY;

        public Image ballImage;

        private Rectangle ballRec;

        public Rectangle BallRec
        {
            get { return ballRec; }

        }

        public Ball()
        {
            randSpeed = new Random();
            x = 230;
            y = 230;
            width = 20;
            height = 20;
            speedX = randSpeed.Next(5, 8);
            speedY = randSpeed.Next(5, 8);

            ballImage = Image.FromFile("C:\\Users\\Anna\\Documents\\ball.png");
            ballRec = new Rectangle(x, y, width, height);


        }

        public void drawBall(Graphics paper)
        {
            paper.DrawImage(ballImage, ballRec);
        }

        public void moveBall()
        {
            ballRec.X += speedX;
            ballRec.Y += speedY;
        }

        public void collideWall()
        {
            if (ballRec.X < 0 || ballRec.X > 464)
            {
                speedX *= -1;
            }

            if (ballRec.Y < 0)
            {
                changeDirection();
            }

        }

        public void hitPaddle(Rectangle paddleRec)
        {
            if (ballRec.IntersectsWith(paddleRec))
            {
                changeDirection();
            }
        }

        public void changeDirection()
        {
            speedY *= -1;
        }
    }

}
