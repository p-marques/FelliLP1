using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame
{
    /// <summary>
    /// A game piece.
    /// </summary>
    public class Piece
    {
        /// <summary>
        /// The Id of the piece.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// A reference to this piece's location.
        /// </summary>
        public BoardSquare Square { get; private set; }

        /// <summary>
        /// The <see cref="Player"/> who owns this piece.
        /// </summary>
        public Player Owner { get; }

        /// <summary>
        /// Thee piece's color.
        /// </summary>
        public PieceColor Color { get; }

        /// <summary>
        /// Creates a new intance of Piece.
        /// </summary>
        /// <param name="color">The color of the piece.</param>
        public Piece(int id, Player player, PieceColor color)
        {
            this.Id = id;
            this.Owner = player;
            this.Color = color;
        }

        /// <summary>
        /// Sets the reference to the current location of this piece.
        /// </summary>
        /// <param name="square">The current square this piece is at.</param>
        public void SetBoardSquareReference(BoardSquare square)
        {
            Square = square;
        }
    }
}
