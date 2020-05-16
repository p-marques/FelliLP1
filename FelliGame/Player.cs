﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame
{
    /// <summary>
    /// A player.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Number of pieces that the player starts with.
        /// </summary>
        private const int pieceCount = 6;

        /// <summary>
        /// The name of the player.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The player's pieces.
        /// </summary>
        public Piece[] Pieces { get; private set; }

        /// <summary>
        /// The color of the player's pieces.
        /// </summary>
        public PieceColor PiecesColor { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="Player"/>.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        public Player(string name)
        {
            this.Name = name;

            this.Pieces = new Piece[pieceCount];
        }

        /// <summary>
        /// Creates the pieces.
        /// </summary>
        /// <param name="color">The color of the pieces.</param>
        public void CreatePieces(PieceColor color)
        {
            for (int i = 0; i < pieceCount; i++)
            {
                this.Pieces[i] = new Piece(i, this, color);
            }

            PiecesColor = color;

            UpdateName();
        }

        /// <summary>
        /// Updates the name of the player using the piece's color if it
        /// wasn't set by command line argument.
        /// </summary>
        /// <param name="color">The color </param>
        private void UpdateName()
        {
            if (Name == "Player A" || Name == "Player B")
            {
                switch (PiecesColor)
                {
                    case PieceColor.Black:
                        Name = "Blacks Player";
                        break;
                    case PieceColor.White:
                        Name = "Whites Player";
                        break;
                }
            }
        }
    }
}
