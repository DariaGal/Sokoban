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
        private readonly HashSet<Keys> pressedKeys = new HashSet<Keys>();
        private readonly Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        private GameState gameState;
        private int tickCount;
        public static Keys KeyPressed;
        private bool gameEnd;
        Timer timer;

        public GameWindow(GameState gameState, DirectoryInfo imagesDirectory = null)
        {
            InitializeComponent();
            gameEnd = false;
            ClientSize = new Size(
                gameState.ElementSize * gameState.MapWidth,
                gameState.ElementSize * gameState.MapHeight + gameState.ElementSize*2);
            FormBorderStyle = FormBorderStyle.FixedDialog;

            if (imagesDirectory == null)
                imagesDirectory = new DirectoryInfo("Images");
            foreach (var e in imagesDirectory.GetFiles("*.png"))
                bitmaps[Path.GetFileNameWithoutExtension(e.Name)] = (Bitmap)Image.FromFile(e.FullName);
            this.gameState = gameState;

            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += TimerTick;
            timer.Start();
        }
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
                Brushes.Black, 0, 32, gameState.ElementSize * gameState.MapWidth,
                gameState.ElementSize * gameState.MapHeight);
            foreach (var a in gameState.Animations)
                e.Graphics.DrawImage(bitmaps[a.Name], a.Location);
            e.Graphics.ResetTransform();
            e.Graphics.DrawString("Steps:"+gameState.StepCount.ToString(), new Font("Arial", 16), Brushes.Green, 0, 32);
            if (gameEnd)
            {
                e.Graphics.FillRectangle(Brushes.Black, gameState.ElementSize * gameState.MapWidth / 2 - 82,
                   (gameState.ElementSize * gameState.MapHeight + 100) / 2, 165, 25);
                e.Graphics.DrawString("LEVEL PASSED", new Font("Arial", 16), Brushes.White,
                    gameState.ElementSize * gameState.MapWidth / 2 - 82, (gameState.ElementSize * gameState.MapHeight + 100) / 2);
            }

        }
        private void TimerTick(object sender, EventArgs args)
        {
            if (!gameEnd)
            {
                gameState.Act();
                tickCount++;
                if (tickCount == 8) tickCount = 0;
                Invalidate();

                if (gameState.EndGame())
                {
                    gameEnd = true;
                }
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Level passed! \nGo to menu to restart or exit", "SOKOBAN", MessageBoxButtons.OK);

            }
        }

        private MenuStrip menuStrip1;

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // GameWindow
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameWindow";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem restartToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameState.RestartGame();
            timer.Start();
            gameEnd = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

