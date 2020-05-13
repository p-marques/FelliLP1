using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame
{
    /// <summary>
    /// Enum defining parser results.
    /// </summary>
    public enum OptionsParserResult
    {
        /// <summary>
        /// Everything ok.
        /// </summary>
        Ok,

        /// <summary>
        /// Errors found.
        /// </summary>
        Error,

        /// <summary>
        /// Player asked for help.
        /// </summary>
        Help
    }
}
