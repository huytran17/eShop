<template>
  <div id="sidebar">
    <div class="logo">
      <img :src="logo.path" alt="MISA eShop" />
    </div>
    <div class="items" ref="nav_items">
      <div
        v-for="(item, index) in items.topMenu"
        :key="item.name"
        :class="index === 3 ? 'item active_nav' : 'item'"
        @click="activeNav"
      >
        <router-link :to="item.name">
          <span :class="'icon icon-' + item.name" @click="activeParent"
            >&nbsp;</span
          >
          <span class="label" @click="activeParent">{{ item.label }}</span>
        </router-link>
      </div>
    </div>
    <div class="bottom_menu">
      <div v-for="item in items.botMenu" :key="item.name" class="item">
        <router-link :to="item.name">
          <span :class="'icon icon-' + item.name" ref="icon">&nbsp;</span>
          <span class="label" ref="label">{{ item.label }}</span>
        </router-link>
      </div>
    </div>
  </div>
</template>


<style scoped>
@import "../../assets/scss/layout/sidebar/style.css";
</style>

<script>
export default {
  data() {
    return {
      logo: {
        //Logo trên top của thanh sidebar
        path: require("../../../public/ImagesIcons/Logo_Full_White.png"),
      },
      items: {
        //Các phần tử điều hướng trên sidebar
        topMenu: [
          { label: "Tổng quan", name: "dashboard" },
          { label: "Báo cáo", name: "report" },
          { label: "Đơn hàng", name: "deleveryledger" },
          { label: "Mua hàng", name: "purchases" },
          { label: "Khuyến mại", name: "sales" },
          { label: "Quản lý cửa hàng", name: "storemanagement" },
        ],
        botMenu: [{ label: "Hàng hóa", name: "products" }],
      },
    };
  },
  methods: {
    /**
     * Kích hoạt item trên sidebar khi nhấn vào
     * Crated by: TVHUY (16-06-2021)
     */
    activeNav(e) {
      e.stopPropagation();
      //Bỏ hết trạng thái active của các item còn lại
      this.$refs.nav_items.querySelectorAll(".item").forEach((el) => {
        el.classList.remove("active_nav");
      });
      this.$refs.nav_items.querySelectorAll(".icon").forEach((el) => {
        el.classList.remove("active_nav");
      });
      this.$refs.nav_items.querySelectorAll(".label").forEach((el) => {
        el.classList.remove("active_nav");
      });
      //Thêm active vào item đang được chọn
      e.target.classList.add("active_nav");
    },
    /**
     * Kích hoạt vào phần tử trên sidebar
     * Active phần tử hiện tại
     * Bỏ active các phần tử còn lại
     * Creted by: TVHUY (16-06-2021)
     */
    activeParent(e) {
      e.stopPropagation();
      e.preventDefault();

      //Bỏ hết trạng thái active của các item còn lại
      this.$refs.nav_items.querySelectorAll(".icon").forEach((el) => {
        el.classList.remove("active_nav");
      });
      this.$refs.nav_items.querySelectorAll(".item").forEach((el) => {
        el.classList.remove("active_nav");
      });
      this.$refs.nav_items.querySelectorAll(".label").forEach((el) => {
        el.classList.remove("active_nav");
      });
      //Thêm active vào item đang được chọn
      e.target.classList.add("active_nav");
      e.target.closest(".item").classList.add("active_nav");
    },
  },
};
</script>