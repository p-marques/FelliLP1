using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame.UI.Entities
{
    /// <summary>
    /// A UI Element representative of a Text Box.
    /// </summary>
    public class UIText : UIElement
    {
        /// <summary>
        /// The padding to be applied above the content.
        /// </summary>
        private readonly int topPadding;

        /// <summary>
        /// The padding to be applied below the content.
        /// </summary>
        private readonly int bottomPadding;

        /// <summary>
        /// The content's background color.
        /// </summary>
        private readonly ConsoleColor contentColorBg;

        /// <summary>
        /// The content to be displayed.
        /// </summary>
        public string Content { get; set; }

        public override int Width => Content.Length;

        public override int Height => topPadding + bottomPadding + 1;

        /// <summary>
        /// Created a new instance of <see cref="UIText"/>.
        /// </summary>
        /// <param name="name">The name given to the element. 
        /// This is basically an ID.</param>
        /// <param name="position">The anchor position of this element.</param>
        /// <param name="content">The content to be displayed.</param>
        /// <param name="topPadding">The padding to be applied above the 
        /// content.</param>
        /// <param name="bottomPadding">The padding to be applied below the 
        /// content.</param>
        /// <param name="colorBg">The background color of the element.</param>
        /// <param name="colorFg">The foreground color of the element.</param>
        /// <param name="colorContentBg">The content's background color.</param>
        public UIText(string name, UIPosition position, string content, 
            int topPadding, int bottomPadding, ConsoleColor colorBg, 
            ConsoleColor colorFg, ConsoleColor colorContentBg) 
            : base(name, position, colorBg, colorFg)
        {
            this.Content = content;

            this.topPadding = topPadding;
            this.bottomPadding = bottomPadding;
            this.contentColorBg = colorContentBg;
        }

        public override void Display()
        {
            ResetCursorPosition();

            DrawBackground();

            SetCursorDelta(0, topPadding);

            WriteContent();

            base.Display();
        }

        /// <summary>
        /// Outputs the content to the console.
        /// </summary>
        private void WriteContent()
        {
            Console.BackgroundColor = this.contentColorBg;
            Console.ForegroundColor = ForegroundColor;

            Console.Write(Content);
        }
    }
}
