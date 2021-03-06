﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;

namespace AlgorithmsStage3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Movement flags   
        bool testFlag1X = true;
        bool testFlag1Y = true;
        #endregion

        #region Key Pressed Flags

        bool flagA = false;
        bool flagD = false;
        bool flagW = false;
        bool flagS = false;

        #endregion

        #region Random Number

        Random rand;
        int ctr = 0;
        bool startAnim = false;
        int anim = 0;

        Image[] myImageArray;

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            myImageArray = new Image[12] { 
                imageMain,
                imageMainFollow,
                testImage1,
                testImage2,
                testImage3,
                testImage4,
                testImage5,
                testImage6,
                testImage7,
                testImage8,
                testImage9,
                testImage10};

            rand = new Random(DateTime.Now.Millisecond);

            for (int i = 2; i < myImageArray.Length; i++) {
                myImageArray[i].Width = rand.Next(20, 40);
                myImageArray[i].Height = myImageArray[i].Width;
                //myImageArray[i].Margin.Left = rand.Next(0,(double)TestGrid.Width);
               // myImageArray[i].Margin.Top = rand.Next(0, (double)TestGrid.Height);
            }

            #region Random Number

            // Add code here

            #endregion

            // Set game loop timer
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(10000); // 10000 ticks = 1 milisecond
            dispatcherTimer.Start();
        }

        /// <summary>
        /// Our time event that fires the movement
        /// </summary>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            rand = new Random(DateTime.Now.Millisecond);
            
            #region Move using a key press
            Utils.Move(testImage1, flagA, flagD, flagW, flagS, 5.00);
            #endregion

            #region Lock_To_Grid

            //Utils.Lock_To_Grid(testImage1,TestGrid);

            #endregion

            #region Move_Lock_To_Grid

            Utils.Move_Lock_To_Grid(myImageArray[0],TestGrid,(10 / rand.Next(1,20)+ 2),ref testFlag1X,ref testFlag1Y,rand,ref ctr);


            #endregion

            #region Follow

           // Utils.Follow(testImage2,testImage4, (10 / (double)rand.Next(1, 20) + 2));
            //Utils.Follow(testImage3, testImage4, (10 / (double)rand.Next(1, 21) + 1));

            for (int i = 2; i < myImageArray.Length; i++) {
                Utils.Follow(myImageArray[i], myImageArray[rand.Next(1,myImageArray.Length)], (10 / (double)rand.Next(1, 21) + 5));
                Utils.RunAway(myImageArray[i],myImageArray[rand.Next(1,myImageArray.Length)], (10 / (double)rand.Next(1, 21) + 5));
            }


            #endregion

            #region Runaway
            Utils.Follow(myImageArray[1], myImageArray[0], 2);
            Utils.RunAway(myImageArray[1], myImageArray[0], 3);
            Utils.Lock_To_Grid(myImageArray[1], TestGrid);


            #endregion

            #region Collide




            
                Utils.Collide(myImageArray, myImageArray[0], ref testFlag1X, ref testFlag1Y, ref startAnim);
            
                #endregion

            #region Random Number

            // Add code here

            #endregion

            Utils.Anim(testImage1,ref startAnim,ref anim);

        }

        /// <summary>
        /// Resizes the grid to the screen size
        /// </summary>
        private void TestWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TestGrid.Width = TestWindow.Width - 30;
            TestGrid.Height = TestWindow.Height - 50;
        }

        #region Key Pressed test

        private void TestWindow_KeyDown(object sender, KeyEventArgs e)
        {

            flagA = false;
            flagD = false;
            flagW = false;
            flagS = false;

            if (e.Key == Key.A) flagA = true;
            if (e.Key == Key.D) flagD = true;
            if (e.Key == Key.W) flagW = true;
            if (e.Key == Key.S) flagS = true;
        }

        #endregion
    }
}
