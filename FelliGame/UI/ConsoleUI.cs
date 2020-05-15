using FelliGame.UI.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame.UI
{
    /// <summary>
    /// Class responsible for managing the user interface.
    /// </summary>
    public class ConsoleUI
    {
        /// <summary>
        /// The current screen.
        /// </summary>
        private Screen Screen { get; }

        /// <summary>
        /// Creates a new instance of <see cref="ConsoleUI"/>.
        /// </summary>
        public ConsoleUI()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.CursorVisible = false;

            this.Screen = new Screen();

            AddTitle();
        }

        /// <summary>
        /// Clears the console and displays the entire UI.
        /// </summary>
        public void RefreshUI()
        {
            this.Screen.Refresh();
        }

        /// <summary>
        /// Adds the title to the screen.
        /// </summary>
        private void AddTitle()
        {
            UIPosition titlePos = new UIPosition(0, 0);
            UITitle appTitle = new UITitle("title", titlePos, "Felli", 30, 2);

            this.Screen.Add(appTitle);
        }
    }
}
