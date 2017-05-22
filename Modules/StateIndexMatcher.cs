using System;
using System.Collections.Generic;
using ZipCoderAPI.Interfaces;
using ZipCoderAPI.Models;
using ZipCoderAPI.Data;
using Microsoft.Data.Sqlite;
using Dapper;

namespace ZipCoderAPI.Modules
{
    
    public class StateIndexMatcher: IStateMatcher
    {
        private static SqliteConnection DBConnection;
        private static IList<StateData> StateIntervals;

        private static Dictionary<int, string> stateDictionary;
        
        public StateIndexMatcher(){
            DBConnection = SQLiteBaseRepository.SimpleDbConnection();

            StateIntervals = DBConnection.Query<StateData>(
            "SELECT State.name as stateName, State.abbreviation, StateZipCodeIntervals.start, " +
            "StateZipCodeIntervals.end FROM State INNER JOIN StateZipCodeIntervals " +
            "ON State.id = StateZipCodeIntervals.stateId").AsList();

            stateDictionary = GenerateDictionary();
        }

        public string LocateState(string zipCode) {
            zipCode = zipCode.PadLeft(5, '0');
            int zipCodePrefix = Convert.ToInt32(zipCode.Substring(0, 3));
            string stateName = "";
            if (stateDictionary.TryGetValue(zipCodePrefix, out stateName)) {
                return stateDictionary[zipCodePrefix];
            } else {
                return "[UNKNOWN]";
            }
        }

        private Dictionary<int, string> GenerateDictionary() {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            foreach (var state in StateIntervals) {
                for (int s = state.start; s <= state.end; s++) {
                    dictionary[s] = state.stateName;
                }
            };
            return dictionary;
        }

    }
}