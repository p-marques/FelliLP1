using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame.UI
{
    /// <summary>
    /// Class responsible for managing the user interface.
    /// </summary>
    public class ConsoleUI
    {
        /// <summary>
        /// Creates a new instance of <see cref="ConsoleUI"/>.
        /// </summary>
        public ConsoleUI()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.CursorVisible = false;
        }
    }
}
