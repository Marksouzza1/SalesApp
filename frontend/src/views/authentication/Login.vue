<template>
  <div id="login-container">
    <div id="left-box">
      <div id="content">
        <div id="logo"><img id="logo" :src="LyncasLogo" alt="logo" /></div>
        <h2>Bem-vindo ao Lyncas Sales, uma aplicação</h2>
        <h2>simples para gerenciar vendas e clientes.</h2>
      </div>
    </div>
    <div id="rigth-box">
      <form id="login-box" autocomplete="off" @submit.prevent="onSubmit">
        <h2>Entrar</h2>
        <div id="login-input">
          <div>
            <InputText
              ref="loginEmail"
              v-model="login.email"
              :placeholder="placeholderEmail"
              type="email"
              :is-email="true"
            />
          </div>
          <div v-if="emailError" class="text-danger">
            <p>Email invalido</p>
          </div>
          <InputText
            ref="loginPassword"
            v-model="login.password"
            type="password"
            :placeholder="placeholderSenha"
          />
        </div>
        <div id="login-button">
          <button class="green-btn" type="submit">Entrar</button>
        </div>
        <div v-if="successMessage">validação bem sucessida</div>
        <div v-else></div>
      </form>
    </div>
  </div>
</template>
<script>
import InputText from "@/components/InputText.vue";
import router from "@/router";
export default {
  components: { InputText },
  data() {
    return {
      LyncasLogo: require("@/assets/img/Login-logo.png"),
      placeholderEmail: "Email",
      placeholderSenha: "Senha",
      classInput: { "input-form": true, "input-form-error": false },
      successMessage: false,
      login: {
        email: "",
        password: "",
      },
      showError: false,
      emailError: false,
    };
  },
  methods: {
    onSubmit() {
      const isLoginValid = this.$refs.loginEmail.validateModelValue();
      const isPasswordValid = this.$refs.loginPassword.validateModelValue();
      if (isLoginValid && isPasswordValid) {
        router.push("/dashboard");
      }
    },
  },
};
</script>
<style scoped>
#login-container {
  display: flex;
  width: 100%;
}
#left-box {
  width: 40%;
  height: 100vh;
  background: rgba(39, 74, 157, 1);
  color: white;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 16px;
}
#logo {
  padding: 20px;
}
#content {
  padding-bottom: 40%;
}
#logo img {
  width: 244px;
  height: 56px;
}
#rigth-box {
  width: 60%;
  display: flex;
  align-items: center;
  justify-content: center;
}
#login-box {
  font-size: 30px;
  width: 40%;
  padding-bottom: 24%;
}
#login-input {
  display: flex;
  flex-direction: column;
  padding: 10px;
}
#login-button {
  text-align: right;
}
@media screen and (max-width: 767px) {
  #login-container {
    flex-direction: column;
  }
  #left-box {
    width: 100%;
    height: 50vh;
  }
  #logo {
    padding: 5px;
  }
  #content {
    padding-bottom: 10%;
  }
  #logo img {
    width: 244px;
    height: 56px;
  }
  #rigth-box {
    width: 100%;
  }
  #login-box {
    width: 80%;
  }
}
</style>
