using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame
{
    /// <summary>
    /// A game board row.
    /// </summary>
    public class BoardRow
    {
        /// <summary>
        /// The squares on this row.
        /// </summary>
        public BoardSquare[] Squares { get; }

        /// <summary>
        /// Creates a new instance of <see cref="BoardRow"/>.
        /// </summary>
        /// <param name="squareCount">The number of squares in this row.</param>
        /// <param name="rowIndex">The index of this row on the board.</param>
        public BoardRow(uint squareCount, int rowIndex)
        {
            this.Squares = new BoardSquare[squareCount];

            CreateSquares(rowIndex);
        }

        /// <summary>
        /// Created the row's squares.
        /// </summary>
        /// <param name="rowIndex">The index of this row on the board.</param>
        private void CreateSquares(int rowIndex)
        {
            for (int i = 0; i < Squares.Length; i++)
            {
                Squares[i] = new BoardSquare(i, rowIndex);
            }
        }
    }
}
