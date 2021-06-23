<template>
  <div id="header">
    <div class="header_title">
      <label>{{ header.title }}</label>
    </div>
    <div class="header_function">
      <Combobox
        :datas="this.items"
        v-if="this.items.length > 0"
        :name="cbxName"
        :isUpdated="false"
      />
      <div
        class="user curp"
        ref="user_option"
        @click="toggleMenuOption"
        focusable="true"
        tabindex="0"
      >
        <div class="avatar">
          <span class="img"></span>
        </div>
        <div class="name">
          <span>{{ user.name }}</span>
        </div>
        <div class="option toggleDisplay" ref="user_menu">
          <span class="toggler"></span>
          <div class="menu" v-show="isShowMenu">
            <div class="item" v-for="item in user.menu" :key="item.id">
              <a href="#" :style="item.style">
                <span>{{ item.label }}</span>
              </a>
            </div>
          </div>
          <div class="shadow" v-show="isShowMenu"></div>
        </div>
      </div>
      <v-tooltip bottom color="#000000" max-width="150">
        <template v-slot:activator="{ on, attrs }">
          <div class="support curp" v-bind="attrs" v-on="on"></div>
        </template>
        <span>Chăm sóc khách hàng MISA eShop</span>
      </v-tooltip>
      <div class="notify curp">
        <v-tooltip bottom color="#000000">
          <template v-slot:activator="{ on, attrs }">
            <span
              class="img icon-notify"
              ref="icon_notify"
              @mouseover="iconNotifyHover"
              @mouseleave="iconNotify"
              v-bind="attrs"
              v-on="on"
            ></span>
          </template>
          <span>Thông báo</span>
        </v-tooltip>
      </div>
      <div class="separator"></div>
      <div class="help curp">
        <v-tooltip bottom color="#000000">
          <template v-slot:activator="{ on, attrs }">
            <span
              class="img icon-help"
              ref="icon_help"
              @mouseover="iconHelpHover"
              @mouseleave="iconHelp"
              v-bind="attrs"
              v-on="on"
            ></span>
          </template>
          <span>Trợ giúp</span>
        </v-tooltip>
      </div>
    </div>
  </div>
</template>

<style scoped>
@import "../../assets/scss/layout/header/style.css";
</style>

<script>
import Combobox from "../Common/MISA.Combobox.vue";

export default {
  created() {
    /**
     * Build dữ liệu danh sách tên của cửa hàng cho combobox
     * Crated by: TVHUY (19-06-2021)
     */
    this.stores.map((el) => {
      return this.items.push({
        label: el.storeName, //Nhãn hiển thị
        id: el.storeId, //id của cửa hàng
        selected: false, //Trạng thái được chọn
      });
    });
    this.items[0].selected = true;
  },

  mounted() {
    /**
     * Sự kiện focusout trên menu tùy chọn tài khoản
     * Ẩn menu tùy chọn tài khoản
     * Crated by: TVHUY (18-06-2021)
     */
    this.$refs.user_option.addEventListener(
      "focusout",
      function () {
        this.isShowMenu = false;
      }.bind(this)
    );
  },
  /**
   * Khai báo các prop
   * Craeted by: TVHUY (16-06-2021)
   */
  props: {
    stores: Array,
  },
  /**
   * Khai báo các component
   * Crated by: TVHUY (16-06-2021)
   */
  components: {
    Combobox,
  },
  data() {
    return {
      cbxName: "boxHeader", //Tên của combobox chọn cửa hàng theo tên
      items: [], //Danh sách dữ liệu đổ vào combobox
      isShowMenu: false, //Quy định việc hiển thị menu tùy chọn của người dùng
      header: {
        title: "Cửa hàng", //Tiêu đề trang hiện tại
      },
      //Thông tin user
      user: {
        name: "Trần Văn Huy", //Họ tên
        menu: [
          //Menu tùy chọn tài khoản
          {
            label: "Thay đổi mật khẩu", //Nhãn hiển thị
            style: {
              //CSS
              background:
                "url(" +
                require("../../../public/ImagesIcons/icon-change-password16.png") +
                ") no-repeat 12px center",
            },
          },
          {
            label: "Góp ý với nhà phát triển",
            style: {
              background:
                "url(" +
                require("../../../public/ImagesIcons/icon-feedback.png") +
                ") no-repeat 12px center",
            },
          },
          {
            label: "Đăng xuất",
            style: {
              background:
                "url(" +
                require("../../../public/ImagesIcons/icon-logout16.png") +
                ") no-repeat 12px center",
            },
          },
        ],
      },
      //Phần tử support trên thanh header
      support: {
        label: "Tổng đài hỗ trợ MISA SUPPORT",
      },
      //Phần tử thông báo trên thanh header
      notify: {
        label: "Thông báo",
      },
      //Phần tử trợ giúp trên thanh header
      help: {
        label: "Trợ giúp",
      },
    };
  },
  methods: {
    //Xử lí sự kiện di chuột vào icon thông báo
    iconNotifyHover() {
      this.toggleClass("notify");
    },
    //Xử lí sự kiện đưa chuột ra khỏi icon thông báo
    iconNotify() {
      this.toggleClass("notify");
    },
    //Xử lí sự kiện di chuột vào icon trợ giúp
    iconHelpHover() {
      this.toggleClass("help");
    },
    //Xử lí sự kiện đưa chuột ra khỏi icon trợ giúp
    iconHelp() {
      this.toggleClass("help");
    },
    //Chuyển đổi giữa các lớp css chứa background image để thay đổi trạng thái khi hover
    toggleClass(name) {
      switch (name.toUpperCase()) {
        //Xử lí hover cho icon thông báo
        case "NOTIFY":
          this.$refs.icon_notify.classList.toggle("icon-notify");
          this.$refs.icon_notify.classList.toggle("icon-notify-hover");
          break;
        //Xử lí hover cho icon trợ giúp
        case "HELP":
          this.$refs.icon_help.classList.toggle("icon-help");
          this.$refs.icon_help.classList.toggle("icon-help-hover");
          break;
        default:
          break;
      }
    },
    //Ẩn hiện menu chức năng của người dùng
    toggleMenuOption() {
      this.isShowMenu = !this.isShowMenu;
    },
  },
};
</script>