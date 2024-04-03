<template>
  <Header />
  <div>
    <h1>Bem vindo !</h1>
  </div>
</template>

<script>
import Header from "@/layouts/Header.vue";
export default {
  name: "Dashboard",
  components: { Header },
};
</script>
<style scoped>
h1 {
  margin-top: 10%;
  text-align: center;
}
</style>
