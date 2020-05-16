using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame
{
    /// <summary>
    /// The game's board.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// The number of rows.
        /// </summary>
        private const int size = 5;

        /// <summary>
        /// The boards' rows.
        /// </summary>
        public BoardRow[] Rows { get; }

        /// <summary>
        /// Creates a new instance of <see cref="Board"/>.
        /// </summary>
        public Board()
        {
            this.Rows = new BoardRow[size];

            CreateRows();
        }

        /// <summary>
        /// Creates the board's rows.
        /// </summary>
        private void CreateRows()
        {
            for (int i = 0; i < size; i++)
            {
                // If it's the midpoint
                if (i == 2)
                {
                    Rows[i] = new BoardRow(1, i);
                    continue;
                }

                Rows[i] = new BoardRow(3, i);
            }
        }

        /// <summary>
        /// Places the players' pieces in their starting positions.
        /// </summary>
        /// <param name="players">An array with both players.</param>
        public void SetupPlayersPieces(Player[] players)
        {
            // For each player.
            for (int i = 0, row = 0; i < 2; i++, row = size - 2)
            {
                // For each row
                for (int piece = 0; row < Rows.Length; row++)
                {
                    // For each square
                    for (int square = 0; square < Rows[row].Squares.Length; square++, piece++)
                    {
                        // If no more pieces to add...
                        if (players[i].Pieces.Length == piece)
                        {
                            // ...get out of current loop.
                            break;
                        }

                        // Place the piece.
                        Rows[row].Squares[square].PutPiece(players[i].Pieces[piece]);
                    }
                }
            }
        }
    }
}
