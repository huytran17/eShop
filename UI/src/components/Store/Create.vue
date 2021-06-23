<template>
  <DraggableDiv class="col-11" v-show="isShowCreateForm" ref="createStoreForm">
    <template slot="header">
      <div class="form_header">
        <div class="title">
          <h3>{{ formTitle }}</h3>
        </div>
        <div class="toggle" @click="hideFormAndOverlay">
          <span :style="icon.close.style"></span>
        </div>
      </div>
    </template>
    <template slot="main">
      <div
        class="form"
        ref="form"
        :style="
          isEditStatus === true ? 'min-height:465px;' : 'min-height:400px;'
        "
      >
        <div class="row" id="storeCode">
          <div class="input-group">
            <label>Mã cửa hàng <span class="req">*</span></label>
            <input
              type="text"
              class="fi required"
              name="storeCode"
              autocomplete="off"
              ref="storeCode"
              autofocus
              :style="styles"
            />
            <div class="tttip tstoreCode">
              <div class="tooltip2">
                <span class="tooltiptext2"
                  >Mã số định danh để phân biệt các cửa hàng.</span
                >
              </div>
            </div>
          </div>

          <div class="tterr">
            <div class="tooltip">
              <div class="error_icon error_required">
                <span :style="errorIcon"></span>
              </div>
              <span class="tooltiptext">Trường này không được để trống</span>
            </div>
          </div>
        </div>
        <div class="row" id="storeName">
          <div class="input-group">
            <label>Tên cửa hàng <span class="req">*</span></label>
            <input
              type="text"
              class="fi required"
              name="storeName"
              autocomplete="off"
              :style="styles"
            />
            <div class="tttip tstoreName">
              <div class="tooltip2">
                <span class="tooltiptext2"
                  >Tên gọi đang lưu hành của cửa hàng.</span
                >
              </div>
            </div>
          </div>
          <div class="tterr">
            <div class="tooltip">
              <div class="error_icon error_required">
                <span :style="errorIcon"></span>
              </div>
              <span class="tooltiptext">Trường này không được để trống</span>
            </div>
          </div>
        </div>
        <div class="row" id="address">
          <div class="input-group">
            <label>Địa chỉ <span class="req">*</span></label>
            <textarea
              name="address"
              cols="30"
              rows="10"
              class="fi required"
              autocomplete="off"
              :style="styles"
            ></textarea>
            <div class="tttip taddress">
              <div class="tooltip2">
                <span class="tooltiptext2">Địa chỉ của cửa hàng.</span>
              </div>
            </div>
          </div>
          <div class="tterr">
            <div class="tooltip">
              <div class="error_icon error_required">
                <span :style="errorIcon"></span>
              </div>
              <span class="tooltiptext">Trường này không được để trống</span>
            </div>
          </div>
        </div>
        <div class="row gdbl">
          <div class="input-group" id="phoneNumber">
            <label>Số điện thoại</label>
            <input
              type="text"
              class="fi iit onlynum"
              name="phoneNumber"
              autocomplete="off"
              :style="styles"
              @input="typeNumber"
            />
          </div>
          <div class="input-group" id="storeTaxCode">
            <label>Mã số thuế</label>
            <input
              type="text"
              class="fi"
              name="storeTaxCode"
              autocomplete="off"
              :style="styles"
            />
          </div>
          <div class="tterr">
            <div class="tooltip phoneOnlyNum">
              <div class="error_icon error_required">
                <span :style="errorIcon"></span>
              </div>
              <span class="tooltiptext">Giá trị của ô này không hợp lệ.</span>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="input-group" id="country">
            <label>Quốc gia</label>
            <Combobox
              :datas="getCountries"
              class="iit combobox"
              id="boxCountry"
              :isUpdated="true"
            />
          </div>
        </div>
        <div class="row gdbl">
          <div class="input-group bx" id="province">
            <label>Tỉnh/Thành phố</label>
            <Combobox
              :datas="getProvinces"
              class="iit q1 combobox"
              id="boxProvince"
              :isUpdated="true"
              :isCreate="getIsCreate"
            />
          </div>
          <div class="input-group bx" id="district">
            <label>Quận/Huyện</label>
            <Combobox
              :datas="getDistricts"
              class="iit q2 combobox"
              id="boxDistrict"
              :isUpdated="true"
            />
          </div>
        </div>
        <div class="row gdbl">
          <div class="input-group bx" id="ward">
            <label>Phường/Xã</label>
            <Combobox
              :datas="getWards"
              class="iit q3 combobox"
              id="boxWard"
              :isUpdated="true"
            />
          </div>
          <div class="input-group" id="street">
            <label>Đường phố</label>
            <input
              type="text"
              class="fi"
              name="street"
              autocomplete="off"
              :style="styles"
            />
          </div>
        </div>
        <div class="row" v-if="isEditStatus">
          <div class="input-group" id="checkboxStatus">
            <v-checkbox
              label="Ngừng hoạt động"
              color="#00577b"
              v-model="store.Status"
            ></v-checkbox>
          </div>
        </div>
      </div>
    </template>
    <template slot="footer">
      <div class="formfooter">
        <div class="tip" focusable tabindex="0">
          <span :style="icon.tips.style"></span>
          <span>Trợ giúp</span>
        </div>
        <div class="btns">
          <div
            class="btnSave"
            tabindex="0"
            @click="
              saveStore(isEdit === true ? submitType.edit : submitType.save)
            "
          >
            <span :style="icon.btnSave.style"></span>
            <span>Lưu</span>
          </div>
          <div
            class="btnSaveAndAdd"
            tabindex="0"
            @click="
              saveStore(
                isEdit === true ? submitType.edit : submitType.save,
                true
              )
            "
          >
            <span :style="icon.btnSaveAndAdd.style"></span>
            <span>Lưu và thêm mới</span>
          </div>
          <div class="btnCancel" tabindex="0" @click="hideFormAndOverlay">
            <span :style="icon.btnCancel.style"></span>
            <span>Hủy bỏ</span>
          </div>
        </div>
      </div>
    </template>
  </DraggableDiv>
</template>

<style scoped>
@import "../../assets/scss/store/create/style.css";
</style>

<script>
import DraggableDiv from "../Common/MISA.CreateFormDragable";
import Combobox from "../Common/MISA.Combobox.vue";
import axios from "axios";
import moment from "moment";
export default {
  //Khai báo prop
  props: {
    apiUrl: String,
  },
  methods: {
    /**
     * Khai báo sử dụng thư viện momentjs
     * Created by: TVHUY (20-06-2021)
     */
    moment: function () {
      return moment();
    },
    /**
     * Kiểm tra dữ liệu có rỗng hay k
     * Created by: TVHUY (20-06-2021)
     */
    isEmpty(value) {
      if (
        value === null ||
        value === "" ||
        value === undefined ||
        value === "null" ||
        value === "undefined"
      )
        return true;
      return false;
    },
    /**
     * Thực hiện lưu trữ dwx liệu
     * Created by: TVHUY (18-06-2021)
     */
    async saveStore(type, saveAndCreate = false) {
      //Danh sách các phần tử validate không hợp lệ
      this.validateFails = [];

      //Gán dữ liệu trên form vào đối tượng cửa hàng
      this.store.StoreCode = this.getInputValue(
        document.querySelector("input[name=storeCode]").value
      );
      this.store.StoreName = this.getInputValue(
        document.querySelector("input[name=storeName]").value
      );
      this.store.Address = this.getInputValue(
        document.querySelector("textarea[name=address]").value
      );
      this.store.PhoneNumber = this.getInputValue(
        document.querySelector("input[name=phoneNumber]").value
      );
      this.store.StoreTaxCode = this.getInputValue(
        document.querySelector("input[name=storeTaxCode]").value
      );
      this.store.Street = this.getInputValue(
        document.querySelector("input[name=street]").value
      );
      this.store.CountryId = this.getInputValue(
        document
          .querySelector("#country input[type=search]")
          .getAttribute("val")
      );
      this.store.ProvinceId = this.getInputValue(
        document.querySelector("#province [type=search]").getAttribute("val")
      );
      this.store.DistrictId = this.getInputValue(
        document.querySelector("#district [type=search]").getAttribute("val")
      );
      this.store.WardId = this.getInputValue(
        document.querySelector("#ward [type=search]").getAttribute("val")
      );
      //Validate dữ liệu
      document.querySelectorAll(".required").forEach((el) => {
        //Nếu bỏ trống các dữ liệu bát buộc
        if (el.value === null || el.value === "") {
          //Thêm phần tử lỗi vào mảng
          this.validateFails.push(el);
        }
      });
      //Nếu không có lỗi
      if (this.validateFails.length == 0 && this.phoneIsNotNum === false) {
        //Hành động thêm cửa hàng
        if (type === this.submitType.save) {
          //Kiểm tra mã cửa hàng tồn tại hay chưa
          if ((await this.isExistsStoreCode(this.store.StoreCode)) === true) {
            //Hiển thị thông báo
            this.$bus.$emit("showAlertAlreadyExists");
            return false;
          } else {
            this.store.Status = this.store.Status === true ? 1 : 0;
            this.store.CreatedDate = this.moment().format();
            //Thực hiện lưu trữ DL
            await axios({
              method: "post",
              url: `${this.apiUrl}/Stores`,
              data: this.store,
            }).then(() => {
              //Cập nhật lại danh sách DL
              this.$bus.$emit("updateStoreTable", this.store, type);
            });
          }
        }
        //Cập nhật thông tin cửa hàng
        else if (type === this.submitType.edit) {
          if (this.store.StoreCode !== this.itemEdit.storeCode) {
            //Kiểm tra mã cửa hàng tồn tại hay chưa
            if ((await this.isExistsStoreCode(this.store.StoreCode)) === true) {
              //Hiển thị thông báo
              this.$bus.$emit("showAlertAlreadyExists");
              return;
            }
          }
          this.store.Status = this.store.Status === true ? 1 : 0;
          this.store.ModifiedDate = this.moment().format();
          //Thực hiện cập nhật dữ liệu
          await axios({
            method: "put",
            url: `${this.apiUrl}/Stores/${this.store.StoreId}`,
            data: this.store,
          }).then(() => {
            //Cập nhật lại danh sách dữ liệu
            this.$bus.$emit("updateStoreTable", this.store, type);
          });
        }
        //Hành động "Lưu và thêm mới"
        if (saveAndCreate === true) {
          //Đặt lại giá trị của các ô nhập liệu
          document
            .querySelectorAll(".form input:not(.combobox input)")
            .forEach((el) => {
              el.value = "";
            });
          document.querySelector(".form textarea").value = "";
          //Xóa dữ liệu combobox
          this.resetBox();
          //Lấy dữ liệu combobox quốc gia
          await axios.get(`${this.apiUrl}/Countries`).then((res) => {
            this.countries = [];
            //Đưa dữ liệu vào danh sách quốc gia
            res.data.map((el) => {
              return this.countries.push({
                label: el.countryName,
                id: el.countryId,
                selected: false,
              });
            });
            //Chọn phần tử để hiển thị
            this.countries[0].selected = true;
          });
          this.isEdit = false;
        } else {
          this.hideFormAndOverlay(); //Đóng form
        }
      } else {
        /**
         * Nếu validate fail
         * Hiển thị cảnh báo lỗi
         * Focus vào vị trí lỗi
         * Created by: TVHUY (18-06-2021)
         */
        this.validateFails.forEach((el, index) => {
          //Hiển thị lỗi
          el.closest(".row").classList.add("inputErrors");
          if (index === 0) el.focus();
        });
      }
    },
    /**
     * Kiểm tra giá trị đầu vào có là kiểu số hay không
     * Created by: TVHUY (20-06-2021)
     */
    typeNumber(e) {
      if (e.target.value !== "") {
        //Nếu không phải là số
        if (isNaN(e.target.value) || isNaN(parseFloat(e.target.value))) {
          this.phoneIsNotNum = true;
          //Hiển thị lỗi
          e.target.closest(".row").classList.add("inputErrors");
        } else {
          //Bỏ hiển thị cảnh báo lỗi
          e.target.closest(".row").classList.remove("inputErrors");
          this.phoneIsNotNum = false;
        }
      } else {
        //Bỏ hiển thị cảnh báo lỗi
        e.target.closest(".row").classList.remove("inputErrors");
        this.phoneIsNotNum = false;
      }
    },
    //TODO: alert khi đóng form
    //Hiển thị form với lớp phủ mờ
    /**
     * Đóng form cùng lớp phủ mờ và các thông báo
     * Created by: TVHUY (20-06-2021)
     */
    hideFormAndOverlay() {
      this.$bus.$emit("hideOverlay");
      this.$bus.$emit("hideCreateStoreForm");
      this.$bus.$emit("closeDelAlert");
      this.$bus.$emit("closeAlertAlreadyExists");
    },

    /**
     * Kiểm tra mã cửa hàng tồn tại hay chưa
     * Created by: TVHUY (20-06-2021)
     */
    async isExistsStoreCode(storeCode) {
      var res = await axios.get(`${this.apiUrl}/Stores/exists/${storeCode}`);
      //Nếu mã cửa hàng đã tồn tại
      if (res.data === true) {
        return true;
      }
      return false;
    },
    /**
     * Lấy danh sách tỉnh thành theo mã quốc gia
     * Created by: TVHUY (21-06-2021)
     */
    async getProvincesByCountryId(countryId) {
      await axios
        .get(`${this.apiUrl}/Provinces/ref/${countryId}`)
        .then((res) => {
          this.provinces = [];
          res.data.map((item) => {
            //Đẩy dữ liệu vào mảng danh sách tỉnh thành
            return this.provinces.push({
              label: item.provinceName,
              id: item.provinceId,
              selected:
                this.store.ProvinceId === item.provinceId ? true : false,
            });
          });
        });
    },
    /**
     * Lấy dnah sách quận huyện theo mã tỉnh thành
     * Created by: TVHUY (21-06-2021)
     */
    async getDistrictsByProvinceId(provinceId) {
      await axios
        .get(`${this.apiUrl}/Districts/ref/${provinceId}`)
        .then((res) => {
          this.districts = [];
          res.data.map((item) => {
            //Đẩy dữ liệu vào danh sách quận huyện
            return this.districts.push({
              label: item.districtName,
              id: item.districtId,
              selected:
                this.store.DistrictId === item.districtId ? true : false,
            });
          });
        });
    },
    /**
     * Lấy danh sách phường xã theo mã quận huyện
     * Created by: TVHUY (21-06-2021)
     */
    async getWardsByDistrictId(districtId) {
      await axios.get(`${this.apiUrl}/Wards/ref/${districtId}`).then((res) => {
        this.wards = [];
        res.data.map((item) => {
          //Đẩy dữ liệu vào mảng phường xã
          return this.wards.push({
            label: item.wardName,
            id: item.wardId,
            selected: this.store.WardId === item.wardId ? true : false,
          });
        });
      });
    },
    /**
     * Sự kiện nhấn vào tên 1 quốc gia trong combobox
     * Tải các tỉnh thành tương ứng
     * Created byL TVHUY (21-06-2021)
     */
    countryItemClick(e) {
      //Lấy id quốc gia
      let countryId = e.target.getAttribute("val");
      //Lấy tỉnh thành theo mã QG
      this.getProvincesByCountryId(countryId);
      this.resetBoxes(
        //Xóa dữ liệu các box còn lại
        [
          document.getElementById("province"),
          document.getElementById("district"),
          document.getElementById("ward"),
        ],
        e
      );
    },
    /**
     * Sự kiện nhấn vào tên 1 tỉnh thành trong combobox
     * Lấy dữ liệu các quận huyện tương ứng
     * Created by: TVHUY (21-06-2021)
     */
    provinceItemClick(e) {
      //Lấy id của tỉnh thành
      let provinceId = e.target.getAttribute("val");
      //Lấy quận huyện theo mã tỉnh thành
      this.getDistrictsByProvinceId(provinceId);
      this.resetBoxes(
        //Xóa dữ liệu các box còn lại
        [document.getElementById("district"), document.getElementById("ward")],
        e
      );
    },
    /**
     * Sự kiện nhấn vào tên 1 quận huyện trong combobox
     * Lấy dữ liệu các phường xã tương ứng
     */
    districtItemClick(e) {
      //Lấy id của quận huyện
      let districtId = e.target.getAttribute("val");
      //Lấy phường xã theo mã quận huyện
      this.getWardsByDistrictId(districtId);
      //Xóa dữ liệu các box còn lại
      this.resetBoxes([document.getElementById("ward")], e);
    },
    /**
     * Xóa dữ liệu của combobox
     * Created by: TVHUY (21-06-2021)
     */
    resetBoxes(els, e) {
      els.forEach((el) => {
        let inp = el.querySelector("input");
        if (inp !== null && inp !== e.target) {
          inp.setAttribute("placeholder", "Nhập để tìm kiếm");
          inp.value = "";
          inp.setAttribute("val", "");
        }
      });
    },
    /**
     * Sự kiện focus vào ô input bị lỗi
     * Created by: TVHUY (21-06-2021)
     */
    inputErrorFocus(e) {
      if (e.target.value === null || e.target.value === "") {
        //Thêm viền và thông báo lỗi
        e.target.closest(".row").classList.add("inputErrors");
      }
      //Bỏ viền và tắt thông báo lỗi
      else e.target.closest(".row").classList.remove("inputErrors");
    },
    /**
     * Đặt lại trạng thái các ô input về bình thường
     * Created by: TVHUY (21-06-2021)
     */
    resetInputError() {
      document.getElementsByClassName("tooltip").forEach((el) => {
        //Bỏ viền và tắt thông báo lỗi tắt
        el.closest(".row").classList.remove("inputErrors");
      });
    },
    /**
     * Gán dữ liệu của 1 cửa hàng cho các ô input tương ứng
     * Created by: TVHUY (20-06-2021)
     */
    bindDataIntoForm(item) {
      //Trỏ tới các ô nhập liệu trên form và gắn giá các trị tương ứng lấy từ đối tượng cửa hàng
      document.querySelector("input[name=storeCode]").value = item.storeCode;
      document.querySelector("input[name=storeName]").value = item.storeName;
      document.querySelector("textarea[name=address]").value = item.address;
      document.querySelector("input[name=phoneNumber]").value =
        item.phoneNumber;
      document.querySelector("input[name=storeTaxCode]").value =
        item.storeTaxCode;
      document.querySelector("input[name=street]").value = item.street;

      this.store.StoreId = item.storeId;
      this.store.StoreCode = item.storeCode;
      this.store.StoreName = item.storeName;
      this.store.Address = item.address;
      this.store.PhoneNumber = item.phoneNumber;
      this.store.StoreTaxCode = item.storeTaxCode;
      this.store.Street = item.street;
      this.store.CountryId = item.countryId;
      this.store.ProvinceId = item.provinceId;
      this.store.DistrictId = item.districtId;
      this.store.WardId = item.wardId;
      this.store.CreatedDate = item.createdDate;
      this.store.ModifiedDate = item.modifiedDate;
    },
    //Đặt lại dữ liệu cho các ô input
    resetInput() {
      document.querySelectorAll(".fi").forEach((el) => {
        el.value = "";
      });
    },
    //Đặt lại dữ liệu cho các combobox
    resetBox() {
      this.$refs.form.querySelectorAll(".combobox input").forEach((el) => {
        el.value = "";
        el.setAttribute("val", "");
      });
      this.itemEdit = {};
      this.countries = [];
      this.provinces = [];
      this.districts = [];
      this.wards = [];
    },
    //Đặt dữ liệu cho ô combobox chọn quốc gia
    async setStoreBox() {
      await axios.get(`${this.apiUrl}/Countries`).then((res) => {
        this.countries = [];
        res.data.map((el) => {
          return this.countries.push({
            label: el.countryName,
            id: el.countryId,
            selected: false,
          });
        });
        this.countries[0].selected = true; //Chọn phần tử đầu tiên
        this.getProvincesByCountryId(res.data[0].countryId); //Lấy danh sach tỉnh thành theo mã QG
      });
    },
    /**
     * Lấy giá trị của ô input
     * Created by: TVHUY (19-06-2021)
     */
    getInputValue(val) {
      if (this.isEmpty(val)) {
        return null;
      } else return val;
    },
  },
  /**
   * Khai báo các component
   * Created by: TVHUY (15-06-2021)
   */
  components: {
    DraggableDiv,
    Combobox,
  },
  /**
   * Theo dõi sự thay đổi dữ liệu
   * Created by: TVHUY (15-06-2021)
   */
  watch: {
    /**
     * Theo dõi sự thay đổi dữ liệu của biến isEdit
     * true: hiển thị form chỉnh sửa
     * false; ẩn form chỉnh sửa
     * Created by: TVHUY (16-06-2021)
     */
    async isEdit() {
      if (this.isEdit === true) {
        this.formTitle = "Chỉnh sửa cửa hàng"; //Đặt lại tiêu đề form
        if (!this.isEmpty(this.store.CountryId)) {
          //Mếu có mã quốc gia
          //Lấy dữ liệu quốc gia
          await axios.get(`${this.apiUrl}/Countries`).then((res) => {
            this.countries = []; //Reset giá trị của mảng các quốc gia
            //Duyệt kết quả trả về
            res.data.map((item) => {
              //Thêm phần tử vào mảng quốc gia
              return this.countries.push({
                label: item.countryName,
                id: item.countryId,
                selected:
                  this.store.CountryId === item.countryId ? true : false, //Đặt lại trạng thái để hiển thị trên checkbox
              });
            });
          });
          if (!this.isEmpty(this.store.ProvinceId)) {
            //Nếu cửa hàng có mã tỉnh thành
            this.getProvincesByCountryId(this.store.CountryId); //Lấy danh sách tỉnh thành theo mã QG
            if (!this.isEmpty(this.store.DistrictId)) {
              //Nếu cửa hàng có mã quận huyện
              this.getDistrictsByProvinceId(this.store.ProvinceId); //Lấy danh sách quận huyện theo mã QG
              if (!this.isEmpty(this.store.WardId)) {
                //Nếu cửa hàng có mã phường xã
                this.getWardsByDistrictId(this.store.DistrictId); //Lấy danh sách phường xã theo mã QG
              }
            }
          }
        }
        //Đặt dữ liệu ban đầu cho combobox trong from thêm mới
        else this.setStoreBox();
      }
    },
    /**
     * Theo dõi biến isDuplicate
     * true: hiển thị form nhân bản
     * false: ẩn form nhân bản
     * Created by: TVHUY (17-06-2021)
     */
    async isDuplicate() {
      if (this.isDuplicate === true) {
        this.formTitle = "Nhân bản cửa hàng"; //Đặt lại tiêu đề form
        if (!this.isEmpty(this.store.CountryId)) {
          //Mếu có mã quốc gia
          await axios.get(`${this.apiUrl}/Countries`).then((res) => {
            this.countries = []; //Reset giá trị của mảng các quốc gia
            //Duyệt kết quả trả về
            res.data.map((item) => {
              //Thêm phần tử vào mảng quốc gia
              return this.countries.push({
                label: item.countryName,
                id: item.countryId,
                selected:
                  this.store.CountryId === item.countryId ? true : false, //Đặt lại trạng thái để hiển thị trên checkbox
              });
            });
          });
          if (!this.isEmpty(this.store.ProvinceId)) {
            //Nếu cửa hàng có mã tỉnh thành
            this.getProvincesByCountryId(this.store.CountryId); //Lấy danh sách tỉnh thành theo mã QG
            if (!this.isEmpty(this.store.DistrictId)) {
              //Nếu cửa hàng có mã quận huyện
              this.getDistrictsByProvinceId(this.store.ProvinceId); //Lấy danh sách quận huyện theo mã QG
              if (!this.isEmpty(this.store.WardId)) {
                //Nếu cửa hàng có mã phường xã
                this.getWardsByDistrictId(this.store.DistrictId); //Lấy danh sách phường xã theo mã QG
              }
            }
          }
        } else this.setStoreBox();
      }
    },
    /**
     * Theo dõi biến isShowCreateForm
     * true: thay đổi tiêu đề và cập nhật lại form
     * Created by: TVHUY(17-06-2021)
     */
    isShowCreateForm() {
      if (this.isCreate === true) {
        this.formTitle = "Thêm mới cửa hàng"; //Đặt lại tiêu đề form
      }

      if (this.isShowCreateForm === true) {
        //Nếu đang hiện form
        this.$refs.createStoreForm.$forceUpdate; //Cập nhật lại form
      }
    },
    //Ctrl + s: thêm cửa hàng
    // saveShortcut(e) {
    //   e.preventDefault();
    //   if (e.ctrlKey && (e.key === 's' || e.key === 'S')) {
    //     this.saveStore(
    //       this.isEdit === true ? this.submitType.edit : this.submitType.save
    //     );
    //   }
    //   console.log(e);
    // },
    // //Ctrl + shift + s: nhân bản cửa hàng
    // saveAndCreateShortcut(e) {
    //   e.preventDefault();
    //   if (e.ctrlKey && e.shiftKey && (e.key === 's' || e.key === 'S')) {
    //     this.saveStore(
    //       this.isEdit === true ? this.submitType.edit : this.submitType.save
    //     );
    //   }
    // },
    // //Ctrl + b: hủy bỏ thêm, sửa cửa . đóng form
    // closeFormShortcut(e) {
    //   e.preventDefault();
    //   if (e.ctrlKey && (e.key === 'b' || e.key === 'B')) {
    //     this.hideFormAndOverlay();
    //   }
    // },
  },
  updated() {
    document.querySelector("#storeCode input").focus(); //Focus vào ô input đầu tiên

    /**
     * Hủy sự kiện trên item trong combobox quốc gia
     * Created by: TVHUY (17-06-2021)
     */
    document.querySelectorAll("#boxCountry .menu .item").forEach((el) => {
      el.removeEventListener("click", this.countryItemClick);
    });
    /**
     * Thêm lại sự kiện trên item trong combobox quốc gia
     * Created by: TVHUY (17-06-2021)
     */
    document.querySelectorAll("#boxCountry .menu .item").forEach((el) => {
      el.addEventListener("click", this.countryItemClick);
    });
    /**
     * Hủy sự kiện trên item trong combobox tỉnh thành
     * Created by: TVHUY (17-06-2021)
     */
    document.querySelectorAll("#boxProvince .menu .item").forEach((el) => {
      el.removeEventListener("click", this.provinceItemClick);
    });
    /**
     * Thêm lại sự kiện trên item trong combobox tỉnh thành
     * Created by: TVHUY (17-06-2021)
     */
    document.querySelectorAll("#boxProvince .menu .item").forEach((el) => {
      el.addEventListener("click", this.provinceItemClick);
    });
    /**
     * Hủy sự kiện trên item trong combobox quận huyện
     * Created by: TVHUY (17-06-2021)
     */
    document.querySelectorAll("#boxDistrict .menu .item").forEach((el) => {
      el.removeEventListener("click", this.districtItemClick);
    });
    /**
     * Thêm sự kiện trên item trong combobox quận huyện
     * Created by: TVHUY (17-06-2021)
     */
    document.querySelectorAll("#boxDistrict .menu .item").forEach((el) => {
      el.addEventListener("click", this.districtItemClick);
    });
    /**
     * Hủy sự kiện trên các ô input có ràng buộc dữ liệu
     * Created by: TVHUY (17-06-2021)
     */
    document.querySelectorAll(".required").forEach((el) => {
      el.removeEventListener("focusout", this.inputErrorFocus);
    });
    /**
     * Thêm lại sự kiện trên các ô input có ràng buộc dữ liệu
     * Created by: TVHUY (17-06-2021)
     */
    document.querySelectorAll(".required").forEach((el) => {
      el.addEventListener("focusout", this.inputErrorFocus);
    });
  },
  mounted() {
    /**
     * Khởi tạo mặc định cho các ô input trong form
     * Created by: TVHUY (17-06-2021)
     */
    document.getElementsByClassName("bx").forEach((el) => {
      let inp = el.querySelector("input");
      if (inp !== null) {
        inp.setAttribute("placeholder", "Nhập để tìm kiếm");
        inp.value = "";
      }
    });

    /**
     * Sự kiện nhấn nút lưu bản ghi
     * Created by: TVHUY (17-06-2021)
     */
    document.querySelector(".btnSave").addEventListener("keyup", (e) => {
      if (e.key === "Enter") {
        //THực hiện lưu trữ dữ liệu
        this.saveStore(
          this.isEdit === true ? this.submitType.edit : this.submitType.save
        );
      }
    });
    /**
     * Sự kiện nhấn nút hủy dùng form
     * Created by: TVHUY (17-06-2021)
     */
    document.querySelector(".btnCancel").addEventListener("keyup", (e) => {
      if (e.key === "Enter") {
        this.hideFormAndOverlay(); //Ẩn form và các lớp phủ màn hình
      }
    });

    document.querySelector(".btnSaveAndAdd").addEventListener("keyup", (e) => {
      if (e.key === "Enter") {
        this.saveStore(
          this.isEdit === true ? this.submitType.edit : this.submitType.save
        );
      }
    });
    // //Tạo phím tắt
    // document.addEventListener("keyup", this.saveShortcut);
    // document.addEventListener("keyup", this.saveAndCreateShortcut);
    // document.addEventListener("keyup", this.closeFormShortcut);
  },

  async created() {
    /**
     * Sự kiện hiển thị form tạo cửa hàng
     * Created by: TVHUY (18-06-2021)
     */
    this.$bus.$on("showCreateStoreForm", async () => {
      this.$bus.$emit("showOverlay");
      this.isShowCreateForm = true;
      this.isCreate = true;
      this.isEdit = false;
      this.isDuplicate = false;
      this.phoneIsNotNum = false;
      this.resetInputError(); //Xóa các thông báo lỗi trên các input

      this.setStoreBox(); //Đựt lại combox quốc gia và tỉnh thành
    });
    /**
     * Sự kiện hiển thị form chỉnh sửa cửa hàng
     * Created by: TVHUY (18-06-2021)
     */
    this.$bus.$on("showEditStoreForm", (item, type) => {
      //Nếu là hành động chỉnh sửa
      if (type === this.editFormType.edit) {
        this.isEdit = true;
        this.itemEdit = item;
        this.isDuplicate = false;
        this.isEditStatus = true;
      }
      //Nếu là hành động nhân bản
      else if (type === this.editFormType.duplicate) {
        this.isDuplicate = true;
        this.itemEdit = item;
        this.isEdit = false;
        this.isEditStatus = false;
      }
      this.isCreate = false;
      this.isShowCreateForm = true;
      this.phoneIsNotNum = false;
      this.$bus.$emit("showOverlay"); //Hiển thị lớp phủ mờ
      this.store.Status = item.status === 0 ? false : true; //Thiết lập trạng thái hiển thị tren checkbox

      this.bindDataIntoForm(item); //Gán dữ liệu cho form
      this.resetInputError(); //Reset lỗi trên các ô input
    });
    /**
     * Sự kiện ẩn form tạo cửa hàng
     * Crared by: TVHUY (18-06-2021)
     */
    this.$bus.$on("hideCreateStoreForm", () => {
      this.isShowCreateForm = false;
      this.isUpdated = false;
      this.isEdit = false;
      this.isCreate = false;
      this.isDuplicate = false;
      this.isEditStatus = false;

      this.resetInput(); //Đặt lại giá trị các ô input
      this.resetInputError(); //Reset lỗi trên các ô input
      this.resetBox(); //Đặt lại dữ liệu các ô combobox
    });
    /**
     * Lấy danh sách quốc gia
     * Created by: TVHUY (18-06-2021)
     */
    await axios.get(`${this.apiUrl}/Countries`).then((res) => {
      this.countries = [];
      //Đưa dữ liệu vào danh sách quốc gia
      res.data.map((el) => {
        //Đưa dữ liệu vào mảng quốc gia
        return this.countries.push({
          label: el.countryName,
          id: el.countryId,
          selected: false,
        });
      });
      this.countries[0].selected = true; //Chọn phần tử để hiển thị
    });
  },
  computed: {
    /**
     * Lấy trạng thái đang tạo mới cửa hàng
     * Created by: TVHUY (18-06-2021)
     */
    getIsCreate() {
      return this.isCreate;
    },
    /**
     * Láy danh sách quốc gia
     * Created by: TVHUY (18-06-2021)
     */
    getCountries() {
      return this.countries;
    },
    /**
     * Lấy danh sách tỉnh thành
     * Created by: TVHUY (18-06-2021)
     */
    getProvinces() {
      return this.provinces;
    },
    /**
     * Lấy danh sách quận huyện
     * Created by: TVHUY (18-06-2021)
     */
    getDistricts() {
      return this.districts;
    },
    /**
     * Lấy danh sách phường xã
     * Created by: TVHUY (18-06-2021)
     */
    getWards() {
      return this.wards;
    },
  },
  data() {
    return {
      phoneIsNotNum: false, //trạng thái xác thực của số điện thoại
      isEditStatus: false, //trạng thái hiển thị checkbox trạng thái
      formTitle: "", //Tiêu đề form
      //Quy định các hành động trên form
      submitType: {
        save: 0,
        edit: 1,
        delete: 2,
        duplicate: 3,
      },
      //Quy định các hành động trên form
      editFormType: {
        save: 0,
        edit: 1,
        delete: 2,
        duplicate: 3,
      },
      itemEdit: {}, //Phần tử đang chọn để edit
      isEdit: false, //Trjang thái đang cập nhật
      isDuplicate: false, //Trạng thái đang nhân bản
      isCreate: false, //Trạng thái đang tạo
      validateFails: [], //Danh sách phần tử lỗi
      //đối tượng Cửa hàng
      store: {
        StoreId: "4826f0a3-015d-4ff0-b23e-9434be479fd6",
        StoreCode: null,
        StoreName: null,
        Address: null,
        PhoneNumber: null,
        StoreTaxCode: null,
        CountryId: null,
        ProvinceId: null,
        DistrictId: null,
        WardId: null,
        Street: null,
        Status: true,
        CreatedDate: null,
      },
      isUpdated: false, //Trạng thái cần hay không việc cập nhật combobox
      isShowCreateForm: false, //Trạng thái hiển thị form tạo mới
      countries: [], //danh sách quốc gia
      provinces: [], //Danh sách tỉnh thành
      districts: [], //Danh sách quận huyện
      wards: [], //Danh sách phường xã
      //Các icon
      icon: {
        close: {
          //Nút đóng
          style: {
            //css
            background:
              "url(" +
              require("../../../public/ImagesIcons/common-icon.png") +
              ") no-repeat -1127px -127px",
          },
        },
        tips: {
          //Nứt trợ giúp
          style: {
            background:
              "url(" +
              require("../../../public/ImagesIcons/help.png") +
              ") no-repeat 4px 1px",
          },
        },
        btnSave: {
          //Nút lưu
          style: {
            background:
              "url(" +
              require("../../../public/ImagesIcons/common-icon.png") +
              ") no-repeat -327px -127px",
          },
        },
        btnSaveAndAdd: {
          //Nút Lưu và thêm mới
          style: {
            background:
              "url(" +
              require("../../../public/ImagesIcons/common-icon.png") +
              ") no-repeat -26px -127px",
          },
        },
        btnCancel: {
          //Nút hủy
          style: {
            background:
              "url(" +
              require("../../../public/ImagesIcons/common-icon.png") +
              ") no-repeat -252px -127px",
          },
        },
      },
      styles: {
        "border-color": "#8f8f8f",
      },
      errorIcon: {
        background:
          "url(" +
          require("../../../public/ImagesIcons/exclamation.png") +
          ") no-repeat",
      },
    };
  },
};
</script>
