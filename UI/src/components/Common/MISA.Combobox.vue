<template>
  <div class="combobox">
    <div class="wrapper">
      <div class="input">
        <input
          type="search"
          id="search"
          name="search"
          autocomplete="off"
          ref="input_search"
          tabindex="0"
        />
      </div>
      <div
        class="toggler curp"
        focusable
        tabindex="-1"
        ref="menuToggler"
        @click="toggleMenu"
      ></div>
    </div>
    <div class="menu" v-show="isShowMenu">
      <div class="wrappermenu">
        <ul class="items" ref="items">
          <li
            v-for="(item, index) in datas"
            :key="index"
            :val="item.id"
            :class="index === 0 ? 'item combobox_item_active' : 'item'"
            @click="pickItem(item)"
          >
            {{ item.label }}
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<style scoped>
@import "../../assets/scss/customize/combobox/style.css";
</style>

<script>
export default {
  props: ["datas", "isUpdated", "isCreate"], //Khai báo prop
  created() {
    /**
     * Sự kiện click vào các phần tử khác nhau trên trang
     * Nếu không phải phần tử input của combobox thì đóng menu của combobox
     * Created by: TVHUY (17-06-2021)
     */
    document.addEventListener("click", (e) => {
      e.stopPropagation();
      if (e.target !== this.$refs.input_search) {
        //Nếu không phải nhấn vào input trong combobox
        this.hideMenu(); //Ẩn menu
      } else return;
    });
  },
  updated() {
    /**
     * Cập nhật lại combobox với dữ liệu có sự thay đổi thường xuyên
     * Crated by: TVHUY (17-06-2021)
     */
    if (this.isUpdated === true) {
      this.setBox();
    }
  },
  mounted() {
    //Nhấn mũi tên để chọn dữ liệu
    // this.$refs.input_search.addEventListener("keydown", (e) => {
    //   this.showMenu();
    //   //Phím mũi tên đi lên
    //   if (e.keyCode === this.keys.UP) {
    //     this.isMoving = true;
    //     --this.indexItemSelected;
    //     //nếu vị trí của phần tử được chọn < 0
    //     if (this.indexItemSelected < 0) {
    //       //Gán bằng vị trí cuối cùng
    //       this.indexItemSelected =
    //         this.$refs.items.querySelectorAll(".item").length - 1;
    //     }
    //   } else if (e.keyCode === this.keys.DOWN) {
    //     this.isMoving = true;
    //     ++this.indexItemSelected;
    //     //Nếu vị trí phần tử đang chọn vượt quá số phàn tử của list
    //     if (
    //       this.indexItemSelected ===
    //       this.$refs.items.querySelectorAll(".item").length
    //     ) {
    //       //Đưa về phần tử đầu tiên
    //       this.indexItemSelected = 0;
    //     }
    //   } else if (e.keyCode === this.keys.ENTER) {
    //     this.isMoving = true;
    //     this.$refs.items.querySelector(".combobox_item_active").click();
    //   } else {
    //     this.isMoving = false;
    //   }
    //   if (this.isMoving === true) {
    //     this.$refs.items.querySelectorAll(".item").forEach((el, index) => {
    //       if (index === this.indexItemSelected) {
    //         //Thêm avtive và đặt lại giá trị của ô nhập liệu trên combobox
    //         el.classList.add("combobox_item_active");
    //         this.$refs.input_search.value = el.innerText.trim();
    //         this.$refs.input_search.setAttribute("val", el.getAttribute("val"));
    //         el.scrollIntoView();
    //       } else {
    //         el.classList.remove("combobox_item_active");
    //       }
    //     });
    //   }
    // });
    //Nhập để tìm kiếm
    // this.$refs.input_search.addEventListener("input", (e) => {
    //   this.$refs.items.querySelectorAll(".item").forEach((el, index) => {
    //     if (el.classList.contains("combobox_item_active")) {
    //       this.indexItemSelected = index;
    //     }
    //   });
    //   this.showMenu();
    //   //Nếu giá trị nhập vào bị rỗng
    //   if (e.target.value === "") {
    //     //Hiển thị hết các item của menu
    //     this.$refs.items.querySelectorAll(".items .item").forEach((el) => {
    //       //Hiển thị item
    //       el.classList.remove("hide");
    //       el.classList.add("d-block");
    //     });
    //   } else {
    //     this.$refs.items.innerHTML = "";
    //     this.datas.forEach((item) => {
    //       if (item.label.includes(e.target.value)) {
    //         let el = document.createElement("li");
    //         el.setAttribute("val", item.id);
    //         el.setAttribute("class", "item");
    //         el.addEventListener("click", this.pickItem(item));
    //         el.innerText = item.label;
    //         this.$refs.items.appendChild(el);
    //       }
    //     });
    //     if (this.$refs.items.children.length > 0) {
    //       this.$refs.items
    //         .querySelector(".item")
    //         .classList.add("combobox_item_active");
    //     }
    //   }
    // });
    //Nếu dữ liệu gửi vào k thay đổi gì
    if (this.isUpdated === false) {
      this.setBox(); //Dặt dữ liệu combo box
    }
    //Sự kiện focusin vào ô input trong box
    this.$refs.input_search.addEventListener(
      "focusin",
      function (e) {
        this.addFocus(e); //Thêm viền
      }.bind(this)
    );
    //Sự kiện focusout vào ô input trong box
    this.$refs.input_search.addEventListener(
      "focusout",
      function (e) {
        this.removeFocus(e); //Bỏ viền
      }.bind(this)
    );
    //Sự kiện focusin vào nút mũi tên trong box
    this.$refs.menuToggler.addEventListener(
      "focusin",
      function (e) {
        this.addFocus(e); //Thêm viền
      }.bind(this)
    );
    //Sự kiện focusout vào nút mũi tên trong box
    this.$refs.menuToggler.addEventListener(
      "focusout",
      function (e) {
        this.removeFocus(e); //Bỏ viền
      }.bind(this)
    );
  },

  data() {
    return {
      // selectedItem: null, //Phần tử được chọn
      // scrollPossition: 0, //vị trí của thanh scroll trong box
      isShowMenu: false, //trạng thái ẩn hiện menu dropdown
      hasSelectedItem: false, //trạng thái đang có item đc chọn mặc định
      selecetdId: null, //id của item đc chọn
      keys: {
        //Giá trị của các phím bấm
        UP: 38, //Mũi tên lên
        DOWN: 40, //Mũi tên xuống
        ENTER: 13, //Enter
      },
      indexItemSelected: -1, //vị trí của item đang được chọn trong box
      isMoving: false, //Trạng thái dùng phím mũi tên để di chuyển trong combobox
    };
  },
  methods: {
    /**
     * Set dữ liệu cho combobox
     * Thêm các tùy chọn cho combobox
     * Created by: TVHUY (17-06-2021)
     */
    setBox() {
      //Nếu dữ liệu rỗng
      if (this.datas.length === 0) {
        this.datas.push({ label: "", id: "", selected: false }); //Đặt 1 phần tử rỗng
      } else {
        //Duyệt dữ liệu
        this.datas.forEach((item, index) => {
          //Nếu có phần tử đc chọn mặc định và không trong form tạo cửa hàng
          if (item.selected === true) {
            this.indexItemSelected = index; //Lấy vị trí của phần tử được chọn
            this.hasSelectedItem = true; //trạng thái có phần tử được chọn mặc định
            this.selecetdId = item.id; //id của phần tử được chọn
            this.$refs.input_search.value = item.label; //Đặt giá trị của input trong combobox
            this.$refs.input_search.setAttribute("val", item.id); //Đặt thuộc tính id của ô input trong combobox
          }
        });
        //Nếu không có phần tử được chọn mặc định và không trong form tạo cửa hàng
        if (this.hasSelectedItem === false) {
          this.$refs.input_search.value = this.datas[0].label; //Đặt giá trị của input trong combobox bằng phần tử đầu tiên
          this.$refs.input_search.setAttribute("val", this.datas[0].id); //Đặt thuộc tính id của ô input trong combobox bằng phần tử đầu tiên
        } else {
          //Duyệt qua tất cả phần tử trong menu
          this.$refs.items.querySelectorAll(".item").forEach((el) => {
            //Nếu là phần tử được chọn
            if (el.getAttribute("val") == this.selecetdId) {
              this.$refs.items.querySelectorAll(".item").forEach((el) => {
                //Bỏ active khỏi các phần tử còn lại
                el.classList.remove("combobox_item_active");
              });
              el.classList.add("combobox_item_active"); //Thêm active vào phần tử đc chọn
            }
          });
        }
      }
    },
    /**
     * Thay đổi Trạng thái ẩn hiện menu combobox
     * Created by: TVHUY (17-06-2021)
     */
    toggleMenu(e) {
      e.stopPropagation();
      this.isShowMenu = !this.isShowMenu;
    },
    /**
     * Kích hoạt combobox khi focus
     * Thêm viền
     * Crated by: TVHUY (17-06-2021)
     */
    addFocus(e) {
      e.currentTarget
        .closest(".combobox")
        .classList.add("input_find_store_active");
    },
    /**
     * Bỏ viền khỏi combobox khi mất focus
     * Created by: TVHUY (17-06-2021)
     */
    removeFocus(e) {
      e.currentTarget
        .closest(".combobox")
        .classList.remove("input_find_store_active");
    },
    /**
     * Hiện menu của combobox
     * Created by: TVHUY (17-06-2021)
     */
    showMenu() {
      this.isShowMenu = true;
    },
    /**
     * Ẩn menu của combobox
     * Creatd by: TVHUY (17-06-2021)
     */
    hideMenu() {
      this.isShowMenu = false;
    },
    /**
     * Sự kiện chọn 1 item trong menu
     * Thêm trạng thái kích hoạt cho item được chọn
     * Đặt giá trị cho combobox: nhãn của phần tử được chọn, giá trị của phần tử được chọn
     * Created by: TVHUY (17-06-2021)
     */
    pickItem(item) {
      //Bỏ kích hoạt khỏi các item khác
      this.$refs.items.querySelectorAll(".item").forEach((el) => {
        el.classList.remove("combobox_item_active");
        if (this.isUpdated === false && item.id == el.getAttribute("val")) {
          //Thêm active cho item đc chọn
          el.classList.add("combobox_item_active");
          //Đặt giá trị cho input trong box
          this.$refs.input_search.value = el.innerText;
          //Đặt thuộc tính id cho input trong box đc chọn
          this.$refs.input_search.setAttribute("val", el.getAttribute("val"));
        }
      });
      this.datas.forEach((it) => {
        it.selected = false;
      });
      if (this.isUpdated === true) {
        item.selected = true;
      }
    },
  },
};
</script>
