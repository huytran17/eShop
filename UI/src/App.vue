<template>
  <div style="position: relative">
    <v-app id="app">
      <div id="nav">
        <Sidebar />
      </div>
      <div id="main">
        <Header :stores="stores" v-if="stores.length > 0" />
        <Content :apiUrl="apiUrl" />
      </div>
    </v-app>
    <div>
      <CreateStoreForm :apiUrl="apiUrl" />
    </div>
    <Overlay />
    <DelAlert :apiUrl="apiUrl" />
    <AlertStoreAlreadyExists />
    <Loading />
  </div>
</template>

<style scoped>
@import "./assets/scss/app/style.css";
</style>

<script>
import Header from "./components/Layout/TheHeader";
import Sidebar from "./components/Layout/TheSidebar";
import Content from "./components/Layout/TheContent";
import CreateStoreForm from "./components/Store/Create";
import Overlay from "./components/Common/MISA.Overlay";
import DelAlert from "./components/Common/MISA.AlertDeleteStore.vue";
import AlertStoreAlreadyExists from "./components/Common/MISA.AlertStoreAlreadyExists.vue";
import Loading from "./components/Common/MISA.Loading.vue";
import axios from "axios";
export default {
  //Lấy danh sách cửa hàng
  async created() {
    try {
      var res = await axios.get(`${this.apiUrl}/Stores`);
      this.stores = res.data;
    } catch (error) {
      console.log(error);
    }
  },
  mounted() {
    // Add a request interceptor
    axios.interceptors.request.use(
      function (config) {
        // Do something before request is sent
        document.getElementById('loading').classList.add('d-block');
        return config;
      },
      function (error) {
        // Do something with request error
        return Promise.reject(error);
      }
    );

    // Add a response interceptor
    axios.interceptors.response.use(
      function (response) {
        // Any status code that lie within the range of 2xx cause this function to trigger
        // Do something with response data
        document.getElementById('loading').classList.remove('d-block');
        document.getElementById('loading').classList.add('hide');
        return response;
      },
      function (error) {
        // Any status codes that falls outside the range of 2xx cause this function to trigger
        // Do something with response error
        return Promise.reject(error);
      }
    );
  },
  name: "App",
//Khai báo các component
  components: {
    Header,
    Sidebar,
    CreateStoreForm,
    Content,
    Overlay,
    DelAlert,
    AlertStoreAlreadyExists,
    Loading,
  },

  data() {
    return {
      apiUrl: "https://localhost:5001/api/v1", //Đường dẫn API
      stores: [], //Danh sách cửa hàng
    };
  },
};
</script>
