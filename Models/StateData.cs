using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MB.Algodat;

namespace ZipCoderAPI.Models
{
    public class StateData: IRangeProvider<int>
    {
        public string stateName;
        public string abbreviation;
        public int start;
        public int end;
        public Range<int> Range {
            get {
                return new Range<int>(this.start, this.end);
            }
        }
    }
}