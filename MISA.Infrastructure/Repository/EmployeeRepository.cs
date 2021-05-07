using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.Infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public bool CheckCustomerCodeExist(string customerCode)
        {
            throw new NotImplementedException();
        }

        public string GetMaximumEmployeeCode()
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "GetMaxEmployeeCode";
                string MaxEmployeeCode = dbConnection.QueryFirstOrDefault<string>(sqlCommand, commandType: CommandType.StoredProcedure);
                return MaxEmployeeCode;
            }
        }
    }
}
