using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Moves a piece.
        /// </summary>
        /// <param name="move">A description of the move to be perform.</param>
        public void MovePiece(BoardMove move)
        {
            move.Destination.PutPiece(move.Piece);

            if (move.IsEating)
            {
                RemovePiece(move.PieceEaten);
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

        /// <summary>
        /// Get all possible moves by a given piece.
        /// </summary>
        /// <param name="piece">The piece looking to move.</param>
        /// <returns>Array with the possible moves.</returns>
        public BoardMove[] GetPossibleMoves(Piece piece)
        {
            Piece eatenPiece;
            BoardSquare holder;
            BoardMove move;
            Direction[] directions = GetAvailableDirections(piece.Square.Pos);
            IList<BoardMove> possibleMoves = new List<BoardMove>();

            // For all the available directions
            for (int i = 0; i < directions.Length; i++)
            {
                // Get square in the current direction
                holder = GetBoardSquareByDirection(piece.Square, directions[i]);

                if (holder != null)
                {
                    // If there's no piece there...
                    if (!holder.HasPiece)
                    {
                        // ...this move is possible.
                        move = new BoardMove(piece, holder, null);
                        possibleMoves.Add(move);
                    }
                    // If there is a piece there but it's from the other player
                    else if (holder.Piece.Color != piece.Color)
                    {
                        eatenPiece = holder.Piece;
                        holder = GetBoardSquareByDirection(holder, directions[i]);

                        // A EAT is possible?
                        if (holder != null && !holder.HasPiece)
                        {
                            move = new BoardMove(piece, holder, eatenPiece);
                            possibleMoves.Add(move);
                        }
                    }
                }
            }

            return possibleMoves.ToArray();
        }

        /// <summary>
        /// Checks if a given piece has any abailable moves.
        /// </summary>
        /// <param name="piece">The piece looking to move.</param>
        /// <returns>A flag representing the availability of moves.</returns>
        public bool GetPieceHasPossibleMoves(Piece piece)
        {
            return GetPossibleMoves(piece).Length > 0;
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
        /// Calculates the new coordinates that a move in a 
        /// given direction would result in.
        /// </summary>
        /// <param name="square">The reference square.</param>
        /// <param name="direction">The desired direction.</param>
        /// <returns></returns>
        private Coord GetNewCoordinates(BoardSquare square, Direction direction)
        {
            Coord delta, result, oldCoords = square.Pos;

            if (oldCoords.Row == 1 || oldCoords.Row == 3)
            {
                if (direction == Direction.DownRight || 
                    direction == Direction.DownLeft)
                {
                    direction = Direction.Down;
                }
                else if (direction == Direction.UpRight || 
                        direction == Direction.UpLeft)
                {
                    direction = Direction.Up;
                }
            }

            switch (direction)
            {
                case Direction.Up:
                    if (oldCoords.Row == 2)
                        delta = new Coord(-1, 1);
                    else
                        delta = new Coord(-1, 0);
                    break;
                case Direction.Down:
                    if (oldCoords.Row == 2)
                        delta = new Coord(1, 1);
                    else
                        delta = new Coord(1, 0);
                    break;
                case Direction.Right:
                    delta = new Coord(0, 1);
                    break;
                case Direction.Left:
                    delta = new Coord(0, -1);
                    break;
                case Direction.UpRight:
                    delta = new Coord(-1, 2);
                    break;
                case Direction.UpLeft:
                    delta = new Coord(-1, 0);
                    break;
                case Direction.DownRight:
                    delta = new Coord(1, 2);
                    break;
                case Direction.DownLeft:
                    delta = new Coord(1, 0);
                    break;
                default:
                    delta = new Coord(0, 0);
                    break;
            }

            result = oldCoords + delta;

            if (oldCoords.Row != 2 && result.Row == 2)
            {
                result = new Coord(2, 0);
            }

            return result;
        }

        /// <summary>
        /// Get square in a given direction of another square.
        /// </summary>
        /// <param name="square">The reference square.</param>
        /// <param name="direction">The desired direction.</param>
        /// <returns>The square at the given direction.
        /// Can be null, meaning no available square exists in that direction.
        /// </returns>
        private BoardSquare GetBoardSquareByDirection(BoardSquare square, 
                                                        Direction direction)
        {
            Coord pos;
            BoardSquare result = null;

            pos = GetNewCoordinates(square, direction);

            if (GetIsPositionValid(pos))
            {
                result = Rows[pos.Row].Squares[pos.Column];
            }

            return result;
        }

        /// <summary>
        /// Get available move directions.
        /// </summary>
        /// <param name="pos">The position of a square.</param>
        /// <returns>Array with available move directions.</returns>
        private Direction[] GetAvailableDirections(Coord pos)
        {
            if (pos.Column == 1 || pos.Row == 0 || pos.Row == 4)
            {
                return new Direction[] { Direction.Up, Direction.Down, 
                                        Direction.Right, Direction.Left };
            }

            if (pos.Row == 1 && pos.Column == 0)
            {
                return new Direction[] { Direction.Up, Direction.DownRight, 
                                            Direction.Right };
            }

            if (pos.Row == 1 && pos.Column == 2)
            {
                return new Direction[] { Direction.Up, Direction.DownLeft, 
                                                        Direction.Left };
            }

            if (pos.Row == 3 && pos.Column == 0)
            {
                return new Direction[] { Direction.Down, Direction.Right, 
                                            Direction.UpRight };
            }

            if (pos.Row == 3 && pos.Column == 2)
            {
                return new Direction[] { Direction.Down, Direction.Left, 
                                                        Direction.UpLeft };
            }

            return Helpers.GetEnumValues<Direction>();
        }

        /// <summary>
		/// Checks if a given position is inside the game's board.
		/// </summary>
		/// <param name="pos">The position to check.</param>
		/// <returns>A boolean value representing the validity of the 
        /// position given.</returns>
		private bool GetIsPositionValid(Coord pos)
        {
            if (pos.Row < 0 || pos.Row >= Rows.Length)
            {
                return false;
            }

            if (pos.Column < 0 || pos.Column >= Rows[pos.Row].Squares.Length)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Removes a piece from the board.
        /// </summary>
        /// <param name="piece"></param>
        private void RemovePiece(Piece piece)
        {
            piece.Owner.RemovePieceById(piece.Id);
        }
    }
}
