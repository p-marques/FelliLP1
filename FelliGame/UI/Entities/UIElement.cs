using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame.UI.Entities
{
    /// <summary>
    /// An abstract representation of a UI element.
    /// </summary>
    public abstract class UIElement
    {
        /// <summary>
        /// The background color of the element.
        /// </summary>
        protected readonly ConsoleColor colorBackground;

        /// <summary>
        /// The foreground color of the element.
        /// </summary>
        protected readonly ConsoleColor colorForeground;

        /// <summary>
        /// The background color of the element when hovered.
        /// </summary>
        protected readonly ConsoleColor colorBackgroundHover;

        /// <summary>
        /// The foreground color of the element when hovered.
        /// </summary>
        protected readonly ConsoleColor colorForegroundHover;

        /// <summary>
        /// The name given to the element. This is basically an ID.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The element's top left position.
        /// This represents the element's anchor point.
        /// </summary>
        public UIPosition TopLeft { get; private set; }

        /// <summary>
        /// The element's top right position.
        /// This represents the first available position outside of the element.
        /// </summary>
        public UIPosition TopRight 
        { 
            get { return new UIPosition(TopLeft.X + Width + 1, TopLeft.Y); }
        }

        /// <summary>
        /// The element's bottom left position.
        /// This represents the first available position outside of the element.
        /// </summary>
        public UIPosition BottomLeft
        {
            get { return new UIPosition(TopLeft.X, TopLeft.Y + Height); }
        }

        /// <summary>
        /// The element's bottom right position.
        /// This represents the first available position outside of the element.
        /// </summary>
        public UIPosition BottomRight
        {
            get { return new UIPosition(TopLeft.X + Width, TopLeft.Y + Height); }
        }

        /// <summary>
        /// The element's bottom middle position.
        /// This represents the first available position outside of the element.
        /// </summary>
        public UIPosition BottomMiddle 
        { 
            get { return new UIPosition(TopLeft.X + (int)(Width / 2), TopLeft.Y + Height); }
        }

        /// <summary>
        /// Flag representing if element is hovered.
        /// </summary>
        public bool IsHovered { get; set; }

        /// <summary>
        /// Flag representing if element is centered.
        /// </summary>
        public bool IsCentered { get; set; }

        /// <summary>
        /// The current background color of the element.
        /// </summary>
        public ConsoleColor BackgroundColor
        {
            get { return IsHovered ? colorBackgroundHover : colorBackground; }
        }

        /// <summary>
        /// The current foreground color of the element.
        /// </summary>
        public ConsoleColor ForegroundColor
        {
            get { return IsHovered ? colorForegroundHover : colorForeground; }
        }

        /// <summary>
        /// The width of the element.
        /// </summary>
        public abstract int Width { get; }

        /// <summary>
        /// The height of the element.
        /// </summary>
        public abstract int Height { get; }

        /// <summary>
        /// Base constructor.
        /// </summary>
        /// <param name="name">The name given to the element. This is basically an ID.</param>
        /// <param name="position">The anchor position of this element.</param>
        /// <param name="colorBg">The background color of the element.</param>
        /// <param name="colorFg">The foreground color of the element.</param>
        /// <param name="colorHoverBg">The background color of the element when hovered.</param>
        /// <param name="colorHoverFg">The foreground color of the element when hovered.</param>
        public UIElement(string name, UIPosition position, ConsoleColor colorBg, ConsoleColor colorFg, 
            ConsoleColor colorHoverBg = UISettings.ColorConsoleBg, ConsoleColor colorHoverFg = UISettings.ColorConsoleFg)
        {
            this.Name = name;

            this.TopLeft = position;

            this.colorBackground = colorBg;

            this.colorForeground = colorFg;

            this.colorBackgroundHover = colorHoverBg;

            this.colorForegroundHover = colorHoverFg;
        }

        /// <summary>
        /// Sets the anchor position of the element.
        /// </summary>
        /// <param name="newPosition">The new anchor position.</param>
        public void SetPosition(UIPosition newPosition)
        {
            this.TopLeft = newPosition;
        }

        /// <summary>
        /// Moves the anchor point.
        /// </summary>
        /// <param name="delta">A <see cref="UIPosition"/> representative of the desired move delta.</param>
        public void Move(UIPosition delta)
        {
            this.TopLeft += delta;
        }

        /// <summary>
        /// Outputs the element to the console.
        /// </summary>
        public abstract void Display();

        /// <summary>
        /// Resets the cursor position to the element's anchor point.
        /// </summary>
        protected void ResetCursorPosition()
        {
            Console.CursorLeft = TopLeft.X;
            Console.CursorTop = TopLeft.Y;
        }

        /// <summary>
        /// Correctly moves to a new line using anchor point.
        /// </summary>
        protected void NewLine()
        {
            Console.CursorLeft = TopLeft.X;
            Console.CursorTop = TopLeft.Y + 1;
        }

        /// <summary>
        /// Draws the background of the element.
        /// This uses the <see cref="Width"/> and <see cref="Height"/> of the element.
        /// </summary>
        protected void DrawBackground()
        {
            Console.BackgroundColor = BackgroundColor;

            for (int k = 0; k < Height; k++, NewLine())
            {
                SetCursorDelta(0, k);

                for (int i = 0; i < Width; i++)
                {
                    Console.Write(" ");
                }
            }
        }

        /// <summary>
        /// Sets the cursor position using the element's anchor point as base.
        /// </summary>
        /// <param name="leftDelta">The delta value for <see cref="Console.CursorLeft"/>.</param>
        /// <param name="topDelta">The delta value for <see cref="Console.CursorTop"/>.</param>
        protected void SetCursorDelta(int leftDelta, int topDelta)
        {
            Console.CursorLeft = TopLeft.X + leftDelta;
            Console.CursorTop = TopLeft.Y + topDelta;
        }

        /// <summary>
        /// Performs some simple cleanup:
        /// <list type="bullet">
        /// <item>Sets console colors to default;</item>
        /// <item>Sets cursor position to <see cref="BottomRight"/>.</item>
        /// </list>
        /// </summary>
        protected void DisplayCleanup()
        {
            Console.BackgroundColor = UISettings.ColorConsoleBg;
            Console.ForegroundColor = UISettings.ColorConsoleFg;

            Console.CursorLeft = BottomRight.X;
            Console.CursorTop = BottomRight.Y;
        }

        /// <summary>
        /// Gets user input key.
        /// </summary>
        /// <returns>The key hit by the user.</returns>
        protected ConsoleKey GetUserInputKey()
        {
            Console.ForegroundColor = UISettings.ColorConsoleBg;

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            return keyInfo.Key;
        }
    }
}
