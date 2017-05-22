using System.Collections.Generic;

namespace ZipCoderAPI.Models
{
    public class StateListItem
    {
        public string stateName;
        public string stateAbbreviation;
        public long recordCount;

        public StateListItem(string sn, string sa, long rc) {
            this.stateName = sn;
            this.stateAbbreviation = sa;
            this.recordCount = rc;
        }
    }

    public class StateList
    {
        public List<StateListItem> results;
    }
}