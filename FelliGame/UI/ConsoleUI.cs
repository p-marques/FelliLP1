﻿using FelliGame.UI.Entities;
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
        /// The UI element representative of the game's board.
        /// </summary>
        private UIFelliBoard Board => 
                            Screen.GetElementByName("board") as UIFelliBoard;

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
        /// Prompts player to play.
        /// </summary>
        /// <param name="player">The player to be prompted.</param>
        /// <returns>The move the player desires to make.</returns>
        public BoardMove PromptPlay(Player player)
        {
            return this.Board.Play(player);
        }

        /// <summary>
        /// Prompts players to select one the players.
        /// </summary>
        /// <param name="players">The available options.</param>
        /// <param name="title">The title for the dialog.</param>
        /// <returns>Index value of the selected player.</returns>
        public int PromptPlayerSelection(Player[] players, string title)
        {
            int value;
            string helpText;
            UIPosition position;
            UIDialog dialog;

            position = new UIPosition(0, 1);

            helpText = "Use A, D or ArrowLeft and ArrowRight to select one " +
                        "of the buttons bellow and then press Enter.";

            dialog = new UIDialog("userSelection", position, title,
                helpText, players[0].Name, players[1].Name);

            Screen.Add(dialog, "title");

            dialog.Display();

            value = dialog.GetSelected();

            Screen.Remove();

            return value;
        }

        /// <summary>
        /// Adds the game's board to the screen.
        /// </summary>
        /// <param name="board"></param>
        public void AddBoard(Board board)
        {
            UIPosition position;
            UIFelliBoard felliBoard;

            position = new UIPosition(0, 1);

            felliBoard = new UIFelliBoard("board", position, board)
            {
                IsCentered = true
            };

            Screen.Add(felliBoard, "title", AnchorPoint.BottomMiddle);
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
