using System;
using System.IO;
using System.Collections;
using Microsoft.Data.Sqlite;

namespace ZipCoderAPI.Data
{
    public class SQLiteBaseRepository
    {
        static IDictionary environmentVariables = Environment.GetEnvironmentVariables();

        public static string DbFile{
            get {return Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "StateZipCode.sqlite"; }
        }

        public static SqliteConnection SimpleDbConnection() {
            return new SqliteConnection("Data Source = " + DbFile);
        }

        
    }
}