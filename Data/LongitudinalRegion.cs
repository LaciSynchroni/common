using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaciSynchroni.Common.Data
{
    /// <summary>
    /// Identifies a longitudinal area of planet Earth by local time zone offset from UTC.
    /// </summary>
    public struct LongitudinalRegion
    {
        private const int _dayMinutes = 60 * 24;

        /// <summary>
        /// The number of minutes <em>after</em> UTC for this longitudinal region.
        /// </summary>
        /// <remarks>
        /// Ranges from -719 to 719 minutes (-12 to 12 hours).
        /// </remarks>
        public int UtcOffsetMinutes { get; }

        /// <summary>
        /// Creates a new <see cref="LongitudinalRegion"/> that identifies the region whose time is offset the given number of minutes from UTC.
        /// </summary>
        /// <param name="utcOffsetMinutes">The offset of this region's time from UTC.</param>
        public LongitudinalRegion(int utcOffsetMinutes)
        {
            // Wrap utcOffsetMinutes into the -12 hours to 12 hours range (-720 to 720 minutes)
            utcOffsetMinutes = WrappedMod(utcOffsetMinutes + _dayMinutes / 2, _dayMinutes) - _dayMinutes / 2;

            Debug.Assert(utcOffsetMinutes > -_dayMinutes / 2 && utcOffsetMinutes < _dayMinutes / 2);

            UtcOffsetMinutes = utcOffsetMinutes;
        }

        /// <summary>
        /// Computes the positive distance around the globe between this region and the given other region, in time zone minutes.
        /// </summary>
        /// <param name="other">The region to get the distance from.</param>
        /// <returns></returns>
        public int GetWrappedDistance(LongitudinalRegion other)
        {
            // Wrap the offsets from -12 - 12 hours into 0 to 24 hours
            int thisOffsetPositive = (UtcOffsetMinutes + _dayMinutes) % _dayMinutes;
            int otherOffsetPositive = (other.UtcOffsetMinutes + _dayMinutes) % _dayMinutes;

            // Compute the basic distance between the offsets
            int distance = Math.Abs(thisOffsetPositive - otherOffsetPositive);

            // If the distance is more than half the planet, go the other way around the globe
            distance = Math.Min(distance, _dayMinutes - distance);

            return distance;
        }

        /// <summary>
        /// Creates a <see cref="LongitudinalRegion"/> identifying the region of the given time zone.
        /// </summary>
        /// <param name="timeZone">The time zone to create a region identifying.</param>
        public static LongitudinalRegion FromTimeZone(TimeZoneInfo timeZone)
        {
            return new LongitudinalRegion((int)timeZone.BaseUtcOffset.TotalMinutes);
        }

        /// <summary>
        /// Creates a <see cref="LongitudinalRegion"/> identifying the region of the time zone, if any, with the given id.
        /// </summary>
        /// <param name="timeZoneId">The <see cref="TimeZoneInfo.Id"/> of the time zone to create a region identifying.</param>
        public static LongitudinalRegion? FromTimeZoneId(string timeZoneId)
        {
            if (TimeZoneInfo.TryFindSystemTimeZoneById(timeZoneId, out var timeZone))
            {
                return LongitudinalRegion.FromTimeZone(timeZone);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Creates a <see cref="LongitudinalRegion"/> identifying the region of the local system's time zone.
        /// </summary>
        public static LongitudinalRegion FromLocalSystemTimeZone()
        {
            return LongitudinalRegion.FromTimeZone(TimeZoneInfo.Local);
        }

        public static int OffsetFromLocalSystemTimeZone()
        {
            return FromTimeZone(TimeZoneInfo.Local).UtcOffsetMinutes;
        }

        /// <summary>
        /// Computes the modulo, correctly wrapping negative inputs into positive outputs.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        private static int WrappedMod(int number, int mod)
        {
            return ((number % mod) + mod) % mod;
        }
    }
}

