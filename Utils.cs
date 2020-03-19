using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

// Include for the image and grid objects
using System.Windows;
using System.Windows.Controls;

namespace AlgorithmsStage3
{
    public static class Utils
    {
        #region Move using a key press
        /// <summary>
        /// Move an Image
        /// </summary>
        public static void Move(Image anImage, bool left, bool right, bool top, bool bottom, double speed)
        {
            double leftMargin = anImage.Margin.Left;
            double topMargin = anImage.Margin.Top;
            double rightMargin = anImage.Margin.Right;
            double bottomMargin = anImage.Margin.Bottom;


            if (left == true) leftMargin = leftMargin - speed;
            if (right == true) leftMargin = leftMargin + speed;
            if (top == true) topMargin = topMargin - speed;
            if (bottom == true) topMargin = topMargin + speed;

            anImage.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);
        }
        #endregion

        #region Lock_To_Grid

        #region Lock_To_Grid

        public static void Lock_To_Grid(Image anImage, Grid testGrid)
        {

            double leftMargin = anImage.Margin.Left;
            double rightMargin = anImage.Margin.Right;
            double topMargin = anImage.Margin.Top;
            double bottomMargin = anImage.Margin.Bottom;



            if (leftMargin + anImage.Width > testGrid.Width) leftMargin = testGrid.Width - anImage.Width;
            if (leftMargin < 0) leftMargin = 0;
            if (topMargin < 0) topMargin = 0;
            if (topMargin + anImage.Height > testGrid.Height) topMargin = testGrid.Height - anImage.Height;


            anImage.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);

        }

        #endregion


        #endregion

        #region Move_Lock_To_Grid

        #region Move_Lock_To_Grid

        public static void Move_Lock_To_Grid(Image anImage, Grid testGrid, double speed, ref bool testFlag1X, ref bool testFlag1Y,Random rand, ref int ctr)
        {
            rand = new Random(DateTime.Now.Millisecond);
            double min = 240;
            double max = 420;
            double duration = rand.NextDouble() * (max - min) + min;


            double leftMargin = anImage.Margin.Left;
            double rightMargin = anImage.Margin.Right;
            double topMargin = anImage.Margin.Top;
            double bottomMargin = anImage.Margin.Bottom;



            if (leftMargin + anImage.Width > testGrid.Width)
            {

                leftMargin = testGrid.Width - anImage.Width;
                testFlag1X = !testFlag1X;
            }
            if (leftMargin < 0)
            {

                leftMargin = 0;
                testFlag1X = !testFlag1X;

            }
            if (topMargin < 0)
            {

                topMargin = 0;
                testFlag1Y = !testFlag1Y;

            }
            if (topMargin + anImage.Height > testGrid.Height)
            {

                topMargin = testGrid.Height - anImage.Height;

                testFlag1Y = !testFlag1Y;
            }



            ctr++;
            Console.WriteLine(ctr);
            if (ctr > duration)
            {
                testFlag1X = Convert.ToBoolean(rand.Next(0, 2));
                testFlag1Y = Convert.ToBoolean(rand.Next(0, 2));
                ctr = 0;
            }



            if (testFlag1X) leftMargin += speed;
            if (!testFlag1X) leftMargin -= speed;
            if (testFlag1Y) topMargin += speed;
            if (!testFlag1Y) topMargin -= speed;


            anImage.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);



        }

        #endregion

        #endregion

        #region Follow

        public static void Follow(Image anImage, Image target, double speed)
        {

            //double distance = 10;

            double leftMargin = anImage.Margin.Left;
            double rightMargin = anImage.Margin.Right;
            double topMargin = anImage.Margin.Top;
            double bottomMargin = anImage.Margin.Bottom;

            if (leftMargin + anImage.Width < target.Margin.Left ) leftMargin += speed;
            
            if (leftMargin > target.Margin.Left + target.Width ) leftMargin -= speed;

            if (topMargin + anImage.Height < target.Margin.Top ) topMargin += speed;

            if (topMargin > target.Margin.Top + target.Height ) topMargin -= speed;


            anImage.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);

        }



        #endregion

        #region Runaway


        public static void RunAway(Image anImage, Image target, double speed)
        {

            double distance =200;

            double leftMargin = anImage.Margin.Left;
            double rightMargin = anImage.Margin.Right;
            double topMargin = anImage.Margin.Top;
            double bottomMargin = anImage.Margin.Bottom;

            if (leftMargin + anImage.Width < target.Margin.Left - distance) leftMargin -= speed;
            
            if (leftMargin > target.Margin.Left + target.Width + distance) leftMargin += speed;

            if (topMargin + anImage.Height  <= target.Margin.Top - distance) topMargin -= speed;

            if (topMargin > target.Margin.Top + target.Height + distance) topMargin += speed;


            anImage.Margin = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);



        }


        #endregion

        #region Collide

        public static void Collide(Image[] anImage, Image target, ref bool testFlag1X, ref bool testFlag1Y, ref bool startAnim)
        {
            for (int i = 2; i < anImage.Length; i++) {
                if ((anImage[i].Margin.Left + anImage[i].Width) > target.Margin.Left && anImage[i].Margin.Left < (target.Margin.Left + target.Width) &&
                      (anImage[i].Margin.Top + anImage[i].Height) > target.Margin.Top && anImage[i].Margin.Top < (target.Margin.Top + target.Height))
                {


                    if (anImage[i].Visibility != Visibility.Hidden)
                    {
                        testFlag1X = !testFlag1X;
                        testFlag1Y = !testFlag1Y;
                        anImage[i].Visibility = Visibility.Hidden;
                        //target.Opacity -= 0.10;
                        target.Width += 10;
                        target.Height += 10;

                        startAnim = true;
                        SoundPlayer sound = new SoundPlayer(Properties.Resources.sound);
                        sound.Play();
                        Console.WriteLine("BOOOOOOOOOOOM PARANG NENENG B KANYANG KATAWAN");
                    }

                }
                else
                {
                    Console.WriteLine("bat ayaw mo lumapit sa akin crush");
                }
                }
            }


        public static void Anim(Image anImage,ref bool startAnim, ref int anim)
        {

            if (startAnim)
            {
                anim++;
                if (anim > 10) anImage.Visibility = Visibility.Hidden;
                if (anim > 20) anImage.Visibility = Visibility.Visible;
                if (anim > 30) anImage.Visibility = Visibility.Hidden;
                if (anim > 40) anImage.Visibility = Visibility.Visible;
                if (anim > 50) anImage.Visibility = Visibility.Hidden;
                if (anim > 60)
                {
                    anImage.Visibility = Visibility.Visible;
                    anim = 0;
                    startAnim = false;
                }

            }

        }
        #endregion



    }
}
