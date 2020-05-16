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
        /// A reference to the game title.
        /// </summary>
        private UITitle UITitleRef { get; set; }

        /// <summary>
        /// A reference to the game board.
        /// </summary>
        private UIFelliBoard UIBoardRef { get; set; }

        /// <summary>
        /// A reference to the info panel.
        /// </summary>
        private UIInfoPanel UIInfoPanelRef { get; set; }

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
            UIInfoPanelRef.UpdateContentByName("currentPlayer", 
                                        $"Current player: {player.Name}");

            UIInfoPanelRef.Display();

            return this.UIBoardRef.Play(player);
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

            Screen.Add(dialog, UITitleRef.BottomLeft);

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

            Screen.Add(felliBoard, UITitleRef.BottomMiddle);

            this.UIBoardRef = felliBoard;

            AddInfoPanel();
        }

        /// <summary>
        /// Displays the end game screen.
        /// </summary>
        /// <param name="winner">Optional. If a player is provided he will be 
        /// congratulated for the win.</param>
        public void DisplayEndGameScreen(Player winner = null)
        {
            string content;
            UITitle gameOver;
            UIPosition pos = new UIPosition(0, 0);

            Screen.Clear();

            content = "Game Over!";

            if (winner != null)
            {
                content += $" {winner.Name} has won! Congratulations!";
            }

            gameOver = new UITitle("gameover", pos, content, 30, 2);

            Screen.Add(gameOver);

            RefreshUI();
        }

        /// <summary>
        /// Adds the title to the screen.
        /// </summary>
        private void AddTitle()
        {
            UIPosition titlePos = new UIPosition(0, 0);
            UITitle appTitle = new UITitle("title", titlePos, "Felli", 30, 2);

            this.Screen.Add(appTitle);

            UITitleRef = appTitle;
        }

        /// <summary>
        /// Adds the info panel to the screen.
        /// </summary>
        private void AddInfoPanel()
        {
            UIPosition position;
            UIText uiText;
            UIInfoPanel infoPanel;

            position = new UIPosition(0, 0);

            infoPanel = new UIInfoPanel("info", position, 1);

            uiText = new UIText("currentPlayer", position, "", 0, 5, 
                UISettings.ColorInfoPanelBg, 
                ConsoleColor.White, 
                ConsoleColor.Red);

            infoPanel.Add(uiText);

            uiText = new UIText("availableInput0", position, "Available keys:", 
                0, 0, UISettings.ColorInfoPanelBg, ConsoleColor.Black, 
                UISettings.ColorInfoPanelBg);

            infoPanel.Add(uiText);

            uiText = new UIText("availableInput1", position, 
                "    [A] / [D] or [ArrowLeft] / [ArrowRight]", 0, 0, 
                UISettings.ColorInfoPanelBg, ConsoleColor.Black, 
                UISettings.ColorInfoPanelBg);

            infoPanel.Add(uiText);

            uiText = new UIText("availableInput2", position, "    [Enter]", 0, 0, 
                UISettings.ColorInfoPanelBg, ConsoleColor.Black, 
                UISettings.ColorInfoPanelBg);

            infoPanel.Add(uiText);

            uiText = new UIText("availableInput3", position, "    [ESC]", 0, 0, 
                UISettings.ColorInfoPanelBg, ConsoleColor.Black, 
                UISettings.ColorInfoPanelBg);

            infoPanel.Add(uiText);

            Screen.Add(infoPanel, UITitleRef.BottomRight);

            this.UIInfoPanelRef = infoPanel;
        }
    }
}
