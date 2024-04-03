<template>
  <div>
    <Header>
      <template #router-button>
        <router-link to="/customers" class="back-btn">Voltar</router-link>
      </template>
    </Header>

    <form id="container-form" @submit.prevent="onSubmit">
      <div class="head-line">
        <h2>{{ header }}</h2>
      </div>

      <div class="grid">
        <div class="col">
          <InputText
            ref="inputName"
            v-model="customer.name"
            label-name="Nome"
            type="text"
          />
        </div>

        <div class="col">
          <InputText
            ref="inputEmail"
            v-model="customer.email"
            label-name="Email"
            type="text"
            :is-email="true"
          />
        </div>

        <div class="col">
          <InputText
            ref="inputCpf"
            v-model="customer.cpf"
            v-mask="'###.###.###-##'"
            label-name="CPF"
            type="text"
            :is-c-p-f="true"
          />
        </div>

        <div class="col">
          <InputText
            ref="inputTel"
            v-model="customer.phoneNumber"
            v-mask="'(##) #####-####'"
            label-name="Telefone"
            type="tel"
            :is-tel="true"
          />
        </div>
      </div>
      <div id="save-btn" class="text-right">
        <button v-if="!editing" ref="saveBtn" class="green-btn" type="submit">
          Salvar
        </button>
        <button v-if="editing" ref="saveBtn" class="back-btn" type="submit">
          Salvar
        </button>
      </div>
      <Teleport to="body">
        <modal :show="showModal" :has-error="error" @close="showModal = false">
          <template #body>
            <p>dados salvos com sucesso</p>
          </template>
        </modal>
      </Teleport>
    </form>
  </div>
</template>

<script>
import modal from "@/components/modal.vue";
import InputText from "@/components/InputText.vue";
import Header from "@/layouts/Header.vue";
import { mask } from "vue-the-mask";
import router from "@/router";
import CustomerService from "@/common/api/service/customer.service";
export default {
  directives: { mask },
  components: {
    Header,
    InputText,
    modal,
  },

  data() {
    return {
      header: "Adicionar Cliente",
      editing: false,
      error: false,
      showModal: false,
      customer: {
        name: "",
        email: "",
        phoneNumber: "",
        cpf: "",
      },
    };
  },
  mounted() {
    if (this.$route.params.id) {
      this.fetchCustomerById(this.$route.params.id);
      this.EditMode();
      this.customerId = this.$route.params.id;
    }
  },
  methods: {
    onSubmit(event) {
      const isValidEmail = this.$refs.inputEmail.validateModelValue();
      const isValidName = this.$refs.inputName.validateModelValue();
      const isValidCpf = this.$refs.inputCpf.validateModelValue();
      const isValidiTel = this.$refs.inputTel.validateModelValue();
      event.preventDefault();

      if (isValidEmail && isValidName && isValidCpf && isValidiTel) {
        if (this.editing == false) {
          this.SaveCustomers(this.customer);
          console.log("dados validados");
          this.showModal = true;
          setTimeout(this.routerRedirect, 1 * 1000);
        } else {
          this.UpdateCustomers(this.customer.customerid, this.customer);
          this.showModal = true;
          setTimeout(this.routerRedirect, 1 * 1000);
        }
      }
    },
    routerRedirect() {
      router.push("/customers", this.customer);
    },
    save(customer) {
      if (!this.customer.customerId) {
        CustomerService.post(customer);
      } else {
        CustomerService.update(customer);
      }
    },
    SaveCustomers(customerData) {
      CustomerService.post(customerData)
        .then((response) => {
          this.customer = response.data;
        })
        .catch((error) => console.log("", error));
    },
    fetchCustomerById(customerId) {
      CustomerService.getById(customerId)
        .then((response) => {
          this.customer = response.data;
        })
        .catch((error) => {
          console.error("Erro ao buscar cliente:", error);
        });
    },
    UpdateCustomers() {
      CustomerService.update(this.customerId, this.customer)
        .then((response) => {
          this.customer = response.data;
          response.data;
        })
        .catch((error) => {
          console.log("Erro ao atualizar", error);
        });
    },
    EditMode() {
      this.editing = true;
      this.header = "Editar cliente";
    },
  },
};
</script>

<style scoped>
#container-form {
  justify-content: center;
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 5px;
  padding: 20px;
  margin: 20px;
  flex: 1;
}

.grid {
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 10px;
  margin-right: -5px;
}
#save-btn {
  margin-top: 10px;
  width: 100%;
  text-align: right;
}

@media screen and (max-width: 844px) {
  .grid {
    display: flex;
    flex-direction: column;
  }
  #container-form {
    justify-content: flex-start;
    margin-bottom: 0;
  }
}
</style>
