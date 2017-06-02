using System;
using System.Collections.Generic;
using ZipCoderAPI.Interfaces;
using ZipCoderAPI.Models;
using ZipCoderAPI.Data;
using Microsoft.Data.Sqlite;
using Dapper;

namespace ZipCoderAPI.Modules
{
    public class StateIntervalMatcher: IStateMatcher
    {
        private static SqliteConnection DBConnection;
        private static IList<StateData> StateIntervals;

        public StateIntervalMatcher() {
            DBConnection = SQLiteBaseRepository.SimpleDbConnection();

            StateIntervals = DBConnection.Query<StateData>(
            "SELECT State.name as stateName, State.abbreviation, StateZipCodeIntervals.start, " +
            "StateZipCodeIntervals.end FROM State INNER JOIN StateZipCodeIntervals " +
            "ON State.id = StateZipCodeIntervals.stateId").AsList();
        }

        public string LocateState(string zipCode) {
            try {
                if (zipCode.Length != 5) {
                    return "[UNKNOWN]";
                } else {
                    int zipCodePrefix = Convert.ToInt32(zipCode.Substring(0, 3));
                    string stateName = "[UNKNOWN]";
                    foreach (var state in StateIntervals) {
                        if (zipCodePrefix >= state.start && zipCodePrefix <= state.end) {
                            stateName = state.stateName;
                            break;
                        }   
                    };
                    return stateName;
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return "[UNKNOWN]";
            }
        }
    }
}