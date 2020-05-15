using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FelliGame.UI.Entities
{
    /// <summary>
    /// A UI Element representative of a Button.
    /// </summary>
    public class UIButton : UIElement
    {
        /// <summary>
        /// The ammount of padding to the left and right.
        /// </summary>
        private readonly int leftRightPadding;

        /// <summary>
        /// The ammount of padding to the top and bottom.
        /// </summary>
        private readonly int topBottomPadding;

        /// <summary>
        /// The text to be displayed.
        /// </summary>
        private readonly string text;

        public override int Width 
        { 
            get { return text.Length + leftRightPadding * 2; }
        }

        public override int Height 
        {
            get { return topBottomPadding * 2 + 1; }
        }

        /// <summary>
        /// Creates a new instance of <see cref="UIButton"/>.
        /// </summary>
        /// <param name="name">The name given to the element. 
        /// This is basically an ID.</param>
        /// <param name="position">The anchor position of this element.</param>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="leftRightPadding">The ammount of padding 
        /// to the left and right.</param>
        /// <param name="topBottomPadding">The ammount of padding 
        /// to the top and bottom.</param>
        /// <param name="colorBg">The background color of the button.</param>
        /// <param name="colorFg">The foreground color of the button.</param>
        /// <param name="colorHoverBg">The background color of the button 
        /// when hovered.</param>
        /// <param name="colorHoverFg">The foreground color of the button 
        /// when hovered.</param>
        public UIButton(string name, UIPosition position, string text, 
            int leftRightPadding = 1, int topBottomPadding = 1,
            ConsoleColor colorBg = UISettings.ColorButtonBg, 
            ConsoleColor colorFg = UISettings.ColorButtonFg,
            ConsoleColor colorHoverBg = UISettings.ColorButtonHoverBg,
            ConsoleColor colorHoverFg = UISettings.ColorButtonHoverFg) 
            : base(name, position, colorBg, colorFg, colorHoverBg, colorHoverFg)
        {
            this.text = text;
            this.leftRightPadding = leftRightPadding;
            this.topBottomPadding = topBottomPadding;
        }

        public override void Display()
        {
            ResetCursorPosition();

            DrawBackground();

            ResetCursorPosition();

            SetCursorDelta(leftRightPadding, topBottomPadding);

            WriteText();

            base.Display();
        }

        /// <summary>
        /// Writes the value in <see cref="text"/> to the console.
        /// </summary>
        private void WriteText()
        {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;

            Console.Write(text);
        }
    }
}
