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
        /// Shows all rows.
        /// </summary>
        private void ShowRows()
        {
            UIPosition delta;

            for (int i = 0; i < rows.Length; i++)
            {
                int x = (this.Width - rows[i].Width) / 2;

                rows[i].SetPosition(this.TopLeft);

                delta = new UIPosition(x, i * rows[i].Height);

                rows[i].Move(delta);

                rows[i].Display();
            }
        }
    }
}
