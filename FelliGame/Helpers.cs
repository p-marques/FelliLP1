using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FelliGame
{
    /// <summary>
    /// Static class with a collection of helper functions.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Get values of given enum.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <returns>Array with all values in enum.</returns>
        public static T[] GetEnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        }
    }
}
