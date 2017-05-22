using System;
using System.Collections.Generic;

namespace ZipCoderAPI.Data
{
    public class StateNamesAndAbbreviations
    {
        public static Dictionary<string, string> state2Abbr = new Dictionary<string, string>();

        public static Dictionary<string, string> abbr2State = new Dictionary<string, string>();

        public StateNamesAndAbbreviations() {
            abbr2State.Add("AL", "Alabama");
            abbr2State.Add("AK", "Alaska");
            abbr2State.Add("AZ", "Arizona");
            abbr2State.Add("AR", "Arkansas");
            abbr2State.Add("CA", "California");
            abbr2State.Add("CO", "Colorado");
            abbr2State.Add("CT", "Connecticut");
            abbr2State.Add("DE", "Delaware");
            abbr2State.Add("DC", "District of Columbia");
            abbr2State.Add("FL", "Florida");
            abbr2State.Add("GA", "Georgia");
            abbr2State.Add("HI", "Hawaii");
            abbr2State.Add("ID", "Idaho");
            abbr2State.Add("IL", "Illinois");
            abbr2State.Add("IN", "Indiana");
            abbr2State.Add("IA", "Iowa");
            abbr2State.Add("KS", "Kansas");
            abbr2State.Add("KY", "Kentucky");
            abbr2State.Add("LA", "Louisiana");
            abbr2State.Add("ME", "Maine");
            abbr2State.Add("MD", "Maryland");
            abbr2State.Add("MA", "Massachusetts");
            abbr2State.Add("MI", "Michigan");
            abbr2State.Add("MN", "Minnesota");
            abbr2State.Add("MS", "Mississippi");
            abbr2State.Add("MO", "Missouri");
            abbr2State.Add("MT", "Montana");
            abbr2State.Add("NE", "Nebraska");
            abbr2State.Add("NV", "Nevada");
            abbr2State.Add("NH", "New Hampshire");
            abbr2State.Add("NJ", "New Jersey");
            abbr2State.Add("NM", "New Mexico");
            abbr2State.Add("NY", "New York");
            abbr2State.Add("NC", "North Carolina");
            abbr2State.Add("ND", "North Dakota");
            abbr2State.Add("OH", "Ohio");
            abbr2State.Add("OK", "Oklahoma");
            abbr2State.Add("OR", "Oregon");
            abbr2State.Add("PA", "Pennsylvania");
            abbr2State.Add("RI", "Rhode Island");
            abbr2State.Add("SC", "South Carolina");
            abbr2State.Add("SD", "South Dakota");
            abbr2State.Add("TN", "Tennessee");
            abbr2State.Add("TX", "Texas");
            abbr2State.Add("UT", "Utah");
            abbr2State.Add("VT", "Vermont");
            abbr2State.Add("VA", "Virginia");
            abbr2State.Add("WA", "Washington");
            abbr2State.Add("WV", "West Virginia");
            abbr2State.Add("WI", "Wisconsin");
            abbr2State.Add("WY", "Wyoming");
            abbr2State.Add("ZZ", "[UNKNOWN]");

            List<string> keyList = new List<string>(StateNamesAndAbbreviations.abbr2State.Keys);

            foreach (var k in keyList) {
                string v = abbr2State[k];
                state2Abbr.Add(v, k);
            }
        }

        public Dictionary<string, string> s2a(){
            return state2Abbr;
        }

        public Dictionary<string, string> a2s() {
            return abbr2State;
        }
    }
}