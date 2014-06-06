using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace $safeprojectname$
{
    public class Paddle
    {
        private int x, y, width, height;

        public Image paddleImage;

        private Rectangle paddleRec;

        public Rectangle PaddleRec
        {
            get { return paddleRec; }

        }

        public Paddle()
        {
            x = 0;
            y = 445;
            width = 70;
            height = 10;
            paddleImage = Image.FromFile("C:\\Users\\Anna\\Documents\\paddle.png");
            paddleRec = new Rectangle(x, y, width, height);


        }

        public void drawPaddle(Graphics paper)
        {
            paper.DrawImage(paddleImage, paddleRec);
        }

        public void movePaddel(int eyeX)
        {
            paddleRec.X = eyeX - (paddleRec.Width / 2);
        }
    }

}
