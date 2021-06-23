<template>
  <DraggableDiv id="alertDelete" v-show="getIsDeleting">
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
          <p>
            Bạn có chắc chắn muốn xóa
            <span class="storeName"
              ><b style="font-weight: 700">{{ this.store.storeCode }}</b></span
            >
            khỏi danh sách cửa hàng không?
          </p>
        </div>
      </div>
    </template>
    <template slot="footer">
      <div class="footer">
        <div class="agree" @click="deleteStore">
          <span class="txt">Xóa</span>
        </div>
        <div class="cancel" @click="closeAlert">
          <span class="txt">Hủy bỏ</span>
        </div>
      </div>
    </template>
  </DraggableDiv>
</template>

<style scoped>
@import "../../assets/scss/customize/alertDelete/style.css";
</style>

<script>
import DraggableDiv from "../Common/MISA.CreateFormDragable";
import axios from "axios";
export default {
  created() {
    /**
     * Sự kiện Hiển thị thông báo khi xóa cửa hàng
     * Hiển thị lớp phủ màn hình
     * Hiển thị popup thông báo
     * Crated by: TVHUY (16-06-2021)
     */
    this.$bus.$on("openDelAlert", (item) => {
      this.isDeleting = true;
      this.store = item;
      this.$bus.$emit("showOverlay");
    });
    /**
     * Đóng thông báo cảnh báo xóa cửa hàng
     * Đóng lớp phủ màn hình
     * Đóng popup thông báo
     * Created by: TVHUY (16-06-2021)
     */
    this.$bus.$on("closeDelAlert", () => {
      this.isDeleting = false;
    });
  },
  computed: {
    /**
     * Lấy thông tin trạng thái của việc xóa cửa hàng
     * true: đang thực hiện xóa
     * Created by: TVHUY (16-06-2021)
     */
    getIsDeleting() {
      return this.isDeleting;
    },
  },
  data() {
    return {
      isDeleting: false, //Trạng thái đang xóa
      store: {}, //Cửa hàng cần xóa
      submitType: {
        //Quy định loại hành động thực hiện
        create: 0,
        edit: 1,
        delete: 2,
        duplicate: 3,
      },
    };
  },
  /**
   * Khai báo prop
   * Created by: TVHUY (16-06-2021)
   */
  props: {
    apiUrl: String,
  },
  /**
   * khai báo component
   * Created by: TVHUY (16-06-2021)
   */
  components: {
    DraggableDiv,
  },
  methods: {
    /**
     * Đóng thông báo
     * Ẩn lớp phủ màn hình
     * Created by: TVHUY (16-06-2021)
     */
    closeAlert() {
      this.isDeleting = false;
      this.$bus.$emit("hideOverlay");
    },
    /**
     * Xóa cửa hàng
     * Xóa bản ghi trên CSDL theo ID
     * Đóng thông báo
     * Ẩn lớp phủ màn hình
     * Cập nhật lại danh sách cửa hàng
     * Created by: TVHUY (16-06-2021)
     */
    async deleteStore() {
      await axios
        .delete(`${this.apiUrl}/Stores/${this.store.storeId}`)
        .then(() => {
          //Sự kiện cập nhật lại danh sách cửa hàng
          this.$bus.$emit(
            "updateStoreTable",
            this.store,
            this.submitType.delete
          );
          this.isDeleting = false;
          this.$bus.$emit("hideOverlay");
        });
    },
  },
};
</script>