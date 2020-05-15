using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame.UI
{
    /// <summary>
    /// The default settings for the UI.
    /// </summary>
    public struct UISettings
    {
        /// <summary>
        /// The default background color for the console.
        /// </summary>
        public const ConsoleColor ColorConsoleBg = ConsoleColor.Gray;

        /// <summary>
        /// The default foreground color for the console.
        /// </summary>
        public const ConsoleColor ColorConsoleFg = ConsoleColor.Black;

        /// <summary>
        /// The default left margin for the screen.
        /// </summary>
        public const int ScreenMarginLeft = 1;

        /// <summary>
        /// The default top margin for the screen.
        /// </summary>
        public const int ScreenMarginTop = 1;
    }
}
