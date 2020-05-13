using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame
{
    /// <summary>
    /// Class responsible for managing the entire game's flow.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The game's options.
        /// </summary>
        private readonly Options gameOptions;

        /// <summary>
        /// Creates a new instance of <see cref="Game"/>.
        /// </summary>
        /// <param name="options">The game's options.</param>
        public Game(Options options)
        {
            gameOptions = options;
        }

        /// <summary>
        /// The main play loop.
        /// </summary>
        public void Play()
        {
            bool playing = true;

            while (playing)
            {
                playing = false;
            }
        }
    }
}
