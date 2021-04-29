using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;

namespace MISA.Infrastructure.Repository
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity> where MISAEntity : class
    {
        IConfiguration configuration;
        //chuỗi kết nối DB 
        string connectionString = "" +
                "Host = 47.241.69.179;" +
                "Port=3306;" +
                "User Id = dev;" +
                "Password = 12345678;" +
                "Database =  MF0_NVManh_CukCuk02;" +
                "ConvertZeroDateTime=True";
        // Lấy tên đối tượng MISAEntity
        string tableName = typeof(MISAEntity).Name;
        protected IDbConnection dbConnection;
        public IEnumerable<MISAEntity> GetAll()
        {
            //Kết nối DB 
            using (dbConnection = new MySqlConnection(connectionString))
            {
                //Thực thi với DB 
                var sqlCommand = $"Proc_Get{tableName}s";
                var customers = dbConnection.Query<MISAEntity>(sqlCommand, commandType: CommandType.StoredProcedure);
                return customers;
            }
        }
        public MISAEntity GetById(Guid entityId)
        {
            // Kết nối DB 
            using (dbConnection = new MySqlConnection(connectionString))
            {
                //3. Thực thi với DB (Thêm , Sửa xóa)
                var sqlCommand = $"Proc_Get{tableName}ById";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{tableName}Id", entityId);
                var entity = dbConnection.QueryFirstOrDefault<MISAEntity>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return entity;
            }
        }
        public int Insert(MISAEntity entity)
        {
            // Kêt nối DB 
            using (dbConnection = new MySqlConnection(connectionString))
            {
                // số dòng bị thay đổi 
                var rowsAffect = dbConnection.Execute($"Proc_Insert{tableName}", param: entity, commandType: CommandType.StoredProcedure);
                return rowsAffect;
            }
        }
        public int Update(MISAEntity entity)
        {
            // Kết nối DB
            using (dbConnection = new MySqlConnection(connectionString))
            {
                // số dòng bị thay đổi  
                var sqlCommand = $"Proc_Update{tableName}";
                var rowAffects = dbConnection.Execute(sqlCommand, param: entity, commandType: System.Data.CommandType.StoredProcedure);
                return rowAffects;
            }
        }
        public int Delete(Guid entityId)
        {
            // Kết nối DB 
            using (dbConnection = new MySqlConnection(connectionString))
            {
                // 3. Thực thi hành động
                var sqlCommand = $"Proc_Delete{tableName}";
                var rowsAffects = dbConnection.Execute(sqlCommand, param: new { CustomerId = entityId }, commandType: CommandType.StoredProcedure);
                return rowsAffects;
            }
        }


    }
}