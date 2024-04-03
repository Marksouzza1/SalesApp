<template>
  <div>
    <Sidebar />
    <div id="container">
      <router-view />
    </div>
  </div>
</template>

<script>
import Sidebar from "@/layouts/Sidebar.vue";

export default {
  name: "Layout",
  components: {
    Sidebar,
  },
  data() {
    return {
      headerRoute: "/dashboard",
      isMobile: false,
    };
  },
  mounted() {
    this.isMobile = window.innerWidth <= 844;
    window.addEventListener("resize", this.checkMobile);
  },
  methods: {
    changeHeaderRoute() {
      this.headerRoute = "/";
    },
    checkMobile() {
      this.isMobile = window.innerWidth <= 844;
    },
    beforeDestroy() {
      window.removeEventListener("resize", this.checkMobile);
    },
  },
};
</script>

<style>
#container {
  margin-left: 168px;
}
@media screen and (max-width: 844px) {
  #container {
    margin-left: 0;
  }
  .fixed-sidebar {
    position: fixed;
    bottom: 0;
    width: 100%;
    z-index: 1000;
  }
}
</style>
