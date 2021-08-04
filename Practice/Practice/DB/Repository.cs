using Dapper;
using Microsoft.AspNetCore.Connections;
using Practice.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Practice.DB
{
    public class Repository
    {
        private string connectionString { get; }

        public Repository()
        {
            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
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
                return dbConnection.Query<TestModel>("SELECT * FROM DapperTable");
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
