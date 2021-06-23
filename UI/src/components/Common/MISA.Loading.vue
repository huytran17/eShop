<template>
  <div id="loading" ref="loading" class="hide">
    <div class="wapper">
      <v-progress-circular
        :size="30"
        :width="1.5"
        indeterminate
        color="#00577b"
      ></v-progress-circular>
    </div>
  </div>
</template>

<style scoped>
@import "../../assets/scss/customize/loading/style.css";
</style>