using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame
{
    /// <summary>
    /// A player.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// The name of the player.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="Player"/>.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        public Player(string name)
        {
            this.Name = name;
        }
    }
}
