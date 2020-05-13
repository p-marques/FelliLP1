using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FelliGame
{
    /// <summary>
    /// Struct to store game options. Also responsible for parsing arguments.
    /// </summary>
    public struct Options
    {
        /// <summary>
        /// A list will the available options
        /// </summary>
        private static readonly IList<string> validOptions;

        /// <summary>
        /// Messages to be displayed if the user asks for help.
        /// </summary>
        public static string[] HelpMessages { get; }

        /// <summary>
        /// The first player's name.
        /// </summary>
        public string PlayerAName { get; private set; }

        /// <summary>
        /// The second player's name.
        /// </summary>
        public string PlayerBName { get; private set; }

        /// <summary>
        /// The result of the parser.
        /// </summary>
        public OptionsParserResult ParserResult { get; private set; }

        /// <summary>
        /// A collection of error messages describing any errors found during parsing.
        /// </summary>
        public IList<string> ErrorMessages { get; private set; }

        static Options()
        {
            validOptions = new List<string>() { "-p1", "-p2" };
            HelpMessages = new string[2]
            {
                "-p1:  The name of Player 1. Must have length between 2 and 15. Default = Player <color>.",
                "-p2:  The name of Player 2. Must have length between 2 and 15. Default = Player <color>."
            };
        }

        /// <summary>
        /// Parses arguments, creates a <see cref="Options"/> object from
        /// the parsed data and returns the object.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>A <see cref="Options"/> object containing the game options.</returns>
        public static Options ParseOptions(string[] args)
        {
            Options op = new Options();

            // If player asked for help just get out.
            if (args.Contains("-h"))
            {
                op.ParserResult = OptionsParserResult.Help;
                return op;
            }

            IDictionary<string, string> optionsValues = new Dictionary<string, string>();

            optionsValues["-p1"] = "Player A";
            optionsValues["-p2"] = "Player B";

            // For clarity since default value is 0 (Ok)
            op.ParserResult = OptionsParserResult.Ok;

            IList<string> parsedOptions = new List<string>();

            for (int i = 0; i < args.Length; i += 2)
            {
                // Is valid option?
                if (validOptions.Contains(args[i]))
                {
                    // As this option already been validated before?
                    if (parsedOptions.Contains(args[i]))
                    {
                        op.SetErroState($"Error! Repeated argument: {args[i]}");
                    }
                    else
                    {
                        parsedOptions.Add(args[i]);

                        if (args.Length > i + 1)
                        {
                            optionsValues[args[i]] = args[i + 1];
                        }
                        else
                        {
                            op.SetErroState($"Error! No value provided for argument: {args[i]}");
                        }
                    }
                }
                else
                {
                    op.SetErroState($"Error! Unknown argument: {args[i]}");
                }
            }

            op.PlayerAName = optionsValues[validOptions[0]];
            op.PlayerBName = optionsValues[validOptions[1]];

            op.ValidateOptionsValues();

            return op;
        }

        /// <summary>
        /// Validates values given by the user.
        /// If not valid calls <see cref="SetErroState(string)"/>.
        /// </summary>
        private void ValidateOptionsValues()
        {
            // Player names must have length between 2 and 12
            if (PlayerAName.Length < 2 || PlayerAName.Length > 15)
            {
                SetErroState("Error! String for argument \"-p1\" must have a length between 2 and 15.");
                return;
            }

            // Player names must have length between 2 and 12
            if (PlayerBName.Length < 2 || PlayerBName.Length > 15)
            {
                SetErroState("Error! String for argument \"-p2\" must have a length between 2 and 15.");
                return;
            }
        }

        /// <summary>
        /// Internal method to set error state while adding a message.
        /// </summary>
        /// <param name="msg">Message that describes what went wrong.</param>
        private void SetErroState(string msg)
        {
            ParserResult = OptionsParserResult.Error;

            if (ErrorMessages == null)
            {
                ErrorMessages = new List<string>();
            }

            ErrorMessages.Add(msg);
        }
    }
}
