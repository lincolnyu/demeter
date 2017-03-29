using System;

namespace Demeter.Abstract
{
    public interface IProcessingContext
    {
        /// <summary>
        ///  Input to specify boxed time and output to provide actual time used in the processing
        /// </summary>
        TimeSpan? Time { get; set; }

        /// <summary>
        ///  Description of the resutl of the processing
        /// </summary>
        string Result { get; set; }
    }
}
