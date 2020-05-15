using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FelliGame.UI.Entities
{
    /// <summary>
    /// A UI Element representative of a Title.
    /// </summary>
    public class UITitle : UIElement
    {
        /// <summary>
        /// The width of the left border.
        /// </summary>
        private readonly int leftBorderWidth;

        /// <summary>
        /// The left/right padding.
        /// </summary>
        private readonly int leftRightPadding;

        /// <summary>
        /// The top/bottom padding.
        /// </summary>
        private readonly int topBottomPadding;

        /// <summary>
        /// The text to be displayed.
        /// </summary>
        private readonly string text;

        /// <summary>
        /// The color of the border.
        /// </summary>
        private readonly ConsoleColor borderColor;

        public override int Width 
        { 
            get { return text.Length + leftRightPadding * 2 + leftBorderWidth; }
        }

        public override int Height 
        { 
            get { return topBottomPadding * 2 + 1; }
        }

        /// <summary>
        /// Creates a new instance of <see cref="UITitle"/>.
        /// </summary>
        /// <param name="name">The name given to the element. 
        /// This is basically an ID.</param>
        /// <param name="position">The anchor position of this element.</param>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="leftRightPadding">The left/right padding.</param>
        /// <param name="topBottomPadding">The top/bottom padding.</param>
        /// <param name="leftBorderWith">The width of the left border.</param>
        /// <param name="colorBg">The background color of the element.</param>
        /// <param name="colorFg">The foreground color of the element.</param>
        /// <param name="colorBorder">The color of the border.</param>
        public UITitle(string name, UIPosition position, string text, 
            int leftRightPadding, int topBottomPadding, int leftBorderWith = 1,
            ConsoleColor colorBg = UISettings.ColorTitleBg, 
            ConsoleColor colorFg = UISettings.ColorTitleFg,
            ConsoleColor colorBorder = UISettings.ColorTitleBorder) 
            : base(name, position, colorBg, colorFg)
        {
            this.leftBorderWidth = leftBorderWith;
            this.leftRightPadding = leftRightPadding;
            this.topBottomPadding = topBottomPadding;
            this.text = text;
            this.borderColor = colorBorder;
        }

        public override void Display()
        {
            ResetCursorPosition();

            DrawBackground();

            ResetCursorPosition();

            DrawBorder();

            ResetCursorPosition();

            SetCursorDelta(leftBorderWidth + leftRightPadding, topBottomPadding);

            WriteText();

            base.Display();
        }

        /// <summary>
        /// Draws the border on the console.
        /// </summary>
        private void DrawBorder()
        {
            if (leftBorderWidth == 0)
            {
                return;
            }

            Console.BackgroundColor = borderColor;

            for (int k = 0; k < Height; k++, NewLine())
            {
                SetCursorDelta(0, k);

                for (int i = 0; i < leftBorderWidth; i++)
                {
                    Console.Write(" ");
                }
            }
        }

        /// <summary>
        /// Outputs the text to the console.
        /// </summary>
        private void WriteText()
        {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;

            Console.Write(text);
        }
    }
}
