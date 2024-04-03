<template>
  <div>
    <Header>
      <template #router-button>
        <div>
          <router-link active-class="active" class="green-btn" to="/sales/Form"
            >Adicionar</router-link
          >
        </div>
      </template>
    </Header>
    <div class="table-container">
      <div>
        <div class="head-line-container">
          <h2 class="head-line">Lista de Vendas</h2>
          <div>
            <i id="search-icon" class="bi bi-search" @click="fetchSales()"></i>
            <InputText
              v-model="search"
              :inputform="{ searchInput: true }"
              :placeholder="InputText"
              @keyup.enter="fetchSales()"
            />
          </div>
        </div>
      </div>
      <table class="table custom-table">
        <thead>
          <tr class="table-header">
            <th scope="col col-1">Cliente</th>
            <th scope="col col-2">Qtd. itens</th>
            <th scope="col col-3">Data da venda</th>
            <th scope="col col-4">Data faturamento</th>
            <th scope="col col-5">Valor total</th>
            <th scope="col col-6">Ações</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="sales in sale" :key="sales.id" class="table-row">
            <td class="col col-1" data-label="Cliente">
              {{ sales.customer.name }}
            </td>
            <td class="col col-2" data-label="Qtd. itens">1</td>
            <td class="col col-3" data-label="Data da venda">
              {{ formatData(sales.saleDate) }}
            </td>
            <td class="col col-4" data-label="Data faturamento">
              {{ formatData(sales.billingDate) }}
            </td>
            <td class="col col-5" data-label="Valor total">
              {{ formatMoney(sales.totalAmount) }}
            </td>

            <td class="tableButton">
              <button
                class="tableDeleteButton"
                @click="showDeleteModal(sales.id)"
              >
                Deletar
              </button>
              <router-link
                class="tableEditButton"
                :to="{
                  name: 'edit-sales-Form',
                }"
                @click="eidtSale(sales)"
                >Editar</router-link
              >
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <pagination
      :total-pages="totalPages"
      @value-updated="updateNumber"
    ></pagination>
  </div>
  <modal :show="showModal" @close="showModal = false">
    <template #body>
      <h1>{{ modalMessage }}</h1>
      <div v-if="showModalBtn" class="buttonContainer">
        <div>
          <button class="modalLeftButton" @click="deleteItem()">
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
</template>

<script>
import modal from "@/components/modal.vue";
import Header from "@/layouts/Header.vue";
import InputText from "@/components/InputText.vue";
import saleService from "@/common/api/service/sale.service";
import pagination from "@/components/pagination.vue";

export default {
  components: {
    Header,
    InputText,
    modal,
    pagination,
  },
  emits: ["eidtSale"],
  data() {
    return {
      InputText: "Buscar vendas..",
      showModal: false,
      modalMessageBtn: false,
      modalMessage: "Você deseja deletar?",
      showModalBtn: true,
      search: "",
      pageQuantity: 4,
      pageNumber: 1,
      totalPages: 1,
      sale: [],
      searchList: [],
    };
  },
  mounted() {
    this.fetchSales();
  },
  methods: {
    updateNumber(number) {
      this.pageNumber = number;
      this.fetchSales();
    },
    formatMoney(valor) {
      return valor.toLocaleString("pt-BR", {
        style: "currency",
        currency: "BRL",
      });
    },
    formatData(data) {
      return new Date(data).toLocaleDateString("pt-BR");
    },

    fetchSales() {
      const params = {
        pageNumber: this.pageNumber,
        pageQuantity: this.pageQuantity,
        searchTerm: this.search,
      };
      saleService
        .getPaginatedData(params)
        .then((response) => {
          this.sale = response.data;
          this.totalPages = response.pagination.totalPages;
        })
        .catch((error) => console.error("Erro ao buscar vendas", error));
    },
    resetModalAttributes() {
      this.showModalBtn = true;
      this.modalMessageBtn = false;
      this.modalMessage = "Você deseja deletar?";
    },
    showDeleteModal(saleId) {
      this.saleIdToDelete = saleId;
      this.resetModalAttributes();
      this.showModal = true;
    },
    deletedSaleMessage() {
      this.showModalBtn = false;
      this.modalMessageBtn = true;
      this.modalMessage = "Cliente deletado";
    },
    deleteItem() {
      const index = this.sale.findIndex(
        (sales) => sales.id === this.saleIdToDelete
      );
      if (index !== -1) {
        this.sale.splice(index, 1);
        saleService
          .delete(this.saleIdToDelete)
          .then(() => {
            console.error("venda deletada");
          })
          .catch(() => {
            console.error("error");
          });
      }
      this.deletedSaleMessage();
      setTimeout(() => {
        this.showModal = false;
      }, "1000");
    },
    eidtSale(sales) {
      this.$router.push({
        name: "edit-sales-Form",
        params: {
          id: sales.id,
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
  margin-top: 10px;
  margin-left: 235px;
  color: rgba(229, 235, 241, 1);
  cursor: pointer;
}
@media screen and (max-width: 844px) {
  .head-line-container {
    display: block;
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
}
</style>
