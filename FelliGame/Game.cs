using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FelliGame
{
    /// <summary>
    /// Class responsible for managing the entire game's flow.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The game's options.
        /// </summary>
        private readonly Options gameOptions;

        /// <summary>
        /// The players.
        /// </summary>
        public Player[] Players { get; }

        /// <summary>
        /// Creates a new instance of <see cref="Game"/>.
        /// </summary>
        /// <param name="options">The game's options.</param>
        public Game(Options options)
        {
            gameOptions = options;

            Players = new Player[]
            {
                new Player(options.PlayerAName),
                new Player(options.PlayerBName)
            };
        }

        /// <summary>
        /// The main play loop.
        /// </summary>
        public void Play()
        {
            int playerIndexBlack, playerIndexWhite;
            bool playing = true;

            Program.UIManager.RefreshUI();

            playerIndexBlack = Program.UIManager.PromptPlayerSelection(Players, 
                                "Who plays with the black pieces?");

            Program.UIManager.RefreshUI();

            playerIndexWhite = playerIndexBlack == 1 ? 0 : 1;

            while (playing)
            {
                playing = false;
            }
        }
    }
}
