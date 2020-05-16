using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame.UI.Entities
{
    /// <summary>
    /// A UI Element representative of a square in a Felli Game Board.
    /// </summary>
    public class UIFelliSquare : UIElement
    {
        /// <summary>
        /// The char valoue for a piece.
        /// </summary>
        private const char pieceChar = '\u25CF';

        /// <summary>
        /// The horizontal size of the square.
        /// </summary>
        private const int squareSize = 5;

        /// <summary>
        /// The padding to be applied to the piece.
        /// </summary>
        private const int padding = 1;
        
        /// <summary>
        /// The game's square.
        /// </summary>
        public BoardSquare Square { get; }

        /// <summary>
        /// Flag representative of presence of a piece in the square.
        /// </summary>
        public bool HasPiece => Square.HasPiece;

        /// <summary>
        /// The piece at this square.
        /// </summary>
        public Piece Piece => Square.Piece;

        public override int Width => squareSize;

        public override int Height => padding * 2 + 1;

        /// <summary>
        /// Creates a new instance of <see cref="UIFelliSquare"/>.
        /// </summary>
        /// <param name="name">The name given to the element. 
        /// This is basically an ID.</param>
        /// <param name="position">The anchor position of this element.</param>
        /// <param name="square">The game's square.</param>
        /// <param name="colorBg">The background color of the element.</param>
        /// <param name="colorFg">The foreground color of the element.</param>
        /// <param name="colorHoverBg">The background color of the element 
        /// when hovered.</param>
        public UIFelliSquare(string name, UIPosition position, BoardSquare square, 
            ConsoleColor colorBg = UISettings.ColorBoardSquareBg, 
            ConsoleColor colorFg = UISettings.ColorBoardSquareFg,
            ConsoleColor colorHoverBg = UISettings.ColorBoardSquareHoverBg) 
            : base(name, position, colorBg, colorFg, colorHoverBg)
        {
            this.Square = square;
        }

        public override void Display()
        {
            ResetCursorPosition();

            DrawBackground();

            ResetCursorPosition();

            DrawPiece();

            base.Display();
        }

        /// <summary>
        /// Draws the game piece on the console.
        /// </summary>
        private void DrawPiece()
        {
            if (HasPiece)
            {
                SetCursorDelta((int)(squareSize / 2), padding);

                Console.ForegroundColor = GetPieceConsoleColor(Square.Piece.Color);

                Console.Write(pieceChar);
            }
        }

        /// <summary>
        /// Get piece console color by it's defined color.
        /// </summary>
        /// <param name="color">It's defined color.</param>
        /// <returns>The console color of the piece.</returns>
        private ConsoleColor GetPieceConsoleColor(PieceColor color)
        {
            ConsoleColor value = ConsoleColor.Black;

            if (color == PieceColor.White)
            {
                value = ConsoleColor.White;
            }

            return value;
        }
    }
}
