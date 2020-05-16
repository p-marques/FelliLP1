using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame
{
    /// <summary>
    /// A game board square.
    /// </summary>
    public class BoardSquare
    {
        /// <summary>
        /// The position of the square on the board.
        /// </summary>
        public Coord Pos { get; }

        /// <summary>
        /// The piece on this square.
        /// </summary>
        public Piece Piece { get; private set; }

        /// <summary>
        /// Flag signaling the presence of a piece on this square.
        /// </summary>
        public bool HasPiece
        {
            get { return Piece != null; }
        }

        /// <summary>
        /// Creates a new instance of <see cref="BoardSquare"/>.
        /// </summary>
        /// <param name="columnIndex">The index of this square on the 
        /// row.</param>
        /// <param name="rowIndex">The index of this square's parent row 
        /// on the board.</param>
        public BoardSquare(int columnIndex, int rowIndex)
        {
            Pos = new Coord(rowIndex, columnIndex);
        }

        /// <summary>
		/// Place a game piece in this square.
		/// </summary>
		/// <param name="piece">The game piece to place.</param>
		public void PutPiece(Piece piece)
        {
            // If this piece is already on the board...
            if (piece.Square != null)
            {
                // ...remove it from it's current location.
                piece.Square.Piece = null;
            }

            this.Piece = piece;

            piece.SetBoardSquareReference(this);
        }

        /// <summary>
        /// Removes the piece of this square.
        /// </summary>
        public void RemovePiece()
        {
            this.Piece = null;
        }
    }
}
