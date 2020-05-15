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
        /// The default background color for a button.
        /// </summary>
        public const ConsoleColor ColorButtonBg = ConsoleColor.Black;

        /// <summary>
        /// The default foreground color for a button.
        /// </summary>
        public const ConsoleColor ColorButtonFg = ConsoleColor.White;

        /// <summary>
        /// The default background color for a hovered button.
        /// </summary>
        public const ConsoleColor ColorButtonHoverBg = ConsoleColor.Black;

        /// <summary>
        /// The default foreground color for a hovered button.
        /// </summary>
        public const ConsoleColor ColorButtonHoverFg = ConsoleColor.White;

        /// <summary>
        /// The default background color for a dialog.
        /// </summary>
        public const ConsoleColor ColorDialogBg = ConsoleColor.White;

        /// <summary>
        /// The default foreground color for a dialog.
        /// </summary>
        public const ConsoleColor ColorDialogFg = ConsoleColor.Black;

        /// <summary>
        /// The default foreground color for a dialog title.
        /// </summary>
        public const ConsoleColor ColorDialogTitleFg = ConsoleColor.Blue;

        /// <summary>
        /// The default background color for a dialog button.
        /// </summary>
        public const ConsoleColor ColorDialogButtonBg = ConsoleColor.White;

        /// <summary>
        /// The default foreground color for a hovered button.
        /// </summary>
        public const ConsoleColor ColorDialogButtonFg = ConsoleColor.Blue;

        /// <summary>
        /// The default background color for a hovered dialog button.
        /// </summary>
        public const ConsoleColor ColorDialogButtonHoverBg = ConsoleColor.Blue;

        /// <summary>
        /// The default foreground color for a hovered dialog button.
        /// </summary>
        public const ConsoleColor ColorDialogButtonHoverFg = ConsoleColor.White;

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
