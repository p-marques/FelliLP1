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
        /// The game's board.
        /// </summary>
        private readonly Board board;

        /// <summary>
        /// The players.
        /// </summary>
        private readonly Player[] players;

        /// <summary>
        /// Creates a new instance of <see cref="Game"/>.
        /// </summary>
        /// <param name="options">The game's options.</param>
        public Game(Options options)
        {
            gameOptions = options;

            players = new Player[]
            {
                new Player(options.PlayerAName),
                new Player(options.PlayerBName)
            };

            board = new Board();
        }

        /// <summary>
        /// The main play loop.
        /// </summary>
        public void Play()
        {
            int playerIndexBlack, playerIndexWhite, currentPlayerIndex;
            bool playing = true;

            Program.UIManager.RefreshUI();

            // Ask who gets the black pieces.
            playerIndexBlack = Program.UIManager.PromptPlayerSelection(players, 
                                "Who plays with the black pieces?");

            Program.UIManager.RefreshUI();

            // Set the white pieces player as the opposite of the selected above
            // as the black pieces player.
            playerIndexWhite = playerIndexBlack == 1 ? 0 : 1;

            // Create the pieces...
            players[playerIndexBlack].CreatePieces(PieceColor.Black);
            players[playerIndexWhite].CreatePieces(PieceColor.White);

            // ...and add them to the board.
            board.SetupPlayersPieces(players);

            // Ask who plays first.
            currentPlayerIndex = Program.UIManager.PromptPlayerSelection(players,
                                                            "Who plays first?");
            
            Program.UIManager.AddBoard(board);

            Program.UIManager.RefreshUI();

            while (playing)
            {
                currentPlayerIndex = PlayerPlay(currentPlayerIndex);

                // If currentPlayerIndex is below 0 that means the player
                // refused to perform a move. This is treated as an instruction
                // to quit the game.
                playing = currentPlayerIndex < 0 ? false : true;

                if (playing)
                {
                    // If the player that will perform the next play doesn't
                    // have pieces...
                    if (players[currentPlayerIndex].Pieces.Length == 0)
                    {
                        // ...get the winner player index...
                        currentPlayerIndex = GetNextPlayerIndex(currentPlayerIndex);

                        // ...and congratulate him.
                        Program.UIManager
                            .DisplayEndGameScreen(players[currentPlayerIndex]);

                        playing = false;
                    }
                }
                else
                {
                    Program.UIManager.DisplayEndGameScreen();
                }
            }
        }

        /// <summary>
        /// Perform a full play by a player.
        /// </summary>
        /// <param name="currentPlayerIndex">The index of the current player.
        /// </param>
        /// <returns>A value representative of the next player's index.
        /// </returns>
        private int PlayerPlay(int currentPlayerIndex)
        {
            BoardMove move;

            move = Program.UIManager.PromptPlay(players[currentPlayerIndex]);

            if (move.IsValid)
            {
                board.MovePiece(move);
            }

            // Returning -1 is handled upstream as a signal by 
            // this player that he desires to exit the game.
            return move.IsValid ? GetNextPlayerIndex(currentPlayerIndex) : -1;
        }

        /// <summary>
        /// Get the next player to play.
        /// </summary>
        /// <param name="currentIndex">The current's player index.</param>
        /// <returns>A value representative of the next player's index.</returns>
        private int GetNextPlayerIndex(int currentIndex)
        {
            return currentIndex == 0 ? 1 : 0;
        }
    }
}
