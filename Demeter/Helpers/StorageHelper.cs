using System;

namespace Demeter.Helpers
{
    /// <summary>
    ///  Helper for storage related functionalities
    /// </summary>
    public static class StorageHelper
    {
        #region Fields

        /// <summary>
        ///  Typical use by freshness
        /// </summary>
        public const double UseByFreshness = 0.5;

        /// <summary>
        ///  Typical best before freshness
        /// </summary>
        public const double BestBeforeFreshness = 0.7;

        #endregion

        #region Methods

        /// <summary>
        ///  Gets the decay rate based on the expiration information
        /// </summary>
        /// <param name="time">The time to take to go from fresh (1) to the specified freshness</param>
        /// <param name="thresholdFreshness">The freshness it gets to in the specified time</param>
        /// <returns>The decay rate</returns>
        public static double GetDecayRateFromExpiration(TimeSpan time, double thresholdFreshness)
        {
            var days = time.TotalDays;
            return Math.Pow(thresholdFreshness, 1/days);
        }

        #endregion
    }
}
