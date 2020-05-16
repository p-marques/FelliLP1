using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FelliGame.UI.Entities
{
    /// <summary>
    /// A UI Element representative of a Felli Game Board.
    /// </summary>
    public class UIFelliBoard : UIElement
    {
        /// <summary>
        /// The game's board.
        /// </summary>
        private readonly Board board;

        /// <summary>
        /// The rows of the board.
        /// </summary>
        private readonly UIFelliRow[] rows;

        public override int Width => rows[0].Width;

        public override int Height => rows.Length * rows[0].Height;

        /// <summary>
        /// Creates a new instance of <see cref="UIFelliBoard"/>.
        /// </summary>
        /// <param name="name">The name given to the element. 
        /// This is basically an ID.</param>
        /// <param name="position">The anchor position of this element.</param>
        /// <param name="board">The game's board.</param>
        /// <param name="colorBg">The background color of the element.</param>
        /// <param name="colorFg">The foreground color of the element.</param>
        public UIFelliBoard(string name, UIPosition position, Board board, 
            ConsoleColor colorBg = UISettings.ColorConsoleBg, 
            ConsoleColor colorFg = UISettings.ColorConsoleFg) 
            : base(name, position, colorBg, colorFg)
        {
            this.board = board;

            this.rows = new UIFelliRow[board.Rows.Length];

            for (int i = 0; i < board.Rows.Length; i++)
            {
                // If i is even spacing is 1, else is 0
                int spacing = (i % 2) == 0 ? 1 : 0;

                this.rows[i] = new UIFelliRow($"felliRow{i}", 
                                        this.TopLeft, board.Rows[i], spacing);
            }
        }

        public override void Display()
        {
            ResetCursorPosition();

            DrawBackground();

            ResetCursorPosition();

            ShowRows();

            base.Display();
        }

        /// <summary>
        /// Performs a full play cycle.
        /// </summary>
        /// <param name="player">The player who is playing.</param>
        /// <returns>The desired move.</returns>
        public BoardMove Play(Player player)
        {
            int index;
            UIFelliSquare pieceToMove;
            UIFelliSquare[] availableForSelection;
            BoardMove[] possibleMoves;
            BoardMove selectedMove = new BoardMove();

            while (true)
            {
                // Get all squares that have a piece belonging to the current 
                // player and piece has possible moves.
                availableForSelection = 
                            GetAllSquaresWithPiecesOfColor(player.PiecesColor);

                index = GetPlayerSelectSquare(availableForSelection);

                // If index < 0 that means the player hit escape in piece
                // selection...
                if (index < 0)
                {
                    // ...signaling desire to exit the game.
                    pieceToMove = null;
                    break;
                }

                pieceToMove = availableForSelection[index];

                // Get possible moves by the selected piece.
                possibleMoves = board.GetPossibleMoves(pieceToMove.Piece);

                // Get all possible destinations.
                availableForSelection = GetPossibleDestinations(possibleMoves);

                index = GetPlayerSelectSquare(availableForSelection);

                // If index < 0 that means the player hit escape in destination 
                // selection...
                if (index < 0)
                {
                    pieceToMove.IsHovered = false;

                    // ...signaling desire to go back to piece selection.
                    continue;
                }

                selectedMove = possibleMoves[index];

                pieceToMove.IsHovered = false;
                availableForSelection[index].IsHovered = false;
                break;
            }

            // Returning BoardMove.Piece == null will signal an intention to
            // quit the game.
            return new BoardMove(pieceToMove?.Piece, 
                                selectedMove.Destination, 
                                selectedMove.PieceEaten);
        }

        /// <summary>
        /// Gets the selected index in a collection.
        /// </summary>
        /// <param name="possibilities">The available options.</param>
        /// <returns>An index value representative of the selected item.</returns>
        private int GetPlayerSelectSquare(UIFelliSquare[] possibilities)
        {
            ConsoleKey userInput;
            UIFelliSquare square;
            int selectedIndex = 0;

            while (true)
            {
                square = possibilities[selectedIndex];

                square.IsHovered = true;

                Display();

                userInput = GetUserInputKey();

                if (userInput == ConsoleKey.Enter)
                {
                    break;
                }
                else if (userInput == ConsoleKey.Escape)
                {
                    square.IsHovered = false;

                    Display();

                    // -1 is handled upstream has a cancellation of the action.
                    selectedIndex = -1;

                    break;
                }

                selectedIndex += GetIndexSelectionDelta(userInput);

                // If the index would be outside of the array.
                if (selectedIndex < 0)
                {
                    selectedIndex = possibilities.Length - 1;
                }
                else if (selectedIndex >= possibilities.Length)
                {
                    selectedIndex = 0;
                }

                square.IsHovered = false;
            }

            return selectedIndex;
        }

        /// <summary>
        /// Gets all possible destinations in the given moves.
        /// </summary>
        /// <param name="moves">A collection of available moves.</param>
        /// <returns>A collection of available destinations.</returns>
        private UIFelliSquare[] GetPossibleDestinations(BoardMove[] moves)
        {
            IList<UIFelliSquare> result = new List<UIFelliSquare>();

            for (int i = 0; i < moves.Length; i++)
            {
                result.Add(GetSquare(moves[i].Destination.Pos));
            }

            return result.ToArray();
        }

        /// <summary>
        /// Get square by current position on the board.
        /// </summary>
        /// <param name="pos">The current position of the desired square.</param>
        /// <returns>The square at the given position.</returns>
        private UIFelliSquare GetSquare(Coord pos)
        {
            return rows[pos.Row].GetSquare(pos.Column);
        }

        /// <summary>
        /// Get index selection delta given a inputed key.
        /// </summary>
        /// <param name="key">The key hit by the user.</param>
        /// <returns>The delta value representative of the move along the index.</returns>
        private int GetIndexSelectionDelta(ConsoleKey key)
        {
            int value = 0;

            switch (key)
            {
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    value = 1;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    value = -1;
                    break;
            }

            return value;
        }

        /// <summary>
        /// Get all squares with pieces of a given color.
        /// Filters out squares with pieces without any available moves.
        /// </summary>
        /// <param name="pieceColor"></param>
        /// <returns></returns>
        private UIFelliSquare[] GetAllSquaresWithPiecesOfColor(PieceColor pieceColor)
        {
            IList<UIFelliSquare> squares = new List<UIFelliSquare>();

            for (int i = 0; i < this.rows.Length; i++)
            {
                UIFelliSquare[] piecesInRow = this.rows[i].GetSquaresWithPieces();

                for (int j = 0; j < piecesInRow.Length; j++)
                {
                    bool hasMoves = board.GetPieceHasPossibleMoves(piecesInRow[j].Piece);

                    // Same color and has possible moves
                    if (piecesInRow[j].Piece.Color == pieceColor && hasMoves)
                        squares.Add(piecesInRow[j]);
                }
            }

            return squares.ToArray();
        }

        /// <summary>
        /// Shows all rows.
        /// </summary>
        private void ShowRows()
        {
            UIPosition delta;

            for (int i = 0; i < rows.Length; i++)
            {
                // Center the row
                int x = (this.Width - rows[i].Width) / 2;

                rows[i].SetPosition(this.TopLeft);

                delta = new UIPosition(x, i * rows[i].Height);

                rows[i].Move(delta);

                rows[i].Display();
            }
        }
    }
}
