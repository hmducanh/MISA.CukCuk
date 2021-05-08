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
        public bool CheckEmployeeCodeExist(string employeeCode)
        {
            // kiem tra trung ma nhan vien
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = $"Proc_CheckEmployeeCodeExist";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("EmployeeCode", employeeCode);
                var EmployeeCodeExist = dbConnection.ExecuteScalar<bool>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return EmployeeCodeExist;
            }
        }

        public string GetMaximumEmployeeCode()
        {
            // lay ma nhan vien lon nhan
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "Proc_GetMaxEmployeeCode";
                string MaxEmployeeCode = dbConnection.QueryFirstOrDefault<string>(sqlCommand, commandType: CommandType.StoredProcedure);
                return MaxEmployeeCode;
            }
        }
    }
}
