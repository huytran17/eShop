<template>
  <div id="overlay" ref="overlay"></div>
</template>

<style scoped>
@import "../../assets/scss/customize/overlay/style.css";
</style>

<script>
export default {
  created() {
    /**
     * Sự kiện Hiển thị overlay
     * Created by: TVHUY (18-06-2021)
     */
    this.$bus.$on("showOverlay", () => {
      this.on();
    });
    /**
     * Sự kiện Ẩn overlay
     * Crated by: TVHUY (18-06-2021)
     */
    this.$bus.$on("hideOverlay", () => {
      this.off();
    });
    /**
     * Hiển thị overlay đè lên khi có thông báo
     * Creatd by: TVHUY (18-06-2021)
     */
    this.$bus.$on("coverOverlay", (zindex) => {
      this.$refs.overlay.style.zIndex = zindex;
    });
  },
  methods: {
    /**
     * Hiển thị overlay
     * Cerated by: TVHUY (18-06-2021)
     */
    on() {
      this.$refs.overlay.style.display = "block";
    },
    /**
     * Ẩn overlay
     * Created by: TVHUY (18-06-2021)
     */
    off() {
      this.$refs.overlay.style.display = "none";
      // this.$bus.$emit("hideCreateStoreForm");
      // this.$bus.$emit('closeDelAlert');
      // this.$bus.$emit('closeAlertAlreadyExists');
    },
  },
};
</script>