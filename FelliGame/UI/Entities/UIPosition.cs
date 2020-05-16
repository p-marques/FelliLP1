using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame.UI.Entities
{
    /// <summary>
    /// Defines a position on the console.
    /// </summary>
    public readonly struct UIPosition
    {
        /// <summary>
        /// Position in relation to the left of the console.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Position in relation to the top of the console.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Creates a new position on the console.
        /// </summary>
        /// <param name="x">Position in relation to the left of the 
        /// console.</param>
        /// <param name="y">Position in relation to the top of the 
        /// console.</param>
        public UIPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// + operator.
        /// </summary>
        /// <param name="a"><see cref="UIPosition"/> a.</param>
        /// <param name="b"><see cref="UIPosition"/> b.</param>
        /// <returns>New <see cref="UIPosition"/> that is the result 
        /// of a + b.</returns>
        public static UIPosition operator +(UIPosition a, UIPosition b) 
            => new UIPosition(a.X + b.X, a.Y + b.Y);

        /// <summary>
        /// - operator.
        /// </summary>
        /// <param name="a"><see cref="UIPosition"/> a.</param>
        /// <param name="b"><see cref="UIPosition"/> b.</param>
        /// <returns>New <see cref="UIPosition"/> that is the result 
        /// of a - b.</returns>
        public static UIPosition operator -(UIPosition a, UIPosition b)
            => new UIPosition(a.X - b.X, a.Y - b.Y);
    }
}
