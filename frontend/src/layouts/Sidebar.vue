<template>
  <div class="sidebar">
    <img id="logo" :src="LyncasLogo" alt="logo" />

    <div class="menu">
      <div>
        <router-link to="/dashboard" active-class="active" class="btn">
          <i class="bi bi-grid"></i>
          <span>Dashboard</span>
        </router-link>
      </div>

      <div>
        <router-link to="/customers" active-class="active" class="btn">
          <i class="bi bi-people"></i>
          <span>Clientes</span>
        </router-link>
      </div>

      <div>
        <router-link to="/sales" active-class="active" class="btn">
          <i class="bi bi-cart2"></i>
          <span>Vendas</span>
        </router-link>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "Sidebar",
  emits: ["handleDashboardClick", true],
  data() {
    return {
      LyncasLogo: require("@/assets/img/logo-lyncas.png"),
    };
  },
};
</script>
<style scoped>
#logo {
  margin-top: 30%;
  width: 106px;
  height: 24px;
}
.sidebar {
  background-color: #1c3b85;
  height: 100vh;
  width: 168px;
  position: fixed;
  top: 0;
  left: 0;
}
.menu {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 150px;
  padding: 10px;
}
.menu div {
  margin-top: 20px;
  width: 126px;
}
.btn {
  text-decoration: none;
  color: #fff;
  border-radius: 4px;
  background-color: #1c3b85;
  border: solid 1px rgba(6, 53, 124, 0.884);
  width: 100%;
  height: 36px;
  display: flex;
  align-items: center;
}

.btn span {
  margin-left: 5px;
}

.btn i {
  vertical-align: middle;
  margin-left: 10px;
}
.btn.active {
  background: rgba(6, 53, 124, 0.884);
}

.btn:hover {
  background: rgba(6, 53, 124, 0.884);
}

@media screen and (max-width: 844px) {
  .sidebar {
    background-color: #1c3b85;
    width: 100%;
    height: 50px;
    position: fixed;
    bottom: 0;
    left: 0;
    top: auto;
  }
  .menu {
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: center;
    position: fixed;
    bottom: 0;
    left: 0;
  }
  .menu div {
    margin-top: 20px;
    width: 126px;
  }
  .btn {
    text-decoration: none;
    border: none;
    width: 100%;
    height: 36px;
    display: flex;
    flex-direction: column;
    align-items: center;
  }
  .btn.active {
    background-color: #1c3b85;
    border: none;
    color: rgba(255, 255, 255, 1);
  }
  .fixed-sidebar {
    position: fixed;
    bottom: 0;
    width: 100%;
  }
}
</style>
