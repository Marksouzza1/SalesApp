<template>
  <div class="header-container">
    <div class="user-info">
      <img :src="avatarIcon" alt="avatar" class="avatar" />
      <div class="user">
        <h1 class="user-name">Olá, Fulano</h1>
        <div>
          <h2 class="logout" @click="redirect">Sair</h2>
        </div>
      </div>
    </div>
    <div class="btn-container">
      <slot name="router-button"> </slot>
    </div>
  </div>
</template>
<script>
import router from "@/router";

export default {
  name: "Header",
  props: {
    route: {
      type: String,
      default: "/",
    },
  },
  data() {
    return {
      avatarIcon: require("@/assets/img/avatar.png"),
    };
  },
  methods: {
    redirect() {
      router.push("/login");
    },
  },
};
</script>

<style scoped>
.header-container {
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-bottom: 2px solid rgba(229, 235, 241, 1);
  margin: 20px;
}
.user-name {
  font-size: 18px;
  margin-bottom: 0;
}
.user-info {
  display: flex;
  align-items: center;
}
.logout {
  text-align: left;
  font-size: 14px;
  color: gray;
  cursor: pointer;
}
.avatar {
  margin-right: 15px;
  width: 50px;
  height: 50px;
}
@media screen and (max-width: 844px) {
  .header-container {
    padding: 20px;
    margin: 0;
  }
}
</style>
