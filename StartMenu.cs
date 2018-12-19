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
        public StartMenu(string levelPath)
        {
            InitializeComponent();
            var levelDirectory = new DirectoryInfo("Levels");
            foreach (var e in levelDirectory.GetFiles("*.sokoban"))
                comboBoxLevels.Items.Add(Path.GetFileNameWithoutExtension(e.Name));
            comboBoxLevels.SelectedItem = comboBoxLevels.Items[0];
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
        }
    }
}
