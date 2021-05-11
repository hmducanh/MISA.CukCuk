<template>
  <div
    id="EmployeeDetail"
    class="dialog"
    :class="{ 'dialog-hide': !isShow }"
  >
  <!-- tao lop thu 2 ngan cach -->
    <div class="model"></div>
    <!--  Ve cua so  -->
    <div class="dialog-content">
      <div class="dialog-header">
        <div class="dialog-title">THÔNG TIN NHÂN VIÊN</div>
        <div class="more-info-item">
          <div class="logo-item nav-item-19">
            
          </div>
        </div>
        <div class="dialog-close-button" @click="btnCloseOnClick()">
          &#x2715;
        </div>
      <div class="dialog-body">
        <div class="m-row row-1">
          <div>
            <label>Mã(<span style="color: red">*</span>)</label><br>
            <input type="text" class="txtEmployeeCode" v-model="employee.employeeCode" :class="{ 'error-input': !isCheckEmployeeCode}">
          </div>
          <div>
            <label>Tên(<span style="color: red">*</span>)</label><br>
            <input type="text" class="txtFullName" v-model="employee.fullName" :class="{ 'error-input': !isCheckFullName}">
          </div>
          <div>
            <label>Ngày sinh</label><br>
            <input type="date" class="dateDateOfBirth" v-model="employee.dateOfBirth">
          </div>
          <div class="GenderContainer">
            <label>Giới tính</label><br>
            <a-radio-group v-model="employee.gender">
            <a-radio :value="0">
              Nam
            </a-radio>
            <a-radio :value="1">
              Nữ
            </a-radio>
            <a-radio :value="2">
              Khác
            </a-radio>
          </a-radio-group>
          </div>
        </div>
        <div class="m-row row-2">
          <div>
            <label>Đơn vị(<span style="color: red">*</span>)</label><br>
            <select name="" id="comboboxDepartment" v-model="employee.departmentName" :class="{ 'error-input': !isCheckDepartment}">
              <option v-for="department in departments" :key='department.departmentId'>{{department.departmentName}}</option>
              
            </select>
          </div>
          <div>
            <label>Số CMND</label><br>
            <input type="text" class="txtIdentifyNumber" v-model="employee.identifyNumber">
          </div>
          <div>
            <label>Ngày cấp</label><br>
            <input type="date" class="dateIdentifyDate" v-model="employee.identifyDate">
          </div>
        </div>
        <div class="m-row row-3">
          <div>
            <label>Chức danh</label><br>
            <input type="text" class="txtPositionName" v-model="employee.positionName">
          </div>
          <div>
            <label>Nơi cấp</label><br>
            <input type="text" class="txtIdentifyPlace" v-model="employee.identifyPlace">
          </div>
        </div>
        <div class="m-row row-4">
          <div>
            <label>Địa chỉ</label><br>
            <input type="text" class="txtAddress" v-model="employee.address">
          </div>
        </div>
        <div class="m-row row-5">
          <div>
            <label>ĐT di động</label><br>
            <input type="text" class="txtPhoneNumber" v-model="employee.phoneNumber">
          </div>
          <div>
            <label>ĐT cố định</label><br>
            <input type="text" class="txtConstantPhoneNumber" v-model="employee.constantPhoneNumber">
          </div>
          <div>
            <label>Email</label><br>
            <input type="text" class="txtEmail" v-model="employee.email">
          </div>
        </div>
        <div class="m-row row-6">
          <div>
            <label>Tài khoản ngân hàng</label><br>
            <input type="text" class="txtBankAccount" v-model="employee.bankAccount">
          </div>
          <div>
            <label>Tên ngân hàng</label><br>
            <input type="text" class="txtBankName" v-model="employee.bankName">
          </div>
          <div>
            <label>Chi nhánh</label><br>
            <input type="text" class="txtBankBranch" v-model="employee.bankBranch">
          </div>
        </div>
      </div>
      </div>
      <!-- footer -->
      <div class="dialog-footer">
        <button id="btnCancel" @click="btnCloseOnClick()">Hủy</button>
        <button id="btnSave" @click="btnSaveOnClick()">Lưu</button>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
export default {
  props : {
    isShow: {
        type: Boolean,
        default: false,
    },
    employee: {
        type: Object,
        default: null,
    },
    formMode: {
      type: String,
      default: "add",
    },
    departments: {
      type: Array,
      default: null,
    },
    Gender: {
      type: Number,
      default: null,
    }
  },
  methods : {
    // dong dialog
      btnCloseOnClick() {
        this.$emit("hideDialog");
          this.isCheckEmpolyeeCode = true;
          this.isCheckFullName = true;
          this.isCheckEmployeeCode = true;
          this.isCheckDepartment = true;
          this.isCheckFullName = true;
          
      },
      check_validate()
      {
        // kiem tra du lieu
        this.check = true;
        // 1. Khong duoc de trong cac o bat buoc nhap
        // 1.1 Ma nhan vien trong
        if(!this.employee.employeeCode)
        {
          this.check = false;
          this.isCheckEmployeeCode = false;
        }
        else
        {
          this.isCheckEmployeeCode = true;
        }
        // 1.2 Ten de trong
        if(!this.employee.fullName)
        {
          this.check = false;
          this.isCheckFullName = false;
        }
        else
        {
          this.isCheckFullName = true;
        }
        // 1.3 Don vi de trong
        console.log(this.comboboxDepartment);
        if(!this.employee.departmentName)
        {
          this.check = false;
          this.isCheckDepartment = false;
        }
        else
        {
          this.isCheckDepartment = true;
        }
        return this.check;
      },
      // an vao save button 
      // created by : hmducanh (10/05/2021)
      btnSaveOnClick() {
        if(this.check_validate() == false)
        {
          return ;
        }
        // check formmode
        console.log(this.employee);
        if(this.formMode == "add")
        {
          axios.post("http://localhost:8080/api/v1/Employee", this.employee).then(res => {
          console.log(res);
          this.$emit("hideDialog");
          }).catch(res => {
            console.log(res);
          });
        }
        if(this.formMode == "edit")
        {
          axios.put("http://localhost:8080/api/v1/Employee", this.employee).then(res => {
          console.log(res);
          this.$emit("hideDialog");
          }).catch(res => {
            console.log(res);
          });
        }
      }
    },
  data() {
    return {
      isCheckEmployeeCode : true,
      isCheckDepartment : true,
      isCheckFullName : true,
      check : true,
      customGender : this.Gender,
    };
  }
}
</script>


<style scoped>

input[type="text"],
input[type="date"] {
  height: 32px;
  border: 1px solid #bbbbbb;
  padding-left: 10px;
  padding-right: 16px;
  padding-bottom: 3px;
  border-radius: 4px;
  outline: none;
  box-sizing: border-box;
  margin-top: 4px;
}

.error-input {
  border-color: red !important;
}

.nav-item-19 {
  position: absolute;
  right: 34px;
  top: 10px;
  background-position: -88px -145px;
}

</style>