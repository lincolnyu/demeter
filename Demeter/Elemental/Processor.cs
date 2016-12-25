using System;

namespace Demeter.Elemental
{
    /// <summary>
    ///  An entity that actions on manager by altering items
    /// </summary>
    public abstract class Processor
    {
        #region Properties

        /// <summary>
        ///  If the processor is currently inprogress (after Process() is called and before Output() is called)
        /// </summary>
        public bool InProgress { get; protected set; }

        /// <summary>
        ///  The manager it works with
        /// </summary>
        public KitchenManager Manager { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///  Takes whatever is available from the manager, conducts the processing and returns the duration
        /// </summary>
        /// <returns>The duration the process takes</returns>
        public abstract TimeSpan Process();

        /// <summary>
        ///  Outputs back to the manager
        /// </summary>
        public abstract void Output();

        #endregion
    }
}
