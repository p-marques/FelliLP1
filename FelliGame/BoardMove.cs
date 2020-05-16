using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame
{
    /// <summary>
    /// A struct capable of holding all necessary info to
    /// perform a move on the board.
    /// </summary>
    public struct BoardMove
    {
        /// <summary>
        /// The piece on the move.
        /// </summary>
        public Piece Piece { get; }

        /// <summary>
        /// The destination of the piece.
        /// </summary>
        public BoardSquare Destination { get; }

        /// <summary>
        /// The piece being eaten (deleted) by the move.
        /// </summary>
        public Piece PieceEaten { get; }

        /// <summary>
        /// Flag signaling the validity of the move.
        /// </summary>
        public bool IsValid => Piece != null && Destination != null;

        /// <summary>
        /// Flag signaling that this move will result in a piece being eaten (eliminated).
        /// </summary>
        public bool IsEating => PieceEaten != null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="piece">The piece on the move.</param>
        /// <param name="destination">The destination of the piece.</param>
        /// <param name="eaten">The piece being eaten (deleted) by the move.</param>
        public BoardMove(Piece piece, BoardSquare destination, Piece eaten)
        {
            Piece = piece;

            Destination = destination;

            PieceEaten = eaten;
        }
    }
}
