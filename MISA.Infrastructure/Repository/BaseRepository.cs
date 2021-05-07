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
        /// <summary>
        /// Những thao tác chung với db
        /// </summary>
        /// CreatedBy : hmducanh (29/04/2021)
        IConfiguration configuration;
        //chuỗi kết nối DB 
        protected string connectionString = "" +
                "Host = 47.241.69.179;" +
                "Port=3306;" +
                "User Id = dev;" +
                "Password = 12345678;" +
                "Database =  TEST.MISA.AMIS;" +
                "ConvertZeroDateTime=True";
        // Lấy tên đối tượng MISAEntity
        string tableName = typeof(MISAEntity).Name;
        protected IDbConnection dbConnection;
        // Lấy tất cả dữ liệu
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
        // Lấy dữ liệu theo id
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
        // thêm 1 dữ liệu   
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
        // cập nhật 1 dữ liệu
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
        // xóa 1 dữ liệu ( thực thể )
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