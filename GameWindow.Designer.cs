using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Sokoban
{
    partial class GameWindow : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
       // private System.ComponentModel.IContainer components = null;
       
        private readonly HashSet<Keys> pressedKeys = new HashSet<Keys>();
        private readonly Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        private GameState gameState;
        private int tickCount;
        public static Keys KeyPressed;

        public GameWindow(GameState gameState, DirectoryInfo imagesDirectory = null)
        {
            ClientSize = new Size(
                gameState.ElementSize * gameState.MapWidth,
                gameState.ElementSize * gameState.MapHeight + gameState.ElementSize);
            FormBorderStyle = FormBorderStyle.FixedDialog;

            if (imagesDirectory == null)
                imagesDirectory = new DirectoryInfo("Images");
            foreach (var e in imagesDirectory.GetFiles("*.png"))
                bitmaps[Path.GetFileNameWithoutExtension(e.Name)] = (Bitmap)Image.FromFile(e.FullName);
            this.gameState = gameState;

            var timer = new Timer();
            timer.Interval = 50;
            timer.Tick += TimerTick;
            timer.Start();
        }
        /* /// <summary>
         /// Clean up any resources being used.
         /// </summary>
         /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
         protected override void Dispose(bool disposing)
         {
             if (disposing && (components != null))
             {
                 components.Dispose();
             }
             base.Dispose(disposing);
         }*/
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "Sokoban";
            DoubleBuffered = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            pressedKeys.Add(e.KeyCode);
            KeyPressed = e.KeyCode;
            gameState.SetDirection(KeyPressed);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
            KeyPressed = pressedKeys.Any() ? pressedKeys.Min() : Keys.None;
            gameState.SetDirection(KeyPressed);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(0, gameState.ElementSize);
            e.Graphics.FillRectangle(
                Brushes.Black, 0, 0, gameState.ElementSize * gameState.MapWidth,
                gameState.ElementSize * gameState.MapHeight);
            foreach (var a in gameState.Animations)
                e.Graphics.DrawImage(bitmaps[a.Name], a.Location);
            e.Graphics.ResetTransform(); 
            e.Graphics.DrawString(KeyPressed.ToString(), new Font("Arial", 16), Brushes.Green, 0, 0);
            // base.OnPaint(e);
        }
        private void TimerTick(object sender, EventArgs args)
        {
          //  if (tickCount == 0) gameState.BeginAct();
            gameState.Act();
            /* foreach (var e in gameState.Animations)
                 e.Location = new Point(e.Location.X, e.Location.Y);*/
          /*  if (tickCount == 7)
                gameState.EndAct();*/
            tickCount++;
            if (tickCount == 8) tickCount = 0;
            Invalidate();
        }

       /* private void SetDirection(Keys key)
        {
            switch(key)
            {
                case Keys.Up:
                    gameState.SetDirection(Directions.Up);
                    break;
                case Keys.Down:
                    gameState.SetDirection(Directions.Down);
                    break;
                case Keys.Left:
                    gameState.SetDirection(Directions.Left);
                    break;
                case Keys.Right:
                    gameState.SetDirection(Directions.Right);
                    break;
                case Keys.W:
                    gameState.SetDirection(Directions.Up);
                    break;
                case Keys.S:
                    gameState.SetDirection(Directions.Down);
                    break;
                case Keys.A:
                    gameState.SetDirection(Directions.Left);
                    break;
                case Keys.D:
                    gameState.SetDirection(Directions.Right);
                    break;
                default:
                    gameState.SetDirection(Directions.None);
                    break;
            }
        }*/
        /*
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";
        }

        #endregion*/
    }
}

