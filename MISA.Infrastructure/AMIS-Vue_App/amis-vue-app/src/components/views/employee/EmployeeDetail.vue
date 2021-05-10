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
        <div class="dialog-close-button" @click="btnCloseOnClick()">
          &#x2715;
        </div>
      <div class="dialog-body">
        <div class="m-row row-1">
          <div>
            <label>Mã(<span style="color: red">*</span>)</label><br>
            <input type="text" class="txtEmployeeCode" v-model="txtEmployeeCode" :class="{ 'error-input': !isCheckEmpolyeeCode}">
          </div>
          <div>
            <label>Tên(<span style="color: red">*</span>)</label><br>
            <input type="text" class="txtFullName" v-model="txtFullName" :class="{ 'error-input': !isCheckFullName}">
          </div>
          <div>
            <label>Ngày sinh</label><br>
            <input type="date" class="dateDateOfBirth">
          </div>
          <div>
            <label>Giới tính</label><br>
            <input type="checkbox" id="" value="0">
            <label>Nam</label>
            <input type="checkbox" id="" value="1">
            <label>Nữ</label>
            <input type="checkbox" id="" value="2">
            <label>Khác</label>
          </div>
        </div>
        <div class="m-row row-2">
          <div>
            <label>Đơn vị(<span style="color: red">*</span>)</label><br>
            <select name="" id="comboboxDepartment" v-model="comboboxDepartment" :class="{ 'error-input': !isCheckDepartment}">
              <option value="0">Phòng Nhân Sự</option>
            </select>
          </div>
          <div>
            <label>Số CMND</label><br>
            <input type="text" class="txtIdentifyNumber">
          </div>
          <div>
            <label>Ngày cấp</label><br>
            <input type="date" class="dateIdentifyDate">
          </div>
        </div>
        <div class="m-row row-3">
          <div>
            <label>Chức danh</label><br>
            <input type="text" class="txtPositionName">
          </div>
          <div>
            <label>Nơi cấp</label><br>
            <input type="text" class="txtIdentifyPlace">
          </div>
        </div>
        <div class="m-row row-4">
          <div>
            <label>Địa chỉ</label><br>
            <input type="text" class="txtAddress">
          </div>
        </div>
        <div class="m-row row-5">
          <div>
            <label>ĐT di động</label><br>
            <input type="text" class="txtPhoneNumber">
          </div>
          <div>
            <label>ĐT cố định</label><br>
            <input type="text" class="txtConstantPhoneNumber">
          </div>
          <div>
            <label>Email</label><br>
            <input type="text" class="txtEmail">
          </div>
        </div>
        <div class="m-row row-6">
          <div>
            <label>Tài khoản ngân hàng</label><br>
            <input type="text" class="txtBankAccount">
          </div>
          <div>
            <label>Tên ngân hàng</label><br>
            <input type="text" class="txtBankName">
          </div>
          <div>
            <label>Chi nhánh</label><br>
            <input type="text" class="txtBankBranch">
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
export default {
  props : {

    isShow: {
        type: Boolean,
        default: false,
    },
  },
  methods : {
    // dong dialog
      btnCloseOnClick() {
        this.$emit("hideDialog");
          this.isCheckEmpolyeeCode = true;
          this.isCheckFullName = true;
          
      },
    // an vao save button 
    // created by : hmducanh (10/05/2021)
      btnSaveOnClick() {
        // kiem tra du lieu
        this.check = true;
        // 1. Khong duoc de trong cac o bat buoc nhap
        // 1.1 Ma nhan vien trong
        if(this.txtEmployeeCode == "")
        {
          this.check = false;
          this.isCheckEmpolyeeCode = false;
        }
        else
        {
          this.isCheckEmpolyeeCode = true;
        }
        // 1.2 Ten de trong
        if(this.txtFullName == "")
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
        if(this.comboboxDepartment == "")
        {
          this.check = false;
          this.isCheckDepartment = false;
        }
        else
        {
          this.isCheckDepartment = true;
        }
        // neu du lieu hop le, thuc hien thao tac post
      }
    },
  data() {
    return {
      txtEmployeeCode : "",
      txtFullName : "",
      comboboxDepartment : "",
      isCheckEmpolyeeCode : true,
      isCheckDepartment : true,
      isCheckFullName : true,
      check : true,
    };
  }
}
</script>


<style scoped>

input[type="text"],
input[type="date"] {
  height: 32px;
  border: 1px solid #bbbbbb;
  padding-left: 16px;
  padding-right: 16px;
  border-radius: 4px;
  outline: none;
  box-sizing: border-box;
  margin-top: 4px;
}

.error-input {
  border-color: red !important;

}

input[type="text"]:hover {
  border-color: #019160;
}

v-enter-from {
  transform: translateY(-200px);
}

v-enter-to {
  transform: translateY(0px);
}

v-enter-active {
  transition: transform 0.5s;
}
</style>