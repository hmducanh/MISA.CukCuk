<template>
    <div>
        <div class = "content-body">
      <div class = "content-title">
          Nhân viên
      </div>
      <!-- button add employee -->
      <div class = "buttonAdd" @click="btnAddOnClick()">
        <button class = "btnAdd btn-default">Thêm một nhân viên</button>
      </div>
    </div>
    <!-- input search va button refresh -->
    <div class = "content-table">
      <div class = "bar">
            <input type="text" class = "search-table" placeholder="Tìm theo mã, tên nhân viên">
            <div class = "logo-item search-icon"></div>
            <div class = "logo-item refresh-button" @click="loadData()"></div>
            <div class = "logo-item export-button"></div>
        </div>
        <!-- Tao bang data -->
        <div class = "grid">
            <table id = "tblListEmployee" class = "table">
                <thead>
                    <tr class="line0">
                        <th>
                            <input type="checkbox">
                        </th>
                        <th>MÃ NHÂN VIÊN</th>
                        <th>TÊN NHÂN VIÊN</th>
                        <th>GIỚI TÍNH</th>
                        <th>NGÀY SINH</th>
                        <th>SỐ CMND</th>
                        <th>CHỨC DANH</th>
                        <th>TÊN ĐƠN VỊ</th>
                        <th>SỐ TÀI KHOẢN</th>
                        <th>TÊN NGÂN HÀNG</th>
                        <th>CHI NHÁNH TK NGÂN HÀNG</th>
                        <th>CHỨC NĂNG</th>
                    </tr>
                </thead>
                <tbody>
                    <!-- get data from var -->
                    <tr v-for="employee in employees" :key='employee.employeeId' @dblclick="trOnDbClick(employee.employeeId)">
                        <td><input type="checkbox"></td>
                        <td>{{employee.employeeCode}}</td>
                        <td>{{employee.fullName}}</td>
                        <td>{{employee.genderName}}</td>
                        <td>{{ formatDateDDMMYYYYnew(employee.dateOfBirth) }}</td>
                        <td>{{employee.identifyNumber}}</td>
                        <td>{{employee.positionName}}</td>
                        <td>{{employee.departmentName}}</td>
                        <td>{{employee.bankAccount}}</td>
                        <td>{{employee.bankName}}</td>
                        <td>{{employee.bankBranch}}</td>
                        <td>
                            <button class="btnDel" @click="btnDelOnClick(employee.employeeId)">Xóa</button>
                        </td>
                    </tr>
                </tbody>
            </table>
    </div>
    <!-- tranfer data to employeedetail -->
        
        <EmployeeDetail
        :isShow="isShowDialogDetail"
        @hideDialog="hideDialog()"
        :employee="selectedEmployee"
        :formMode="dialogFormMode"
        :departments="departments"
        />
    </div>
        
    </div>
</template>

<script>
import axios from "axios";
import EmployeeDetail from "./EmployeeDetail.vue";

export default {
    components : {
        EmployeeDetail,
    },
    created() {
        // load du lieu
        axios.get("http://localhost:8080/api/v1/Employee").then(res => {
            this.employees = res.data;
            console.log(res);
        }).catch(res => {
            console.log(res);
        });

        axios.get("http://localhost:8080/api/v1/Department").then(res => {
            this.departments = res.data;
            console.log(res);
        }).catch(res => {
            console.log(res);
        });
    },
    props : [],
    methods : {
        loadData() {
            // load du lieu
            axios.get("http://localhost:8080/api/v1/Employee").then(res => {
                this.employees = res.data;
                console.log(res);
            }).catch(res => {
                console.log(res);
            });
        },
        btnDelOnClick(EmployeeId) {
            axios.delete("http://localhost:8080/api/v1/Employee/" + EmployeeId).then(res => {
                this.loadData();
                console.log(res);
            }).catch(res => {
                console.log(res);
            });
        },
        // double click vao 1 hang trong bang de edit
        trOnDbClick(EmployeeId) {
            axios.get("http://localhost:8080/api/v1/Employee/GetById/" + EmployeeId).then(res => {
                this.selectedEmployee = res.data;
                const _dateOfBirth = new Date(this.selectedEmployee.dateOfBirth); // ngay sinh
                    this.selectedEmployee.dateOfBirth =
                    _dateOfBirth.getFullYear().toString() +
                    "-" +
                    (_dateOfBirth.getMonth() + 1 < 10 ? "0" : "") +
                    (_dateOfBirth.getMonth() + 1).toString() +
                    "-" +
                    (_dateOfBirth.getDate() < 10 ? "0" : "") +
                    _dateOfBirth.getDate().toString();
                const _identifyDate = new Date(this.selectedEmployee.identifyDate); // ngay cap cmnd
                    this.selectedEmployee.identifyDate =
                    _identifyDate.getFullYear().toString() +
                    "-" +
                    (_identifyDate.getMonth() + 1 < 10 ? "0" : "") +
                    (_identifyDate.getMonth() + 1).toString() +
                    "-" +
                    (_identifyDate.getDate() < 10 ? "0" : "") +
                    _identifyDate.getDate().toString();               
                this.Gender = parseInt(res.data.gender);
                console.log(this.Gender, "getbyid");      
                this.dialogFormMode = "edit";
                this.isShowDialogDetail = true;
            }).catch(res => {
                console.log(res);
            });
        },
        // click vao button add employee
        btnAddOnClick() {
            axios.get("http://localhost:8080/api/v1/Employee/maxEmployeeCode").then(res => {
                this.selectedEmployee = {};
                this.dialogFormMode = "add";
                this.isShowDialogDetail = true;
                this.selectedEmployee.employeeCode = res.data;
                this.Gender = null;
            }).catch(res => {
                console.log(res);
            });
        },
        // hien thi - an dialog
        hideDialog() {
            //an dialog
            this.isShowDialogDetail = false;
            // load lai data
            this.loadData();
            
        },
        formatDateDDMMYYYYnew(date) {
            //chuyen doi ngay thang de hien thi duoc
            var newDate = new Date(date);
            var dateString = newDate.getDate();
            var monthString = newDate.getMonth() + 1;
            var year = newDate.getFullYear();
            var s = "";
            var t = "";
            if (dateString < 10) {
                s = "0" + dateString.toString();
            } else {
                s = dateString.toString();
            }
            if (monthString < 10) {
                t = "0" + monthString.toString();
            } else {
                t = monthString.toString();
            }
            return `${s}/${t}/${year}`;
        }
    },
    data() {
        return {
            employees : [],
            departments : [],
            isShowDialogDetail: false,
            selectedEmployee: {},
            dialogFormMode: "add",
        };
    },
    watch : {},
}
</script>

<style scoped>
/* css input */

.bar { 
    display: flex;   
}

.line0 > th {
    font-size : 14px;
}

.search-table {
    height: 30px;
    width: 220px;
    border: 1px solid #bbbbbb;
    padding-left: 10px;
    padding-right: 30px;
    padding-top: 8px;
    padding-bottom: 8px;
    margin-top: 16px;
    margin-bottom: 16px;
    border-radius: 4px;
    outline: none;
    box-sizing: border-box;
    left: 200px;
    margin-left: auto;
    margin-right: 20px;
}

.search-table:hover {
    border-color: #019160;
}

.refresh-button {
    background-position: -1096px -88px;
    position: relative;
    top: 20px;
    right: 10px;
}

.refresh-button:active {
    background-color: rgb(201, 201, 201);
}

.export-button {
    background-position: -704px -200px;
    position: relative;
    top: 20px;
    right: 10px;

}

.search-icon {
    background-position: -992px -360px;
    position: absolute;
    margin-top: 22px;
    right: 100px;
}

.btnDel {
    display: inline-block;
    outline: none;
    border: none;
    padding-left: 24px;
    padding-right: 24px;
    margin-right: -5px;
    height: 40px;
    width: 90px;
    line-height: 30px;
    background-color: rgb(216, 48, 48);
    border-radius: 4px;
    font-size: 13px;
    color: #ffffff;
    font-style: normal;
    cursor: pointer;
    text-align: center;
}

.btnDel:hover {
    background-color: #e73f3fb7;
}

.btnDel:active {
    background-color: #e20404;
}


</style>