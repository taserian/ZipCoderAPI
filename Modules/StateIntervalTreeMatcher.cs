using System;
using System.Collections.Generic;
using ZipCoderAPI.Interfaces;
using ZipCoderAPI.Models;
using ZipCoderAPI.Data;
using Microsoft.Data.Sqlite;
using Dapper;
using MB.Algodat;

namespace ZipCoderAPI.Modules
{
    public class StateIntervalTreeMatcher: IStateMatcher
    {
        private static SqliteConnection DBConnection;
        private static IList<StateData> StateIntervals;
        private static IRangeTree<int, StateData> intervalTree;
        
        public StateIntervalTreeMatcher() {
            DBConnection = SQLiteBaseRepository.SimpleDbConnection();

            StateIntervals = DBConnection.Query<StateData>(
            "SELECT State.name as stateName, State.abbreviation, StateZipCodeIntervals.start, " +
            "StateZipCodeIntervals.end FROM State INNER JOIN StateZipCodeIntervals " +
            "ON State.id = StateZipCodeIntervals.stateId").AsList();

            intervalTree = new RangeTree<int, StateData>(new IntervalComparer());

            foreach( var state in StateIntervals ) {
                intervalTree.Add( (StateData)state );
            }
        }

        public string LocateState(string zipCode) {
            int zipCodePrefix = Convert.ToInt32(zipCode.Substring(0, 3));
            List<StateData> states = intervalTree.Query(zipCodePrefix);
            if (states.Count != 1) {
                return "[UNKNOWN]";
            } else {
                return states[0].stateName;
            }
        }
    }
}