﻿namespace Swiss_Tournament.DB
{
    using System;
    using System.Data.SQLite;
    using System.Runtime.InteropServices;

    public class SqliteWrapper
    {
        private string conn;

        public SqliteWrapper(string ConnectionString)
        {
            this.conn = ConnectionString;
        }

        public Tuple<DateTime, string> Created()
        {
            Tuple<DateTime, string> result = null;
            this.EvalReadSql("select * from created", delegate (SQLiteDataReader reader) {
                reader.Read();
                result = new Tuple<DateTime, string>(new DateTime((long) reader["time"]), (string) reader["msg"]);
            });
            return result;
        }

        public static void CreateNew(string filename)
        {
            SQLiteConnection.CreateFile($"{filename}");
        }

        public int EvalScalar(string sql)
        {
            using (SQLiteConnection connection = new SQLiteConnection(this.conn))
            {
                connection.Open();
                var x = new SQLiteCommand(sql, connection).ExecuteScalar();
                if (x == DBNull.Value)
                    return 0;
                return Convert.ToInt32(x);
            }
        }

        public int EvalCount(string table, string attr = "ix")
        {
            return EvalScalar($"select count({attr}) from {table}");
        }

        public void EvalNqSql(string sql)
        {
            using (SQLiteConnection connection = new SQLiteConnection(this.conn))
            {
                connection.Open();
                new SQLiteCommand(sql, connection).ExecuteNonQuery();
            }
        }

        public void EvalReadSql(string sql, Action<SQLiteDataReader> cp)
        {
            using (SQLiteConnection connection = new SQLiteConnection(this.conn))
            {
                connection.Open();
                using (SQLiteDataReader reader = new SQLiteCommand(sql, connection).ExecuteReader())
                {
                    cp(reader);
                }
            }
        }

        public static SqliteWrapper Open(string filename) => 
            new SqliteWrapper($"Data Source={filename};Version=3;");
    }
}

