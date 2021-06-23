<template>
  <div id="content">
    <div id="wrapper">
      <div class="header">
        <ul class="funcs">
          <li
            v-for="(item, index) in items"
            :key="item.id"
            class="item"
            :style="item.style.padding"
            @click="exec(index)"
          >
            <span class="box">
              <span class="icon" :style="item.style.icon"></span>
              <span class="label">{{ item.label }}</span>
            </span>
            <div
              class="separator"
              v-if="index !== items.length"
              :style="index === items.length - 1 ? 'border: none;' : ''"
            ></div>
          </li>
        </ul>
      </div>
      <div class="body">
        <div class="table">
          <div class="thead">
            <div class="row">
              <div>
                <div
                  class="cell"
                  v-for="(item, index) in theaders"
                  :key="item.id"
                  :class="item.name"
                >
                  <div class="text">
                    <span>{{ item.label }}</span>
                  </div>
                  <div class="filterh" v-if="index !== items.length - 1">
                    <div class="filterInp">
                      <div class="wrapper"></div>
                      <input type="text" class="txtInp" />
                    </div>
                    <div class="filterBtn">
                      <span class="showText"><span>∗</span></span>
                    </div>
                    <div class="filterList hide" tabindex="0" focusable="true">
                      <div
                        class="item"
                        v-for="item in filterList"
                        :key="item.id"
                      >
                        <span :style="item.style"
                          ><span>{{ item.icon }}</span> : {{ item.label }}</span
                        >
                      </div>
                    </div>
                  </div>
                  <div class="filterc" v-else>
                    <Combobox :datas="filterStatus" :isUpdated="false" />
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="tbody" ref="tbody">
            <div
              class="row"
              v-for="item in getStores"
              :key="item.id"
              @click="activeTableRow"
            >
              <div
                class="cell StoreCode"
                @dblclick="showEditForm(item, submitType.edit)"
                @click="setEditItem(item)"
              >
                <div class="text">
                  <span>{{ item.storeCode }}</span>
                </div>
              </div>
              <div
                class="cell StoreName"
                @dblclick="showEditForm(item, submitType.edit)"
                @click="setEditItem(item)"
              >
                <div class="text">
                  <span>{{ item.storeName }}</span>
                </div>
              </div>
              <div
                class="cell Address"
                @dblclick="showEditForm(item, submitType.edit)"
                @click="setEditItem(item)"
              >
                <div class="text">
                  <span>{{ item.address }}</span>
                </div>
              </div>
              <div
                class="cell PhoneNumber"
                @dblclick="showEditForm(item, submitType.edit)"
                @click="setEditItem(item)"
              >
                <div class="text">
                  <span>{{ item.phoneNumber }}</span>
                </div>
              </div>
              <div
                class="cell Status"
                @dblclick="showEditForm(item, submitType.edit)"
                @click="setEditItem(item)"
              >
                <div class="text">
                  <span>{{
                    item.status === 0 ? "Đang hoạt động" : "Ngừng hoạt động"
                  }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="footer">
        <div class="pagination">
          <div class="part part_left">
            <div class="prevBtns" ref="prevBtns">
              <span
                v-for="item in pagination.prevs"
                :key="item.id"
                :style="item.style"
                :class="item.name"
              ></span>
            </div>
            <div class="txtIn">
              <span>Trang </span>
              <input
                type="text"
                name="txtIn"
                value="1"
                ref="txtIn"
                v-model="pageIndex"
              />
              <span>trên {{ paging.totalPages }}</span>
            </div>
            <div class="nextBtns" ref="nextBtns">
              <span
                v-for="item in pagination.nexts"
                :key="item.id"
                :style="item.style"
                :class="item.name"
              ></span>
            </div>
            <div class="reload" ref="reload">
              <span
                :style="pagination.reload.style"
                :class="pagination.reload.name"
              ></span>
            </div>
            <div class="total" ref="total">
              <Combobox :datas="totalRecordPaging" :isUpdated="false" />
            </div>
          </div>
          <div class="part part_right">
            <div class="txtNote">
              <span class="txt"
                >Hiển thị
                {{
                  pageIndex === 1
                    ? 1
                    : parseInt(this.$refs.total.querySelector("input").value) *
                        (pageIndex - 1) +
                      1
                }}
                <span>-</span>
                {{
                  pageIndex === 1
                    ? stores.length
                    : parseInt(this.$refs.total.querySelector("input").value) *
                        (pageIndex - 1) +
                      stores.length
                }}
                trên {{ paging.totalRecords }} kết quả</span
              >
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
@import "../../assets/scss/common/style.css";
@import "../../assets/scss/layout/content/style.css";
</style>

<script>
import axios from "axios";
import Combobox from "../Common/MISA.Combobox.vue";
export default {
  /**
   * Khai báo sử dụng component
   * Created by: TVHUY (18-06-2021)
   */
  components: {
    Combobox,
  },
  /**
   * Khai báo các prop
   * Created by: TVHUY (18-06-2021)
   */
  props: {
    apiUrl: String,
  },
  async created() {
    /**
     * Đặt lại toàn bộ dữ liệu
     * Created by: TVHUY (19-06-2021)
     */
    this.loadData();
    /**
     * Cập nhật lại danh sách dữ liệu cửa hàng
     * Created bt: TVHUY (18-06-2021)
     */
    this.$bus.$on("updateStoreTable", (store, type) => {
      //Sau hành động lưu
      if (type === this.submitType.save) {
        this.loadData();
        this.rowSelected = null; //Bỏ chọn dòng hiện tại
      }
      //Sau hành động cập nhật dữ liệu
      else if (type === this.submitType.edit) {
        //Duyệt qua tất cả cửa hàng, kiểm tra mã cửa hàng được update có bằng với mã cửa hàng trong danh sách hay không
        for (var i = 0; i < this.stores.length; i++) {
          if (this.stores[i].storeId === store.StoreId) {
            //Cập nhật lại cửa hàng trong danh sách
            this.stores[i] = this.getStoreItem(store);
            this.$forceUpdate();
            break;
          }
        }
      }
      //Sau hành động xóa
      else if (type === this.submitType.delete) {
        //Duyệt qua tất cả cửa hàng, kiểm tra mã cửa hàng được update có bằng với mã cửa hàng trong danh sách hay không
        for (var j = 0; j < this.stores.length; j++) {
          if (this.stores[j].storeId === store.storeId) {
            //Xóa cửa hàng trong danh sách
            this.stores.splice(j, 1);
            break;
          }
        }
      }
    });
  },
  computed: {
    /**
     * Lấy danh sách cửa hàng
     * Created by: TVHUY (18-06-2021)
     */
    getStores() {
      return this.stores;
    },
    /**
     * Lấy trạng thái của hành động xóa
     * Created by: TVHUY (18-06-2021)
     */
    getIsDelete() {
      return this.isDelete;
    },
  },
  updated() {
    /**
     * Nếu có dòng dữ liệu được chọn từ trước
     * Chọn vào dòng dữ liệu đó
     */
    if (this.rowSelected !== null) {
      this.rowSelected.querySelector(".cell").click();
    } else {
      //Chọn vào dòng đầu tiên
      document.querySelector(".tbody .row .cell").click();
    }
  },
  watch: {
    /**
     * Theo dõi biến pageIndex
     * Created by: TVHUY (19-06-2021)
     */
    pageIndex() {
      this.toggleLeftPaging();
      this.toggleRightPaging();
    },

    /**
     * Theo dõi biến paging
     * Created by: TVHUY (19-06-2021)
     */
    paging: {
      handler: function() {
        //nếu chỉ số trang lơn hơn tổng số trang hoặc không phải là số
        if (this.pageIndex >= this.paging.totalPages || isNaN(this.pageIndex)) {
          //Ẩn các nút phần trang bên phải
          this.$refs.nextBtns.querySelectorAll("span").forEach((el) => {
            el.classList.add("disabled");
          });
        } else {
          //Hiện các nút phân trang bên phải
          this.$refs.nextBtns.querySelectorAll("span").forEach((el) => {
            el.classList.remove("disabled");
          });
        }
      },
      //Theo dõi sự thay đổi dữ liệu của thuộc tính của đối tượng paging
      deep: true,
    },
  },
  mounted() {
    /**
     * Lọc dữ liệu khi nhấn chọn trạng thái hoạt động
     * Created by: TVHUY (19-06-2021)
     */
    document.querySelectorAll(".filterc .menu .item").forEach((el) => {
      el.addEventListener("click", () => {
        this.filter();
      });
    });
    /**
     * Nhập để lọc dữ liệu
     * Created by: TVHUY (20-06-2021)
     */
    document.querySelectorAll(".txtInp").forEach((el) => {
      el.addEventListener("input", () => {
        this.filter();
      });
    });
    /**
     * Ẩn các nút paging bên trái
     * Created by: TVHUY (20-06-2021)
     */
    this.toggleLeftPaging();
    /**
     * Phân trang: về trang đầu
     * Created by: TVHUY (20-06-2021)
     */
    this.$refs.prevBtns
      .querySelector(".first")
      .addEventListener("click", this.firstPage);
    /**
     * Phân trang: Trang trước
     * Created by: TVHUY (20-06-2021)
     */
    this.$refs.prevBtns
      .querySelector(".prev")
      .addEventListener("click", this.prevPage);
    /**
     * Phân trang: trang tiếp theo
     * Created by: TVHUY (20-06-2021)
     */
    this.$refs.nextBtns
      .querySelector(".next")
      .addEventListener("click", this.nextPage);
    /**
     * Phân trang: trang cuối cùng
     * Created by: TVHUY (20-06-2021)
     */
    this.$refs.nextBtns
      .querySelector(".last")
      .addEventListener("click", this.lastPage);
    /**
     * NHập chỉ số trang vào ô input phân trang
     * Created by: TVHUY (20-06-2021)
     */
    this.$refs.txtIn.addEventListener("input", () => {
      if (!this.isEmpty(this.pageIndex) && this.pageIndex > 0) {
        //Lấy danh sách cửa hàng khi nhập đúng chỉ số trang
        this.getPageWhenInput();
      } else {
        //Theo dõi để ẩn hiện các nút phân trang
        this.toggleRightPaging();
        this.toggleLeftPaging();
      }
    });
    /**
     * Sự kiện load lại danh sách phân trang
     * Created by: TVHUY (20-06-2021)
     */
    this.$refs.reload.querySelector("span").addEventListener("click", () => {
      //Lấy danh sách cửa hàng theo phân trang
      this.getDataPaging(
        parseInt(this.$refs.total.querySelector(".items .item")).value,
        parseInt(this.pageIndex)
      );
    });
    /**
     * Phân trang theo tổng số  bản ghi mỗi trang
     * Created by: TVHUY (20-06-2021)
     */
    this.$refs.total.querySelectorAll(".items .item").forEach((el) => {
      el.addEventListener("click", (e) => {
        //Lấy danh sách cửa hàng theo phân trang
        this.getDataPaging(
          parseInt(e.target.innerText),
          parseInt(this.pageIndex)
        );
      });
    });
    /**
     * Nhấn vào nút lọc dữ liệu
     * Hiển thị menu điều kiện lọc dữ liệu
     * Created by: TVHUY (21-06-2021)
     */
    document.querySelectorAll(".filterBtn").forEach((el) => {
      el.addEventListener("click", (e) => {
        e.preventDefault();
        e.stopPropagation();
        el.nextElementSibling.classList.toggle("hide");
      });
    });
    /**
     * Sự kiện nhấn chuột ra ngoài khỏi danh sách điều kiện lọc
     * Created by: TVHUY (21-06-2021)
     */
    document.querySelectorAll(".filterList").forEach((el) => {
      el.addEventListener("focusout", (e) => {
        //Ẩn menu điều kiện lọc
        e.target.classList.add("hide");
      });
    });

    /**
     * Sự kiện click vào item trong menu lọc dữ liệu
     * Creatd by: TVHUY (21-06-2021)
     */
    document.querySelectorAll(".filterList .item").forEach((el) => {
      el.addEventListener("click", (e) => {
        //Hiển thị kí tự biểu thị chức năng lên nút đóng mở menu
        e.target
          .closest(".filterList")
          .previousElementSibling.querySelector(
            ".showText span"
          ).innerHTML = e.target.querySelector("span span").innerHTML;
        //Ẩn menu điều kiện lọc
        e.target.closest(".filterList").classList.add("hide");
      });
    });

    // //Duyệt tất cả các phần tử combobox lọc dữ liệu và thêm sự kiện focusin
    // document.querySelectorAll(".filterBtn").forEach((el) => {
    //   //Thêm sự kiện focusout
    //   el.addEventListener(
    //     "focusin",
    //     function () {
    //       console.log("btnin");
    //     }.bind(this)
    //   );
    // });
    //Duyệt tất cả các phần tử combobox lọc dữ liệu và thêm sự kiện focusout
    // document.querySelectorAll(".filterBtn").forEach((el) => {
    //   //Thêm sự kiện focusout
    //   el.addEventListener(
    //     "focusout",
    //     function () {
    //       console.log("btnout");
    //     }.bind(this)
    //   );
    // });
  },

  methods: {
    /**
     * Lọc dữ liệu theo các điều kiện cho trước
     * Created by: TVHUY (21-06-2021)
     */
    async filter(pageSize, pageIndex) {
      //Thêm các điều kiện lọc cho câu truy vấn
      var paramsQuery = {};
      paramsQuery.pageSize = pageSize;
      paramsQuery.pageIndex = pageIndex;
      if (!this.isEmpty(document.querySelectorAll(".txtInp")[0].value)) {
        console.log(document.querySelectorAll(".txtInp")[0].value);
        paramsQuery.storeCode = document.querySelectorAll(".txtInp")[0].value;
      }
      if (!this.isEmpty(document.querySelectorAll(".txtInp")[1].value)) {
        console.log(document.querySelectorAll(".txtInp")[1].value);
        paramsQuery.storeName = document.querySelectorAll(".txtInp")[1].value;
      }
      if (!this.isEmpty(document.querySelectorAll(".txtInp")[2].value)) {
        paramsQuery.address = document.querySelectorAll(".txtInp")[2].value;
      }
      if (!this.isEmpty(document.querySelectorAll(".txtInp")[3].value)) {
        paramsQuery.phoneNumber = document.querySelectorAll(".txtInp")[3].value;
      }
      if (
        document.querySelector(".filterc input").getAttribute("val") === "0"
      ) {
        paramsQuery.status = 0;
      } else {
        paramsQuery.status = parseInt(
          document.querySelector(".filterc input").getAttribute("val")
        );
      }
      await axios
        .get(`${this.apiUrl}/Stores/PagingFilter`, {
          //Gửi các giá trị cần lọc lên server
          params: paramsQuery,
        })
        .then((res) => {
          this.paging = res.data;
          this.stores = this.paging.listStores;
        });
    },
    /**
     * KIểm tra giá trị có rỗng hay không
     * Created by: TVHUY (20-06-2021)
     */
    isEmpty(value) {
      if (value === "" || value === null || value === undefined) return true;
      else return false;
    },
    /**
     * Ẩn hiện các nút phân trang bến phải khi chỉ số trang lớn hơn tổng số trang
     * Created by: TVHUY (21-06-2021)
     */
    toggleRightPaging() {
      if (
        this.pageIndex >= this.paging.totalPages || //Chỉ số trang > tổng số trang
        isNaN(this.pageIndex) || // Chỉ số trang không phải là số
        this.isEmpty(this.pageIndex) || //Chỉ số trang bị rỗng
        this.pageIndex <= 0 //Chỉ số trang < 0
      ) {
        //Ẩn các nút đến trang trước đó khỏi phân trang
        this.$refs.nextBtns.querySelectorAll("span").forEach((el) => {
          el.classList.add("disabled");
        });
      } else {
        //Hiện các nút đến trang sau đó khỏi phân trang
        this.$refs.nextBtns.querySelectorAll("span").forEach((el) => {
          el.classList.remove("disabled");
        });
      }
    },
    /**
     * Ẩn , hiện các nút phân trang bên trái khi chỉ số trang nhỏ hơn 1
     * Created by: TVHUY (21-06-2021)
     */
    toggleLeftPaging() {
      if (
        this.pageIndex <= 1 || //Chỉ số trng <= 1
        isNaN(this.pageIndex) || // Chỉ số trang không phải là số hợp lệ
        this.isEmpty(this.pageIndex) //Chỉ số trang rỗng
      ) {
        //Ẩn các nút về trang trước đó khỏi phân trang
        this.$refs.prevBtns.querySelectorAll("span").forEach((el) => {
          el.classList.add("disabled");
        });
      } else {
        //Hiện các nút về trang trước đó khỏi phân trang
        this.$refs.prevBtns.querySelectorAll("span").forEach((el) => {
          el.classList.remove("disabled");
        });
      }
    },
    /**
     * lấy dữ liệu phân trang khi người dùng nhấn nút mũi tên: trang đầu, trang trước, trang sau, trang cuối
     * Crated By: TVHUY (21-06-2021)
     */
    /**
     * Khi nhấn nút về trang đầu
     * Lấy dữ liệu của trang đầu tiên và hiển thị
     * Crated by: TVHUY (21-06-2021)
     */
    firstPage() {
      if (this.pageIndex > 1) {
        //Lấy dữ liệu của trang đầu
        this.getDataPaging(
          parseInt(this.$refs.total.querySelector("input[name=search]").value),
          1
        );
        this.pageIndex = 1;
      }
    },
    /**
     * Khi nhấn nút về trang trước
     * Lấy dữ liệu của trang trước đó và hiển thị
     * Crated by: TVHUY (21-06-2021)
     */
    prevPage() {
      //Lấy dữ liệu teo kích thước trang và chỉ số trang
      if (this.pageIndex > 1) {
        this.getDataPaging(
          parseInt(this.$refs.total.querySelector("input[name=search]").value),
          parseInt(this.pageIndex) - 1
        );
        //Đặt lại giá trị chỉ số trang trong ô input
        if (parseInt(this.pageIndex) > 1) {
          this.pageIndex = parseInt(this.pageIndex) - 1;
        }
      }
    },
    /**
     * Khi nhấn nút về trang kế tiếp
     * Lấy dữ liệu của trang kế tiếp đó và hiển thị
     * Crated by: TVHUY (21-06-2021)
     */
    nextPage() {
      //Kiểm tra dữ liệu
      if (
        this.pageIndex < this.paging.totalPages &&
        !this.isEmpty(this.pageIndex) &&
        this.pageIndex >= 1
      ) {
        //Lấy dữ liệu phân trang
        this.getDataPaging(
          parseInt(this.$refs.total.querySelector("input[name=search]").value),
          parseInt(this.pageIndex) + 1
        );
        //Đặt lại giá trị chỉ số trang trong ô input
        if (parseInt(this.pageIndex) < this.paging.totalPages) {
          this.pageIndex = parseInt(this.pageIndex) + 1;
        }
      }
    },
    /**
     * Khi nhấn nút về trang cuối
     * Lấy dữ liệu của trang cuối đó và hiển thị
     * Crated by: TVHUY (21-06-2021)
     */
    lastPage() {
      if (
        this.pageIndex < this.paging.totalPages &&
        parseInt(this.$refs.txtIn.value) > 0
      ) {
        this.getDataPaging(
          parseInt(this.$refs.total.querySelector("input[name=search]").value),
          this.paging.totalPages
        );
        //Đặt lại giá trị chỉ số trang trong ô input
        this.pageIndex = this.paging.totalPages;
      }
    },
    /**
     * Nhập vào ô input chỉ số trang cần xem
     * Created by: TVHUY (22-06-2021)
     */
    getPageWhenInput() {
      //Lấy dữ liệu theo giá trị của ô input nhập chỉ số trang
      this.getDataPaging(
        parseInt(this.$refs.total.querySelector("input[name=search]").value),
        parseInt(this.pageIndex)
      );
    },
    /**
     * Lấy danh sách cửa hàng theo phân trang
     * pageSize: số lượng bản ghi cần lấy
     * pageIndex: chỉ số trang băt đầu lấy
     * Created by: TVHUY (22-06-2021)
     */
    async getDataPaging(pageSize = 50, pageIndex = 1) {
      document.querySelectorAll(".txtInp").forEach((el) => {
        if (!this.isEmpty(el.value)) {
          this.isFilter = true;
          console.log('true');
        }
      });
      //Nếu có dữ liệu cần lọc
      if (this.isFilter === true) {
        this.filter(pageSize, pageIndex);
        this.isFilter = false;
      } else {
        //Phân trang không lọc dữ liệu
        try {
          var res = await axios.get(
            `${this.apiUrl}/Stores/Paging?pageSize=${pageSize}&pageIndex=${pageIndex}`
          );
          this.paging = res.data;
          this.stores = this.paging.listStores;
        } catch (error) {
          console.log(error);
        }
      }
    },
    /**
     * Trả về 1 đối tượng cửa hàng với dữ liệu được gán theo mẫu của đối tượng
     * Created by: TVHUY (20-06-201)
     */
    getStoreItem(store) {
      return {
        storeId: store.StoreId,
        storeCode: store.StoreCode,
        storeName: store.StoreName,
        address: store.Address,
        phoneNumber: store.PhoneNumber,
        storeTaxCode: store.StoreTaxCode,
        countryId: store.CountryId,
        provinceId: store.ProvinceId,
        districtId: store.DistrictId,
        wardId: store.WardId,
        street: store.Street,
        status: store.Status,
        createdDate: store.CreatedDate,
      };
    },
    /**
     * HIển thị form tạo cửa hàng
     * Created by: TVHUY (20-06-2021)
     */
    showCreateForm() {
      this.$bus.$emit("showCreateStoreForm");
    },
    /**
     * Bỏ trạng thái selected trên dòng dữ liệu
     * Crated by: TVHUY (20-06-2021)
     */
    removeActiveOnTableRow() {
      document.querySelectorAll(".row").forEach((el) => {
        el.classList.remove("table-row-selected");
      });
    },
    /**
     * Thêm trạng thái selected trên dòng dữ liệu
     * Created by: TVHUY (19-06-2021)
     */
    activeTableRow(e) {
      this.removeActiveOnTableRow();
      e.currentTarget.classList.toggle("table-row-selected");
      this.rowSelected = e.currentTarget;
    },
    /**
     * Hiển thị form nhân bản
     * Created by: TVHUy (19-06-2021)
     */
    showDuplicateForm(item) {
      this.$bus.$emit("showDuplicateStoreForm", item);
    },
    /**
     * Lấy toàn bộ cửa hàng từ CSDL
     * Craeted by: TVHUY (19-06-2021)
     */
    async loadData() {
      try {
        var res = await axios.get(`${this.apiUrl}/Stores/Paging`);
        this.paging = res.data;
        this.stores = this.paging.listStores;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Xác định hành động cần thực thi
     * Crated by: TVHUY (19-06-2021)
     */
    exec(action) {
      switch (action) {
        //Tạo cửa hàng
        case 0:
          this.showCreateForm();
          break;
        //Nhân bản cửa hàng
        case 1:
          this.showEditForm(this.editItem, this.submitType.duplicate);
          break;
        //Chỉnh sửa cửa hàng
        case 2:
          this.showEditForm(this.editItem, this.submitType.edit);
          break;
        //Xóa cửa hàng
        case 3:
          this.$bus.$emit("openDelAlert", this.editItem);
          break;
        //Nạp lại DL
        case 4:
          this.loadData();
          break;
        default:
          break;
      }
    },
    /**
     * Hiên thị form cập nhật cửa hàng
     * Creted by: TVHUY (19-06-2021)
     */
    showEditForm(item, type) {
      this.$bus.$emit("showEditStoreForm", item, type);
    },
    /**
     * Lưu lại phàn tử đang được dùng để cập nhật
     *
     */
    setEditItem(item) {
      this.editItem = item;
    },
  },
  data() {
    return {
      isFilter: false, //Trạng thái lấy dữ liệu có phải là đang lọc hay không
      pagingStart: 1, //Index đầu tiên của kết quả hiển thị
      pagingEnd: Number, //Index cuối cùng của kết quả hiển thị
      pageIndex: 1,
      paging: {}, //Dữ liệu phân trang
      //Kiểu thao tác trên form
      submitType: {
        save: 0,
        edit: 1,
        delete: 2,
        duplicate: 3,
      },
      editItem: {}, //Phần tử đang được dùng để cập nhật
      rowSelected: null, //Dòng đang được chọn
      //Dữ liệu của combobox lọc trạng thái
      filterStatus: [
        { label: "Tất cả", id: 2, selected: true },
        { label: "Đang hoạt động", id: 0, selected: false },
        { label: "Ngừng hoạt động", id: 1, selected: false },
      ],
      //Dữ liệu của combobox phân trang
      totalRecordPaging: [
        { label: 15, id: 15, selected: false },
        { label: 25, id: 25, selected: false },
        { label: 50, id: 50, selected: true },
        { label: 100, id: 100, selected: false },
      ],
      //Trạng thái focus của ô input
      isFocused: false,
      stores: [], //Dnah sách cửa hàng
      //Tiêu đề của bảng danh sách
      theaders: [
        { label: "Mã cửa hàng", name: "StoreCode" },
        { label: "Tên cửa hàng", name: "StoreName" },
        { label: "Địa chỉ", name: "Address" },
        { label: "Số điện thoại", name: "PhoneNumber" },
        { label: "Trạng thái", name: "Status" },
      ],
      //Phần tử của menu lọc DL
      filterList: [
        {
          label: "Chứa", //Nhãn hiển thị
          icon: "∗", //Icon hiển thị
          style: {
            //CSS
            "line-height": "30px !important",
          },
        },
        {
          label: "Bằng",
          icon: "=",
          style: {
            "line-height": "28px !important",
          },
        },
        {
          label: "Bắt đầu bằng",
          icon: "+",
          style: {
            "line-height": "28px !important",
          },
        },
        {
          label: "Kết thúc bằng",
          icon: "-",
          style: {
            "line-height": "28px !important",
          },
        },
        {
          label: "Không chứa",
          icon: "!",
          style: {
            "line-height": "27px !important",
          },
        },
      ],
      //Phần tử thực hiện các hành động trên form
      items: [
        {
          label: "Thêm mới", //Mã hiển thị
          style: {
            //CSS
            icon: {
              background:
                "url(" +
                require("../../../public/ImagesIcons/common-icon.png") +
                ") no-repeat -1px -126.2px",
            },
          },
        },
        {
          label: "Nhân bản",
          style: {
            icon: {
              background:
                "url(" +
                require("../../../public/ImagesIcons/common-icon.png") +
                ") no-repeat -976.5px -126.1px",
            },
          },
        },
        {
          label: "Sửa",
          style: {
            icon: {
              background:
                "url(" +
                require("../../../public/ImagesIcons/common-icon.png") +
                ") no-repeat -77px -201.7px",
            },
          },
        },
        {
          label: "Xóa",
          style: {
            icon: {
              background:
                "url(" +
                require("../../../public/ImagesIcons/common-icon.png") +
                ") no-repeat -1076px -126px",
            },
          },
        },
        {
          label: "Nạp",
          style: {
            icon: {
              background:
                "url(" +
                require("../../../public/ImagesIcons/common-icon.png") +
                ") no-repeat -876px -126px",
            },
          },
        },
      ],
      pagination: {
        prevs: [
          {
            name: "first",
            style: {
              background:
                "url(" +
                require("../../../public/ImagesIcons/common-icon.png") +
                ") no-repeat -649px -124px",
            },
          },
          {
            name: "prev",
            style: {
              background:
                "url(" +
                require("../../../public/ImagesIcons/common-icon.png") +
                ") no-repeat -700px -124px",
            },
          },
        ],
        nexts: [
          {
            name: "next",
            style: {
              background:
                "url(" +
                require("../../../public/ImagesIcons/common-icon.png") +
                ") no-repeat -749px -124px",
            },
          },
          {
            name: "last",
            style: {
              background:
                "url(" +
                require("../../../public/ImagesIcons/common-icon.png") +
                ") no-repeat -798px -124px",
            },
          },
        ],
        reload: {
          name: "reload",
          style: {
            background:
              "url(" +
              require("../../../public/ImagesIcons/common-icon.png") +
              ") no-repeat -849px -124px",
          },
        },
      },
    };
  },
};
</script>
