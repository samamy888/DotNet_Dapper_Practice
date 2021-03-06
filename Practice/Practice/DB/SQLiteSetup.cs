using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Practice.Config;
using Practice.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Practice.DB
{
    public class SQLiteSetup
    {
        public readonly DBConfig dBConfig;
        private SqliteConnection Connection;
        public SQLiteSetup(IOptions<DBConfig> option)
        {
            dBConfig = option.Value;
            Connection = new SqliteConnection($"Data Source={dBConfig.DatabaseName}");
        }
        public int Add(TestModel testModel)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "INSERT INTO DapperTable (Name)"
                                + " VALUES(@Name)";

                dbConnection.Open();
                return dbConnection.Execute(sQuery, testModel);
            }
        }

        public IEnumerable<TestModel> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<TestModel>("SELECT ID,Name FROM DapperTable");
            }
        }

        public TestModel GetByID(int ID)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "SELECT * FROM DapperTable WHERE ID = @ID";
                dbConnection.Open();
                return dbConnection.Query<TestModel>(sQuery, new { Id = ID }).FirstOrDefault();
            }
        }

        public int Delete(int ID)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "DELETE FROM DapperTable  WHERE ID = @ID";
                dbConnection.Open();
                return dbConnection.Execute(sQuery, new { Id = ID });
            }
        }

        public int Update(TestModel testModel)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "UPDATE DapperTable SET Name = @Name WHERE ID = @ID";
                dbConnection.Open();
                return dbConnection.Execute(sQuery, testModel);
            }
        }
    }
}
