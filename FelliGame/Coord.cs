using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame
{
    /// <summary>
    /// A struct capable of holding the position of a board square on 
    /// the game board.
    /// </summary>
    public struct Coord
    {
        /// <summary>
        /// The row on the board.
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// The column on the board.
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="row">The row on the board.</param>
        /// <param name="column">The column on the board.</param>
        public Coord(int row, int column)
        {
            Column = column;
            Row = row;
        }

        /// <summary>
        /// + operator.
        /// </summary>
        /// <param name="a"><see cref="Coord"/> a.</param>
        /// <param name="b"><see cref="Coord"/> b.</param>
        /// <returns>The result of a + b.</returns>
        public static Coord operator +(Coord a, Coord b) => 
                                new Coord(a.Row + b.Row, a.Column + b.Column);
    }
}
