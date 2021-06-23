<template>
  <DraggableDiv id="alertAlreadyExists" v-show="getIsShowing">
    <template slot="header">
      <div class="header">
        <div class="wrapper">
          <div class="title">MISA eShop</div>
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
          <p>Mã cửa hàng đã tồn tại. Vui lòng kiểm tra lại.</p>
        </div>
      </div>
    </template>
    <template slot="footer">
      <div class="footer">
        <div class="agree" @click="closeAlert">
          <span class="txt">Đồng ý</span>
        </div>
      </div>
    </template>
  </DraggableDiv>
</template>

<style scoped>
@import "../../assets/scss/customize/alertstorealreadyexists/style.css";
</style>

<script>
import DraggableDiv from "../Common/MISA.CreateFormDragable";

export default {
  created() {
    /**
     * sự kiện hiển thị thông báo khi thêm cửa hàng có mã cửa hàng đã tồn tại
     * Created by: TVHUY (16-06-2021)
     */
    this.$bus.$on("showAlertAlreadyExists", () => {
      this.isShowing = true;
      this.$bus.$emit("showOverlay");
      this.$bus.$emit("coverOverlay", 99999999); //hiển thị lớp phủ
    });
    /**
     * Sự kiện đóng thông báo cảnh báo mã cửa hàng đã tồn tại khi thêm mới
     * Created by: TVHUY (16-06-2021)
     */
    this.$bus.$on("closeAlertAlreadyExists", () => {
      this.isShowing = false;
      this.$bus.$emit("coverOverlay", 99999);
    });
  },
  /**
   * Khai báo các component
   * Crated by: TVHUY (16-06-2021)
   */
  components: {
    DraggableDiv,
  },
  methods: {
    /**
     * Đóng thông báo
     * Crated by: TVHUY (16-06-2021)
     */
    closeAlert() {
      this.isShowing = false;
      this.$bus.$emit("coverOverlay", 99999);
    },
  },
  data() {
    return {
      isShowing: false, //Trạng thái hiển thị
    };
  },
  computed: {
    /**
     * Lấy trạng thái hiển thị của thông báo
     * true: đang hiển thị
     * Created by: TVHUY (16-06-2021)
     */
    getIsShowing() {
      return this.isShowing;
    },
  },
};
</script>