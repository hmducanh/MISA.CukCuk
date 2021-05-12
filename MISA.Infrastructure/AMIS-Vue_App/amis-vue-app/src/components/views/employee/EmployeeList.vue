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
            <input type="text" class = "search-table" placeholder="Tìm theo mã, tên nhân viên" @input="search_input($event)">
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
                            <input type="checkbox" id="top-checkbox" @click="topCheckboxOnClick()">
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
                <tbody :class="{ 'hide-body': isLoading}">
                    <!-- get data from api -->
                    <tr v-for="employee in employees" :key='employee.employeeId'>
                        <td><input type="checkbox" v-bind:id="employee.employeeCode" @click="trCheckBoxOnClick(employee.employeeCode)"> </td>
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
                            <!-- <button class="btnDel" @click="btnDelOnClick(employee.employeeId)">Xóa</button> -->
                            <div style="display: flex; margin-left: 24px;">
                                <div class="edit-text" @click="trOnClick(employee.employeeId)">Sửa</div>
                                <a-dropdown :trigger="['click']">
                                    <a>
                                        <a-icon style="margin-left:5px;font-weight:600; width: 20px;" type="down" />
                                    </a>
                                    <a-menu slot="overlay">
                                        <a-menu-item key="0">
                                            <a>Nhân bản</a>
                                        </a-menu-item>
                                    <a-menu-item @click="btnDelOnClick(employee.employeeId, employee.employeeCode)" key="1">
                                        <a>Xóa</a>
                                    </a-menu-item>
                                    <a-menu-item key="2">
                                        Ngừng sử dụng
                                    </a-menu-item>
                                    </a-menu>
                                </a-dropdown>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
    </div>
    <div class="footer">
            <div>Tổng số <span style="font-weight: bold;"> {{totalEmployee}} </span> bản ghi</div>
        </div>
    <!-- icon loader -->
        <div class="icon-loader" :class="{ 'icon-loader-show': !isLoading}"></div>
    <!-- tranfer data to employeedetail -->
        
        <EmployeeDetail
        :isShow="isShowDialogDetail"
        @hideDialog="hideDialog()"
        :employee="selectedEmployee"
        :formMode="dialogFormMode"
        :departments="departments"
        />
    <!-- warning when click delete button -->
        <WarningDialog
        :isShowWarning="isShowWarningDialog"
        :employeeCode="employeeCode"
        @hideWarningDialog="hideWarningDialog()"
        @status_delete="child_click"
        />
    </div>
        
    </div>
</template>

<script>
import axios from "axios";
import _lodash from "lodash";
import EmployeeDetail from "./EmployeeDetail.vue";
import WarningDialog from './WarningDialog.vue';

export default {
    components : {
        EmployeeDetail,
        WarningDialog,
    },
    /* 
    load du lieu luc bat dau vao traang
    created by : hmducanh (9/5/2021)
    */
    created() {
        // lay danh sach nhan vien
        axios.get("http://localhost:8080/api/v1/Employee").then(res => {
            this.employees = res.data;
            this.validEmployee = res.data;
            this.totalEmployee = this.employees.length;
            console.log(res);
        }).catch(res => {
            console.log(res);
        });
        // lay danh sach don vi
        axios.get("http://localhost:8080/api/v1/Department").then(res => {
            this.departments = res.data;
            console.log(res);
        }).catch(res => {
            console.log(res);
        });
    },
    props : [],
    methods : {
        /* 
        chay gif xoay xoay khi loading data
        created by : hmducanh (12/5/2021)
        */
        debounceLoadData:_lodash.debounce(function(){
            this.isLoading = false;
        },600),
        /* 
        load lai data trong bang
        created by : hmducanh (9/5/2021)
        */
        loadData() {
            this.isLoading = true;
            axios.get("http://localhost:8080/api/v1/Employee").then(res => {
                this.employees = res.data;
                this.validEmployee = res.data;
                this.totalEmployee = this.employees.length;
                console.log(res);
            }).catch(res => {
                console.log(res);
            });
            this.debounceLoadData();
        },
        /* 
        click vao check box o trong table -> body -> row
        nhan no va kiem tra xem tat ca checkbox co = true khong
        neu co thi bat check cua header
        created by : hmducanh (12/5/2021)
        */
        trCheckBoxOnClick(id) {
            if(document.querySelector("#" + id).checked == false)
                document.querySelector("#top-checkbox").checked = false;
            else
            {
                for(let index in this.employees)
                {
                    var Id = "#" + `${this.employees[index].employeeCode}`;
                    if(document.querySelector(Id).checked == false)
                    {
                        document.querySelector("#top-checkbox").checked = false;
                        return ;
                    }
                }
                document.querySelector("#top-checkbox").checked = true;
            }
        },
        /* 
        nhan su kien click vao check box cua header
        sau do fill cac checkbox cua cac hang duoi tuong ung
        created by : hmducanh (12/5/2021)
        */
        topCheckboxOnClick() {
            this.topCheckboxstatus = document.querySelector("#top-checkbox").checked;
            for(let index in this.employees)
            {
                var Id = "#" + `${this.employees[index].employeeCode}`;
                document.querySelector(Id).checked = this.topCheckboxstatus;
            }
        },
        /* 
        kiem tra xem xau 'child' co phai la xau con cua xau 'father' khong 
        dung de filter
        created by : hmducanh (12/5/2021) 
        */
        check_substring(father, child)
        {
            if(child == "" || child == null)
                return true;
            for(let i = 0; i < (father.length - child.length + 1); i++)
            {
                this.check_sub = true;
                for(let j = 0; j < child.length; j++)
                {
                    if(father[i + j] != child[j])
                    {
                        this.check_sub = false;
                        break;
                    }
                }
                if(this.check_sub == true)
                    return true;
            }
            return false;
        },
        /* 
        lien tuc nhan su kien tu input text de loc data
        created by : hmducanh (12/5/2021)
        */
        search_input(event)
        {
            this.employees = [];
            for(let index in this.validEmployee)
            {
                let input = event.target.value;
                if(this.check_substring(this.validEmployee[index].employeeCode, input) || this.check_substring(this.validEmployee[index].fullName, input))
                {
                    this.employees.push(this.validEmployee[index]);
                }
            }
        },
        /* 
        nhan du lieu tu warning dialog xem co duoc xoa hay khong
        Created by : hmducanh (12/5/2021)
        */
        child_click(value)
        {
            if(value == "0")
            {
                console.log("ko dc xoa");
            }
            else
            {
                axios.delete("http://localhost:8080/api/v1/Employee/" + this.delete_employeeId).then(res => {
                this.loadData();
                console.log(res);
                }).catch(res => {
                    console.log(res);
                });
            }
        },
        /* 
        Click vao nut xoa
        CreatedBy : hmducanh (12/5/2021)
        */
        btnDelOnClick(EmployeeId, EmployeeCode) {
            this.isShowWarningDialog = true;
            this.delete_employeeId = EmployeeId;
            this.employeeCode = EmployeeCode;
            /* axios.delete("http://localhost:8080/api/v1/Employee/" + EmployeeId).then(res => {
                this.loadData();
                console.log(res);
            }).catch(res => {
                console.log(res);
            }); */
        },
        
        /* 
        double click vao 1 hang trong bang de edit
        created by : hmducanh (11/5/2021)
        */
        trOnClick(EmployeeId) {
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
        /* 
        click vao button de add employee
        created by : hmducanh (11/5/2021)
        */
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
        /* 
        // hien thi - an form dialog
        // Created by : hmducanh (10/5/2021)
        */
        hideDialog() {
            //an dialog
            this.isShowDialogDetail = false;
            // load lai data
            this.loadData();
        },
        /* 
        // hien thi - an warning dialog
        // Created by : hmducanh (12/5/2021)
        */
        hideWarningDialog() {
            this.isShowWarningDialog = false;
        },
        /* 
        chuyen doi ngay thang sang chuoi DDMMYYYY de hien thi duoc
        created by : hmducanh (11/5/2021)
        */
        formatDateDDMMYYYYnew(date) {
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
        // cac bien toan cuc
        return {
            employees : [],
            departments : [],
            isShowDialogDetail: false,
            selectedEmployee: {},
            dialogFormMode: "add",
            topCheckboxstatus: false,
            check_sub: false,
            validEmployee: [],
            isShowWarningDialog : false,
            delete_employeeId : "",
            isLoading : false,
            totalEmployee : 0,
            employeeCode : "",
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
    margin-left: 4px;
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

input[type="checkbox"] {
    display: inline-block;
    width: 20px;
    height: 20px;
    margin-top: 10px;
    margin-left: 18px;
}

input[type="text"]:focus {
    border-color: #019160;
}

.icon-loader-show {
    display: none;
}

.hide-body {
    display: none;
}

.footer {
    position: absolute;
    bottom: 30px;
    left: 50px;
}
</style>