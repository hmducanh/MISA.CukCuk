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
        <!-- nut dong dialog -->
        <div class="dialog-close-button" @click="btnCloseOnClick()">
          &#x2715;
        </div>
        <!-- cac input trong class -->
      <div class="dialog-body">
        <div class="m-row row-1">
          <div>
            <label class="title">Mã(<span style="color: red">*</span>)</label><br>
            <input type="text" class="txtEmployeeCode" v-model="employee.employeeCode" :class="{ 'error-input': !isCheckEmployeeCode}">
            <div :class="{ 'hide-msg': isCheckEmployeeCode}"><span style="font-size: 12px; color:red;"> {{msg_empty_employeeCode}} </span></div>
            <div :class="{ 'hide-msg': isExistEmployeeCode}"><span style="font-size: 12px; color:red;"> {{msg_EmployeCodeExist}} </span></div>
          </div>
          <div>
            <label label class="title">Tên(<span style="color: red">*</span>)</label><br>
            <input type="text" class="txtFullName" v-model="employee.fullName" :class="{ 'error-input': !isCheckFullName}">
            <div :class="{ 'hide-msg': isCheckFullName}"><span style="font-size: 12px; color:red;"> {{msg_empty_fullName}} </span></div>
          </div>
          <div>
            <label label class="title">Ngày sinh</label><br>
            <input type="date" class="dateDateOfBirth" v-model="employee.dateOfBirth">
          </div>
          <div class="GenderContainer">
            <label class="title">Giới tính</label><br>
            <div>
              <a-radio-group v-model="employee.gender" class="cover-gender">
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
        </div>
        <div class="m-row row-2">
          <div>
            <label label class="title">Đơn vị(<span style="color: red">*</span>)</label><br>
            <select name="" id="comboboxDepartment" v-model="employee.departmentName" :class="{ 'error-input': !isCheckDepartment}">
              <option v-for="department in departments" :key='department.departmentId'>{{department.departmentName}}</option>
              
            </select>
            <div :class="{ 'hide-msg': isCheckDepartment}"><span style="font-size: 12px; color:red;"> {{msg_empty_departmentName}} </span></div>
          </div>
          <div>
            <label label class="title">Số CMND</label><br>
            <input type="text" class="txtIdentifyNumber" v-model="employee.identifyNumber">
          </div>
          <div>
            <label label class="title">Ngày cấp</label><br>
            <input type="date" class="dateIdentifyDate" v-model="employee.identifyDate">
          </div>
        </div>
        <div class="m-row row-3">
          <div>
            <label label class="title">Chức danh</label><br>
            <input type="text" class="txtPositionName" v-model="employee.positionName">
          </div>
          <div>
            <label label class="title">Nơi cấp</label><br>
            <input type="text" class="txtIdentifyPlace" v-model="employee.identifyPlace">
          </div>
        </div>
        <div class="m-row row-4">
          <div>
            <label label class="title">Địa chỉ</label><br>
            <input type="text" class="txtAddress" v-model="employee.address">
          </div>
        </div>
        <div class="m-row row-5">
          <div>
            <label label class="title">ĐT di động</label><br>
            <input type="text" class="txtPhoneNumber" v-model="employee.phoneNumber" :class="{ 'error-input': !isCheckPhoneNumber}">
            <div :class="{ 'hide-msg': isCheckPhoneNumber}"><span style="font-size: 12px; color:red;"> {{msg_phoneNumber}} </span></div>
          </div>
          <div>
            <label label class="title">ĐT cố định</label><br>
            <input type="text" class="txtConstantPhoneNumber" v-model="employee.constantPhoneNumber" :class="{ 'error-input': !isCheckConstantPhoneNumber}">
            <div :class="{ 'hide-msg': isCheckConstantPhoneNumber}"><span style="font-size: 12px; color:red;"> {{msg_phoneNumber}} </span></div>
          </div>
          <div>
            <label label class="title">Email</label><br>
            <input type="text" class="txtEmail" v-model="employee.email" :class="{ 'error-input': !isCheckEmail}">
            <div :class="{ 'hide-msg': isCheckEmail}"><span style="font-size: 12px; color:red;"> {{msg_email}} </span></div>
          </div>
        </div>
        <div class="m-row row-6">
          <div>
            <label label class="title">Tài khoản ngân hàng </label><br>
            <input type="text" class="txtBankAccount" v-model="employee.bankAccount" :class="{ 'error-input': !isCheckBankAccount}">
            <div :class="{ 'hide-msg': isCheckBankAccount}"><span style="font-size: 12px; color:red;"> {{msg_bankAccount}} </span></div>
          </div>
          <div>
            <label label class="title">Tên ngân hàng</label><br>
            <input type="text" class="txtBankName" v-model="employee.bankName">
          </div>
          <div>
            <label label class="title">Chi nhánh</label><br>
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
      /* 
      mac dinh lai cac ham kiem tra
      created by : hmducanh (12/5/2021)
      */
      refresh()
      {
          this.isCheckEmpolyeeCode = true;
          this.isCheckFullName = true;
          this.isCheckEmployeeCode = true;
          this.isCheckDepartment = true;
          this.isCheckFullName = true;
          this.isCheckBankAccount = true;
          this.isCheckPhoneNumber = true;
          this.isCheckConstantPhoneNumber = true;
          this.isCheckEmail = true;
      },
      /* 
      dong dialog va set cac bien kiem tra ve true
      created by : hmducanh (9/5/2021)
      */
      btnCloseOnClick() {
        this.$emit("hideDialog");
        this.refresh();
      },
      /* 
      check su hop le cua email
      created by : hmducanh (12/5/2021)
      */
      check_valid_email(s)
      {
          let index1 = 0, index2 = 0, cnt = 0;
          for(let i in s)
          {
              if(s[i] == '@')
                  index1 = i;
              if(s[i] == '.')
              {
                  cnt++;
                  if(cnt > 1)
                  {
                    // khong cho 2 dau '.' lien nhau
                    if(i - index2 == 1)
                      return false;
                  }
                  index2 = i;
              }
              //ky tu hop le
              if((s[i] >= '0' && s[i] <= '9') || (s[i] >= 'a' && s[i] <= 'z') || (s[i] >= 'A' && s[i] <= 'Z') || s[i] == '@' || s[i] == '.')
              {
                  //ok
              }
              else
              {
                  return false;
              }
          }
          // @ khong dung dau cau va giua @ va '.' phai co ky tu la '.' khong dung cuoi
          if((index1 > 0) && (index2 - index1 > 1) && (index2 != s.length - 1))
              return true;
          return false;
      },
      /* 
      kiem tra co trung ma nhan vien da ton tai trong database khong
      */
      async checkExistEmployeeCode()
      {
        await axios.get("http://localhost:8080/api/v1/Employee/" + this.employee.employeeCode).then(res => {
                this.isExistEmployeeCode = res.data;   
            }).catch(res => {
                console.log(res);
            });
        return this.isExistEmployeeCode;
      },
      /* 
      kiem tra xem du lieu co hop le khong truoc khi gui len server
      created by : hmducanh (10/5/2021)
      */
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
        // 2. kiem tra so dien thoai
        for(let i in this.employee.phoneNumber)
        {
          if(isNaN(this.employee.phoneNumber[i]))
          {
            this.isCheckPhoneNumber = false;
            this.check = false;
            break;
          }
        }

        for(let i in this.employee.constantPhoneNumber)
        {
          if(isNaN(this.employee.constantPhoneNumber[i]))
          {
            this.isCheckConstantPhoneNumber = false;
            this.check = false;
            break;
          }
        }
        // 3. kiem tra tai khoan ngan hang
        for(let i in this.employee.bankAccount)
        {
          if(isNaN(this.employee.bankAccount[i]))
          {
            this.isCheckBankAccount = false;
            this.check = false;
            console.log("backccc");
            break;
          }
        }
        // 4. kiem tra email hop le
        if(this.check_valid_email(this.employee.email) == false)
        {
          this.isCheckEmail = false;
          this.check = false;
        }
        return this.check;
      },
      /* 
      an vao save button 
      created by : hmducanh (10/05/2021) 
      */
      async btnSaveOnClick() {
        // goi ham kiem tra du lieu
         if(this.check_validate() == false)
        {
          return ;
        } 
        // check formmode xem la sua hay nhap moi
        if(this.formMode == "add")
        {
          // kiem tra ma nhan vien da ton tai trong database chua
          this.isExistEmployeeCode = false;
          if(await this.checkExistEmployeeCode() == true)
          {
            this.isCheckEmployeeCode = false;
            return ;
          }
          axios.post("http://localhost:8080/api/v1/Employee", this.employee).then(res => {
          console.log(res);
          this.refresh();
          this.$emit("hideDialog");
          }).catch(res => {
            console.log(res);
          });
        }
        if(this.formMode == "edit")
        {
          axios.put("http://localhost:8080/api/v1/Employee", this.employee).then(res => {
          console.log(res);
          this.refresh();
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
      isCheckPhoneNumber : true,
      isCheckConstantPhoneNumber : true,
      isCheckBankAccount : true,
      isCheckEmail : true,
      isExistEmployeeCode : false,
      check : true,
      customGender : this.Gender,
      msg_empty_employeeCode : "Mã nhân viên không được để trống !",
      msg_empty_fullName : "Tên nhân viên không được để trống !",
      msg_empty_departmentName : "Đơn vị không được để trống !",
      msg_phoneNumber : "Số điện thoại không hợp lệ !",
      msg_email : "Email không hợp lệ !",
      msg_bankAccount : "Tài khoản ngân hàng không hợp lệ !",
      msg_EmployeCodeExist: "Mã nhân viên đã tồn tại !",
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

input:focus {
  border-color: #019160;
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

.cover-gender {
  position: absolute;
  right: 70px;
  top: 28px;
}

.cover-gender:focus {
  border-color: #019160;
}

.title {
  font-weight: bold;
}

.hide-msg {
  display: none;
}

</style>