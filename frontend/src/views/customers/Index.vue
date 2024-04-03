<template>
  <div>
    <Header>
      <template #router-button>
        <div>
          <router-link
            active-class="active"
            class="green-btn"
            to="/customers/Form"
          >
            Adicionar
          </router-link>
        </div>
      </template>
    </Header>
    <div class="table-container">
      <div>
        <div class="head-line-container">
          <h2 class="head-line">Lista de clientes</h2>
          <div>
            <i
              id="search-icon"
              class="bi bi-search"
              @click="fetchCustomers"
            ></i>
            <InputText
              v-model="search"
              :inputform="{ searchInput: true }"
              :placeholder="InputText"
              @keyup.enter="fetchCustomers()"
            />
          </div>
        </div>
      </div>
      <table class="table custom-table">
        <thead>
          <tr class="table-header">
            <th scope="col col-1">Cliente</th>
            <th scope="col col-2">Email</th>
            <th scope="col col-3">Telefone</th>
            <th scope="col col-4">Cpf</th>
            <th scope="col col-5">Ações</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="customer in customers"
            :key="customer.customerId"
            class="table-row"
          >
            <td class="col col-1" data-label="Cliente">
              {{ customer.name }}
            </td>
            <td class="col col-2" data-label="Email">{{ customer.email }}</td>
            <td class="col col-3" data-label="Telefone">
              {{ customer.phoneNumber }}
            </td>
            <td class="col col-4" data-label="Cpf">{{ customer.cpf }}</td>
            <td class="tableButton">
              <button
                class="tableDeleteButton"
                @click="showDeleteModal(customer.customerId)"
              >
                Deletar
              </button>
              <RouterLink
                class="tableEditButton"
                :to="{
                  name: 'edit-customer-Form',
                }"
                @click="editCustomer(customer)"
              >
                Editar
              </RouterLink>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <modal :show="showModal" @close="showModal = false">
      <template #body>
        <h1>{{ modalMessage }}</h1>
        <div v-if="showModalBtn" class="buttonContainer">
          <div>
            <button class="modalLeftButton" @click="deleteCustomer()">
              confirmar
            </button>
          </div>
          <div>
            <button class="modalRigthtButton" @click="showModal = false">
              cancelar
            </button>
          </div>
        </div>
        <div v-if="modalMessageBtn" class="modalMessage">
          <button class="modalLeftButton" @click="showModal = false">OK</button>
        </div>
      </template>
    </modal>

    <Pagination :total-pages="totalPages" @value-updated="changePage" />
  </div>
</template>

<script>
import Pagination from "@/components/pagination.vue";
import modal from "@/components/modal.vue";
import Header from "@/layouts/Header.vue";
import InputText from "@/components/InputText.vue";
import customerService from "@/common/api/service/customer.service";

export default {
  components: {
    Header,
    InputText,
    modal,
    Pagination,
  },
  emits: ["edit-customer"],
  data() {
    return {
      search: "",
      InputText: "Buscar vendas..",
      showModal: false,
      showModalBtn: true,
      modalMessageBtn: false,
      modalMessage: "Você deseja deletar?",
      pageSize: 4,
      pageNumber: 1,
      totalPages: 1,
      customers: [],
    };
  },
  mounted() {
    this.fetchCustomers();
  },
  methods: {
    deletedCustomerMessage() {
      this.showModalBtn = false;
      this.modalMessageBtn = true;
      this.modalMessage = "Cliente deletado";
    },
    resetModalAttributes() {
      this.showModalBtn = true;
      this.modalMessageBtn = false;
      this.modalMessage = "Você deseja deletar?";
    },
    showDeleteModal(customerId) {
      this.customerIdToDelete = customerId;
      this.resetModalAttributes();
      this.showModal = true;
    },
    fetchCustomers() {
      const params = {
        pageNumber: this.pageNumber,
        pageQuantity: this.pageSize,
        searchTerm: this.search,
      };

      customerService
        .getPaginatedData(params)
        .then((response) => {
          this.customers = response.data;
          this.totalPages = response.pagination.totalPages;
          console.log("pagination", this.customers);
        })
        .catch((error) => console.error("Erro ao buscar clientes;", error));
    },
    changePage(page) {
      this.pageNumber = page;
      this.fetchCustomers();
    },
    deleteCustomer() {
      const index = this.customers.findIndex(
        (customer) => customer.customerId === this.customerIdToDelete
      );
      if (index !== -1) {
        this.customers.splice(index, 1);
        customerService
          .delete(this.customerIdToDelete)
          .then(() => {
            console.error("cliente deletado");
          })
          .catch(() => {
            console.error("error");
          });
      }
      this.deletedCustomerMessage();
      setTimeout(() => {
        this.showModal = false;
      }, "1000");
    },

    editCustomer(customer) {
      this.$router.push({
        name: "edit-customer-Form",
        params: {
          id: customer.customerId,
        },
      });
    },
  },
};
</script>

<style scoped>
#head-line-container {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
#search-icon {
  position: absolute;
  cursor: pointer;
  margin-top: 10px;
  margin-left: 235px;
  color: rgba(229, 235, 241, 1);
}
@media screen and (max-width: 844px) {
  .head-line-container {
    display: block;
  }
  .searchInput {
    width: 30px;
  }
  .table-row {
    margin-bottom: 10px;
    background-color: #fff;
    display: grid;
  }
  td.col.col-2,
  td.col.col-3,
  .tableButton,
  .table-header {
    display: none;
  }
  .col {
    width: 100%;
    height: 30px;
  }
  .col.col-1 {
    grid-area: 1;
  }
  .col.col-4 {
    grid-area: 2;
    color: rgba(191, 191, 191, 1);
  }
  .col.col-5 {
    grid-area: col-5;
    color: rgba(3, 200, 116, 1);
  }
  .highlight {
    background-color: yellow;
  }
}
</style>
