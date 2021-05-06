using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MySqlConnector;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.CukCuk.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserExcel : ControllerBase
    {
        [HttpPost("import")]
        public async Task<DemoResponse<List<UserInfo>>> Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                return DemoResponse<List<UserInfo>>.GetFailResult(-1, "formfile is empty");
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return DemoResponse<List<UserInfo>>.GetFailResult(-1, "Not Support file extension");
            }

            var list = new List<UserInfo>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 3; row <= rowCount; row++)
                    {
                        var dateOfBirth = (worksheet.Cells[row, 6].Value == null) ? "" : ((worksheet.Cells[row, 6].Value.GetType() == typeof(double)) ? worksheet.Cells[row, 6].Value.ToString().Trim() : (string)worksheet.Cells[row, 6].Value);
                        var fmDate = FormatDate(dateOfBirth);
                        UserInfo _userInfo = new UserInfo
                        {
                            CustomerCode = (string)worksheet.Cells[row, 1].Value,
                            FullName = (string)worksheet.Cells[row, 2].Value,
                            MemberCardCode = (string)worksheet.Cells[row, 3].Value,
                            CustomerGroupName = (string)worksheet.Cells[row, 4].Value,
                            PhoneNumber = (worksheet.Cells[row, 5].Value == null) ? "" : worksheet.Cells[row, 5].Value.ToString().Trim(),
                            DateOfBirth = (worksheet.Cells[row, 6].Value == null) ? "" : worksheet.Cells[row, 6].Value.ToString().Trim(),
                            CompanyName = (string)worksheet.Cells[row, 7].Value,
                            TaxCode = (worksheet.Cells[row, 8].Value == null) ? "" : worksheet.Cells[row, 8].Value.ToString().Trim(),
                            Email = (string)worksheet.Cells[row, 9].Value,
                            Address = (string)worksheet.Cells[row, 10].Value,
                            Note = (string)worksheet.Cells[row, 11].Value,
                        };
                        // ktra du lieu

                        // ktra trung ma so dien thoai
                        if(checkPhoneNumberExistInFileExcel(_userInfo, list) == true)
                        {
                            throw new Exception($"Trung ma so dien thoai {_userInfo.PhoneNumber} trong File Excel");
                        }
                        // ktra ma nhan vien bo trong
                        if(string.IsNullOrEmpty(_userInfo.CustomerCode) == true)
                        {
                            throw new Exception("Ma nhan vien khong duoc de trong");
                        }
                        // ktra trung ma nhan vien
                        if(checkCustomerCodeExistInFileExcel(_userInfo, list) == true)
                        {
                            throw new Exception($"Trung ma khach hang {_userInfo.CustomerCode} trong File Excel");
                        }
                        // fix ngay thang cho hop le
                        _userInfo.DateOfBirth = ValidateDOB(_userInfo.DateOfBirth);
                        // them nhan vien hop le vao list
                        list.Add(_userInfo);
                    }
                }
            }

            return DemoResponse<List<UserInfo>>.GetResult(0, "OK", list);
        }
        // string connect
        private string connectionString = "" +
                "Host = 47.241.69.179;" +
                "Port = 3306;" +
                "User Id = dev;" +
                "Password = 12345678;" +
                "Database =  MF823_Import_HDANH;" +
                "ConvertZeroDateTime=True";
        private IDbConnection dbConnection;

        private bool checkPhoneNumberExistInDB(UserInfo userInfo)
        {
            // kiem tra so dien thoai co trong database hay khong
            using (dbConnection = new MySqlConnection(connectionString))
            {
                //Thực thi với DB 
                var sqlCommand = $"Proc_CheckPhoneNumberExist";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("PhoneNumber", userInfo.PhoneNumber);
                var checkPhoneNumber = dbConnection.ExecuteScalar<bool>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return checkPhoneNumber;
            }
        }

        private bool checkPhoneNumberExistInFileExcel(UserInfo userInfo, List<UserInfo> LU)
        {
            // kiem tra so dien thoai co o trong file excel vua nhap vao hay khong
            for(int i = 0; i < LU.Count; i++)
            {
                if(LU[i].CustomerCode != userInfo.CustomerCode && LU[i].PhoneNumber == userInfo.PhoneNumber)
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkCustomerCodeExistInDB(UserInfo userInfo)
        {
            // kiem tra ma khach hang co trong database hay khong
            using (dbConnection = new MySqlConnection(connectionString))
            {
                //Thực thi với DB 
                var sqlCommand = $"Proc_CheckCustomerCodeExist";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("CustomerCode", userInfo.PhoneNumber);
                var checkCustomerCode = dbConnection.ExecuteScalar<bool>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return checkCustomerCode;
            }
        }

        private bool checkCustomerCodeExistInFileExcel(UserInfo userInfo, List<UserInfo> LU)
        {
            // kiem tra ma khach hang co o trong file excel vua nhap vao hay khong
            int cnt = 0;
            for(int i = 0; i < LU.Count; i++)
            {
                if (LU[i].CustomerCode == userInfo.CustomerCode)
                    cnt++;
            }
            return (cnt > 1);
        }

        private string ValidateDOB(string DOB)
        {
            // validate ngay khang
            if(DOB.Length == 10 && DOB[2] == '/' && DOB[5] == '/' )
            {
                // hop le
            }
            else
            {
                // chuoi chi la nam
                if(DOB.Length == 4)
                {
                    string Day_month = "01/01/";
                    DOB = Day_month + DOB;
                }
                // chuoi la thang nam
                else if(DOB.Length == 7)
                {
                    string Day = "01/";
                    DOB = Day + DOB;
                }
            }
            return DOB;
        }

        private DateTime FormatDate(string date)
        {
            DateTime res = new DateTime();
            Regex rg1 = new Regex(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$");
            Regex rg2 = new Regex(@"^(0[1-9]{1}|1[0-2]{1})/\d{4}$");
            Regex rg3 = new Regex(@"\d{4}$");
            if (rg1.IsMatch(date))
            {
                DateTime res1 = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                return res1;
            }
            else if (rg2.IsMatch(date))
            {
                string newDate = "01/" + date;
                DateTime res1 = DateTime.ParseExact(newDate, "dd/MM/yyyy", null);
                return res1;
            }
            else if (rg3.IsMatch(date))
            {
                string newDate = "01/01/" + date;
                DateTime res1 = DateTime.ParseExact(newDate, "dd/MM/yyyy", null);
                return res1;
            }
            return res;
        }

        private bool checkCustomerGroupNameExistInDB(UserInfo userInfo)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = $"Proc_CheckCustomerGroupName";
                var checkCustomerGroupName = dbConnection.ExecuteScalar<bool>(sqlCommand, param: userInfo.CustomerGroupName, commandType: CommandType.StoredProcedure);
                return checkCustomerGroupName;
            }
        }

        private bool Validate(UserInfo userInfo)
        {
            if (checkPhoneNumberExistInDB(userInfo) == false && checkCustomerCodeExistInDB(userInfo) == false && checkCustomerGroupNameExistInDB(userInfo) == true)
                return true;
            else
                return false;
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserInfo userInfo)
        {
            if(Validate(userInfo))
            {
                using (dbConnection = new MySqlConnection(connectionString))
                {
                    //Thực thi với DB 
                    var sqlCommand = $"Proc_InsertCustomer";
                    var rowAffect = dbConnection.Execute(sqlCommand, param: userInfo, commandType: CommandType.StoredProcedure);
                    return StatusCode(201, rowAffect);
                }
            }
            else
            {
                return NoContent();
            }
        }
    }
}