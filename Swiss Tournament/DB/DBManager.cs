namespace Swiss_Tournament.DB
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class DBManager
    {
        private SqliteWrapper db;

        public DBManager(SqliteWrapper db)
        {
            this.db = db;
        }

        public void InsertHistory(int p1, int p2, Status status, int round, string desc)
        {
            string sql = $"insert into history (ix, p1, p2, status, round, desc) values ({this.db.EvalCount("history", "ix") + 1}, {p1}, {p2}, {(int) status}, {round}, '{desc}')";
            this.db.EvalNqSql(sql);
        }

        public void InsertMember(string name, int id)
        {
            string sql = $"insert into members (ix, name, id) values ({this.db.EvalCount("members", "ix") + 1}, '{name}', {id})";
            this.db.EvalNqSql(sql);
        }

        public bool IsExistsId(int id) => 
            this.QueryMember("").Any<Member>(x => (x.Id == id));

        public void ModifyHistory(int ix, Status status)
        {
            string sql = $"update history set status={(int) status} where ix={ix}";
            this.db.EvalNqSql(sql);
        }

        public void ModifyMember(int id_src, int id_dest, string name)
        {
            string sql = $"update members set name='{name}', id={id_dest} where id={id_src}";
            this.db.EvalNqSql(sql);

            string sql2 = $"update history set p1={id_dest} where p1={id_src}";
            this.db.EvalNqSql(sql2);
            string sql3 = $"update history set p2={id_dest} where p2={id_src}";
            this.db.EvalNqSql(sql3);
        }

        public List<History> QueryHistory(string where = "")
        {
            string sql = "select * from history";
            if (where != "")
            {
                sql = sql + " where " + where;
            }
            List<History> result = new List<History>();
            this.db.EvalReadSql(sql, delegate (SQLiteDataReader reader) {
                while (reader.Read())
                {
                    History item = new History();
                    item.Index = (int) reader["ix"];
                    item.Player1 = (int) reader["p1"];
                    item.Player2 = (int) reader["p2"];
                    item.Status = (Status) reader["status"];
                    item.Round = (int) reader["round"];
                    item.Description = (string) reader["desc"];
                    result.Add(item);
                }
            });
            return result;
        }

        public List<Member> QueryMember(string where = "")
        {
            string sql = "select * from members";
            if (where != "")
            {
                sql = sql + " where " + where;
            }
            List<Member> result = new List<Member>();
            this.db.EvalReadSql(sql, delegate (SQLiteDataReader reader) {
                while (reader.Read())
                {
                    Member item = new Member();
                    item.Index = (int) reader["ix"];
                    item.Name = (string) reader["name"];
                    item.Id = (int) reader["id"];
                    result.Add(item);
                }
            });
            return result;
        }

        public int GetMaxRound()
        {
            string sql = "select max(round) from history";
            return this.db.EvalScalar(sql);
        }

        public List<History> QueryRound(int round) => 
            this.QueryHistory($"round={round}");

        public void RemoveHistory(int ix)
        {
            string sql = $"delete from history where ix={ix}";
            this.db.EvalNqSql(sql);
        }

        public void RemoveMember(int id)
        {
            string sql = $"delete from members where id={id}";
            this.db.EvalNqSql(sql);
        }
    }
}

