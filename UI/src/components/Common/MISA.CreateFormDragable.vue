<template>
  <div ref="draggableContainer" id="draggable-container">
    <div id="draggable-header" @mousedown="dragMouseDown">
      <slot name="header"></slot>
    </div>
    <slot name="main"></slot>
    <slot name="footer"></slot>
  </div>
</template>

<style>
@import "../../assets/scss/customize/createformdragable/style.css";
</style>

<script>
export default {
  name: "DraggableDiv",
  data: function () {
    return {
      positions: {
        clientX: undefined, //vị trí theo phương ngang
        clientY: undefined, //vị trí theo phương dọc
        movementX: 0, //vị trí di chuyển chiều ngang
        movementY: 0, //vị trí di chuyển theo chiều dọc
      },
    };
  },
  methods: {
    /**
     * Sự kiện giữu và kéo chuột trên header của form
     * Created by: TVHUY (18-06-2021)
     */
    dragMouseDown: function (event) {
      event.preventDefault();
      //Lấy vị trí ban đầu của con trỏ chuôt
      this.positions.clientX = event.clientX; //Lấy vị trí chiều ngang hiện tại của chuột
      this.positions.clientY = event.clientY; //Lấy vị trí chiều dọc hiện tại của chuột
      document.onmousemove = this.elementDrag; //Sự kiện kéo chuột
      document.onmouseup = this.closeDragElement; //Sự kiện thả chuột
    },
    /**
     * Sự kiện kéo chuột khi nhấn giữ chuột trên form
     * Created by: TVHUY (18-06-2021)
     */
    elementDrag: function (event) {
      event.preventDefault();
      this.positions.movementX = this.positions.clientX - event.clientX; //Vị trí di chuyển phần tử theo chiều ngang
      this.positions.movementY = this.positions.clientY - event.clientY; //Vị trí di chuyển phần tử theo chiều dọc
      this.positions.clientX = event.clientX; //Vị trí phần tử theo chiều ngang
      this.positions.clientY = event.clientY; //Vị trí phần tử theo chiều ngang
      // set the element's new position:
      this.$refs.draggableContainer.style.top =
        this.$refs.draggableContainer.offsetTop -
        this.positions.movementY +
        "px";
      this.$refs.draggableContainer.style.left =
        this.$refs.draggableContainer.offsetLeft -
        this.positions.movementX +
        "px";
    },
    /**
     * Đặt lại sự kiên chuột
     * Bỏ sự kiện thả chuột
     * Bỏ sự kiện di chuột
     */
    closeDragElement() {
      document.onmouseup = null;
      document.onmousemove = null;
    },
  },
};
</script>
