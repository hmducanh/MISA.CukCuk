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
            <input type="text" class = "search-table">
            <div class = "refresh-button"></div>
        </div>
        <!-- Tao bang data -->
        <div class = "grid">
            <table id = "tblListEmployee" class = "table">
                <thead>
                    <tr>
                        <th>
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
                    <tr @dblclick="trOnDbClick()">
                        <td></td>
                        <td>NV0000</td>
                        <td>Nguyễn Phú Trọng</td>
                        <td>Nam</td>
                        <td>01/01/1200</td>
                        <td>001200003322</td>
                        <td>Chủ tịch nước</td>
                        <td>Chính phủ</td>
                        <td>250121232</td>
                        <td>BIDV</td>
                        <td>Hà Nội</td>
                        <td></td>
                    </tr>
                    <!-- get data from var -->
                    <tr v-for="employee in employees" :key='employee.employeeId' @dblclick="trOnDbClick()">
                        <td></td>
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
                        <td></td>
                    </tr>
                </tbody>
            </table>
    </div>
    <!-- tranfer data to employeedetail -->
        <EmployeeDetail
        :isShow="isShowDialogDetail"
        @hideDialog="hideDialog()"
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
    },
    props : [],
    methods : {
        // double click vao 1 hang trong bang
        trOnDbClick() {
            this.isShowDialogDetail = true;
        },
        // click vao button add employee
        btnAddOnClick() {
            this.isShowDialogDetail = true;
        },
        // hien thi - an dialog
        hideDialog() {
            //an dialog
            console.log(1);
            this.isShowDialogDetail = false;
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
            isShowDialogDetail: false,
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

.search-table {
    height: 30px;
    width: 200px;
    border: 1px solid #bbbbbb;
    padding-left: 16px;
    padding-right: 16px;
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
</style>