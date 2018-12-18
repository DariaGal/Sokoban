using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Sokoban
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string levelPath = System.IO.Directory.GetCurrentDirectory()+"\\Levels\\3.txt";
            var levelManager = new LevelManager();
            var map = levelManager.LoadLevel(levelPath);
            var gameState = new GameState(map.GetLength(0),map.GetLength(1), map);
            Application.Run(new GameWindow(gameState));
        }
    }
}
