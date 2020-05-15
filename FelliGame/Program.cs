using FelliGame.UI;
using System;

namespace FelliGame
{
    class Program
    {
        /// <summary>
        /// The UI Manager. Responsible for keeping the UI updated.
        /// </summary>
        public static ConsoleUI UIManager { get; private set; }

        /// <summary>
        /// Program stating point.
        /// </summary>
        /// <param name="args">Optional arguments. 
        /// Allowed arguments: -h, -p1, -p2.</param>
        static void Main(string[] args)
        {
            // Handle arguments
            Options options = Options.ParseOptions(args);

            // Something went wrong parsing arguments?
            if (options.ParserResult == OptionsParserResult.Error)
            {
                string errorsTitle;

                // Error or Errors?
                errorsTitle = options.ErrorMessages.Count == 1 ? 
                    "Error found:" : "Errors found:";

                Console.WriteLine(errorsTitle);

                for (int i = 0; i < options.ErrorMessages.Count; i++)
                {
                    Console.WriteLine($"\t{options.ErrorMessages[i]}");
                }
            }
            // Player asked for the help messages ?
            else if (options.ParserResult == OptionsParserResult.Help)
            {
                Console.WriteLine("Available arguments:");

                for (int i = 0; i < Options.HelpMessages.Length; i++)
                {
                    Console.WriteLine($"\t{Options.HelpMessages[i]}");
                }
            }
            else
            {
                // Create instance of the UI Manager
                UIManager = new ConsoleUI();

                // Creates game instance
                Game game = new Game(options);

                // Start playing
                game.Play();
            }
        }
    }
}
