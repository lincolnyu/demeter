using System;

namespace Demeter.Elemental
{
    /// <summary>
    ///  A task
    /// </summary>
    public class Task
    {
        #region Properties

        /// <summary>
        ///  The processor scheduled
        /// </summary>
        public Processor Processor { get; set; }

        /// <summary>
        ///  Scheduled time
        /// </summary>
        public DateTime Time { get; set; }

        #endregion
    }
}
