using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FelliGame.UI.Entities
{
    /// <summary>
    /// A UI Element representative of a row in a Felli Game Board.
    /// </summary>
    public class UIFelliRow : UIElement
    {
        /// <summary>
        /// The game board's row.
        /// </summary>
        private readonly BoardRow row;

        /// <summary>
        /// The row's squares.
        /// </summary>
        private readonly UIFelliSquare[] squares;

        /// <summary>
        /// The spacing between squares.
        /// </summary>
        private readonly int squareSpacing;

        public override int Width
        {
            get 
            {
                int value = (squares.Length * squares[0].Width);

                value += (squareSpacing * squares[0].Width) * (squares.Length - 1);

                return value; 
            }
        }

        public override int Height
        {
            get { return squares[0].Height; }
        }

        /// <summary>
        /// Creates a new instance of <see cref="UIFelliRow"/>.
        /// </summary>
        /// <param name="name">The name given to the element. This is basically an ID.</param>
        /// <param name="position">The anchor position of this element.</param>
        /// <param name="row">The game board's row.</param>
        /// <param name="squareSpacing">The spacing between squares.</param>
        /// <param name="colorBg">The background color of the element.</param>
        /// <param name="colorFg">The foreground color of the element.</param>
        public UIFelliRow(string name, UIPosition position, BoardRow row, 
            int squareSpacing, ConsoleColor colorBg = UISettings.ColorBoardRowBg,
            ConsoleColor colorFg = UISettings.ColorConsoleFg) 
            : base(name, position, colorBg, colorFg)
        {
            this.row = row;

            this.squares = new UIFelliSquare[row.Squares.Length];

            this.squareSpacing = squareSpacing;

            for (int i = 0; i < row.Squares.Length; i++)
            {
                this.squares[i] = new UIFelliSquare($"felliSquare{i}", 
                                        this.TopLeft, row.Squares[i]);
            }
        }

        public override void Display()
        {
            ResetCursorPosition();

            DrawBackground();

            ResetCursorPosition();

            ShowSquares();

            base.Display();
        }

        /// <summary>
        /// Get squares in this row that have a piece.
        /// </summary>
        /// <returns>Array of squares in this row with a piece.</returns>
        public UIFelliSquare[] GetSquaresWithPieces()
        {
            return this.squares.Where(square => square.HasPiece).ToArray();
        }

        /// <summary>
        /// Get square by index.
        /// </summary>
        /// <param name="index">Index of the square.</param>
        /// <returns>The square at the index.</returns>
        public UIFelliSquare GetSquare(int index)
        {
            return this.squares[index];
        }

        /// <summary>
        /// Shows this row's squares.
        /// </summary>
        private void ShowSquares()
        {
            UIPosition deltaPosition;

            // First square ignores spacing
            for (int i = 0, spaceMult = 0; i < squares.Length; i++, spaceMult = 1)
            {
                int spacing = squares[i].Width * squareSpacing * spaceMult * i;

                squares[i].SetPosition(this.TopLeft);

                deltaPosition = new UIPosition(squares[i].Width * i + spacing, 0);

                squares[i].Move(deltaPosition);

                squares[i].Display();
            }
        }
    }
}
