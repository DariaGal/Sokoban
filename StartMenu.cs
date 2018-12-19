using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban
{
    public partial class StartMenu : Form
    {
        LevelManager levelManager;
        public StartMenu(string levelPath)
        {
            InitializeComponent();
            levelManager = new LevelManager();
            var levelDirectory = new DirectoryInfo("Levels");
            foreach (var file in levelDirectory.GetFiles("*.sokoban"))
            { 
                comboBoxLevels.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
                levelManager.AddLevel(Path.GetFileNameWithoutExtension(file.Name), file);
            }
            if(comboBoxLevels.Items.Count > 0)
                comboBoxLevels.SelectedItem = comboBoxLevels.Items[0];
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            var levelInfo = levelManager.LoadLevel(comboBoxLevels.SelectedItem.ToString());
            var gameState = new GameState(levelInfo);
            GameWindow gameWindow = new GameWindow(gameState);
            gameWindow.FormClosing += delegate { MakeVisible(); };
            this.Hide();
            gameWindow.ShowDialog();
        }

        private void MakeVisible()
        {
            this.Visible = true;
        }
    }
}
