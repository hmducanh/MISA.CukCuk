<template>
    <!-- 
        Tao dialog canh bao nguoi dung khi an nut xoa
        Created by : hmducanh (12/5/2021)
     -->
    <div id="WarningDialog" :class="{ 'warningdialog-hide': !isShowWarning}">
        <!-- tao lop thu 2 ngan cach -->
        <div class="model"></div>
        <!--  Ve cua so  -->
        <div class="WDialog">
            <!-- logo canh bao -->
            <div class="logo-item warning-icon"></div>
            <div class="line-1">Bạn có thực sự muốn xóa Nhân viên ...</div><br>
            <div class="line-2">không?</div>
            <div class="line-3">_________________________________________________________________</div>
            <!-- button truyen trang thai co hoac khong -->
            <button class="btnNo" @click="close_dialog()">Không</button>
            <button class="btnYes" @click="close_dialog()">Có</button>
        </div>

    </div>
</template>

<script>
export default ({
    props: {
        isShowWarning: {
            type: Boolean,
            default: null,
        },
    },
    methods: {
        close_dialog()
        {
            this.$emit("hideWarningDialog");
        }
    },
})
</script>


<style scoped>
.btnNo {
    display: inline-block;
    padding-left: 24px;
    padding-right: 24px;
    height: 40px;
    line-height: 30px;
    background-color: #ffffff;
    border-radius: 4px;
    font-size: 13px;
    color: black;
    font-style: normal;
    cursor: pointer;
    text-align: center;
    position: absolute;
    top: 160px;
    left: 40px;
}

.btnYes {
    display: inline-block;
    outline: none;
    border: none;
    padding-left: 24px;
    padding-right: 24px;
    height: 40px;
    line-height: 30px;
    background-color: #00744d;
    border-radius: 4px;
    font-size: 13px;
    color: #ffffff;
    font-style: normal;
    cursor: pointer;
    text-align: center;
    position: absolute;
    top: 160px;
    right: 42px;
}

#btnYes:hover {
    background-color: #2fbe8e;
  }
  
#btnYes:active {
    background-color: #01b075;
  }
</style>