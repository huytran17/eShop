<template>
  <DraggableDiv id="alertOnClose" v-show="getIsOnClose">
    <template slot="header">
      <div class="header">
        <div class="wrapper">
          <div class="title">Xóa dữ liệu</div>
          <div class="icon" @click="closeAlert">
            <span></span>
          </div>
        </div>
      </div>
    </template>
    <template slot="main">
      <div class="body">
        <div class="icon">
          <span></span>
        </div>
        <div class="text">
          <p>Dữ liệu đã thay đổi, bạn có muốn lưu không?</p>
        </div>
      </div>
    </template>
    <template slot="footer">
      <div class="footer">
        <div class="agree" @click="saveData">
          <span class="txt">Lưu</span>
        </div>
        <div class="agree" @click="closeForm">
          <span class="txt">Không lưu</span>
        </div>
        <div class="cancel" @click="closeAlert">
          <span class="txt">Hủy bỏ</span>
        </div>
      </div>
    </template>
  </DraggableDiv>
</template>

<script>
export default {
  data() {
    return {
      isOnClose: false, //Trạng thái ẩn hiện của thông báo
    };
  },
  computed: {
    /**
     * Lấy trạng thái ẩn hiện của thông báo
     * Created by: TVHUY (16-06-2021)
     */
    getIsOnClose() {
      return this.isOnClose;
    },
  },
};
</script>