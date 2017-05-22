using System.Collections.Generic;
using ZipCoderAPI.Models;

namespace MB.Algodat
{
    public class IntervalComparer: IComparer<StateData>
    {
        /// <summary>
        /// Compares two range items by comparing their ranges.
        /// </summary>
        public int Compare(StateData x, StateData y)
        {
            return x.Range.CompareTo(y.Range);
        }
    }
}