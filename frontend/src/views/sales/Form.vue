<template>
  <div>
    <Header>
      <template #router-button>
        <div>
          <router-link to="/sales" class="back-btn">Voltar</router-link>
        </div>
      </template>
    </Header>

    <form id="container-form">
      <div class="head-line">
        <h1>{{ headerH1 }}</h1>
      </div>
      <div class="grid">
        <div class="col">
          <label class="label-input">Clientes:</label>
          <SelectForm
            :fetch-data="fetchCustomers"
            :customer-id="customerId"
            :selected-option="selectedOption"
            @customer-selected="handleCustomerSelected"
          />
        </div>
        <div class="col">
          <InputText
            id="invoiceDate"
            ref="inputDate"
            v-model="sale.invoiceDate"
            type="date"
            label-name="Data de faturamento"
            :disabled="isDisable"
            placeholder="Data de faturamento"
          />
        </div>
      </div>

      <div class="line"></div>
      <div class="head-line">
        <h2>Itens do pedido</h2>
      </div>
      <div class="grid">
        <div class="col">
          <InputText
            id="description"
            ref="inputDescription"
            v-model="sale.description"
            label-name="Descrição do item"
          />
        </div>
        <div class="col">
          <InputMoney
            ref="inputUnitaryValue"
            v-model="sale.unitaryValue"
            label-name="Valor unitário"
          />
        </div>
      </div>
      <div class="grid">
        <div class="col">
          <InputText
            ref="inputSaleRate"
            v-model="sale.saleRate"
            label-name="Quantidade"
            type="number"
            min="1"
          />
        </div>
        <div class="col">
          <InputMoney
            ref="totalValue"
            v-model="sale.totalValue"
            label-name="Valor total"
            disabled
          />
        </div>
      </div>
      <div class="grid-item add-items">
        <button id="add-items" @click.prevent="addItem">Adicionar Itens</button>
      </div>

      <div v-if="itens.length > 0">
        <h2 class="h2-items">Itens adicionados</h2>
        <table class="table custom-table">
          <thead>
            <tr class="card">
              <th scope="col">Descrição do item</th>
              <th scope="col">Valor unitário</th>
              <th scope="col">Quantidade de itens</th>
              <th scope="col">Valor total</th>
              <th scope="col">Ações</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(item, index) in itens" :key="index" class="card">
              <td>{{ item.description }}</td>
              <td>{{ item.unitaryValue }}</td>
              <td>{{ item.saleRate }}</td>
              <td>{{ item.totalValue }}</td>
              <td class="tableButton">
                <button
                  class="tableDeleteButton"
                  type="button"
                  @click="removeItem(index)"
                >
                  Remover
                </button>
                <button
                  class="tableEditButton"
                  type="button"
                  @click="editItem(index)"
                >
                  Editar
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <div class="result-container">
        <h2 id="result">Total: {{ formattedTotal }}</h2>
        <button class="green-btn" type="submit" @click.prevent="validate">
          Salvar
        </button>
      </div>

      <modal :show="showModal" :has-error="error" @close="showModal = false">
        <template #body>{{ modalMessage }}</template>
      </modal>
    </form>
  </div>
</template>

<script>
import { format, unformat } from "v-money3";
import modal from "@/components/modal.vue";
import { mask } from "vue-the-mask";
import Header from "@/layouts/Header.vue";
import InputText from "@/components/InputText.vue";
import InputMoney from "@/components/InputMoney.vue";
import SelectForm from "@/components/SelectForm.vue";
import router from "@/router";
import saleService from "@/common/api/service/sale.service";
import customerService from "@/common/api/service/customer.service";
export default {
  name: "SalesForm",
  directives: { mask },
  components: {
    Header,
    InputText,
    SelectForm,
    InputMoney,
    modal,
  },
  data() {
    return {
      editing: false,
      selectedOption: null,
      customerId: null,
      headerH1: "Adicionar Venda",
      modalMessage: "",
      showModal: false,
      isDisable: false,
      error: false,
      totalAmount: 0,
      sale: { invoiceDate: "", unitaryValue: "", saleRate: "", totalValue: "" },
      itens: [],
      config: {
        decimal: ",",
        thousands: ".",
        prefix: "R$ ",
        suffix: "",
        precision: 2,
        masked: false,
      },
    };
  },
  computed: {
    formattedTotal() {
      return format(this.calcularTotal(), this.config);
    },
  },
  watch: {
    "sale.saleRate": function (newRate) {
      this.updateTotalValue(newRate);
    },
  },
  mounted() {
    if (this.$route.params.id) {
      this.fetchSaleById(this.$route.params.id);
      this.fetchCustomers();
      this.editMode();
      this.saleId = this.$route.params.id;
    }
  },
  methods: {
    editItem(index) {
      this.sale.description = this.itens[index].description;
      this.sale.unitaryValue = this.itens[index].unitaryValue;
      this.sale.saleRate = this.itens[index].saleRate;
      this.sale.totalValue = this.itens[index].totalValue;

      this.itens.splice(index, 1);
    },
    handleCustomerSelected(customerId) {
      this.customerId = customerId;
    },
    validate() {
      const isValidDate = this.$refs.inputDate.validateModelValue();

      if (this.itens.length === 0) {
        this.showValidationError("Lista de itens vazia");
        return;
      }

      if (!isValidDate) {
        this.showValidationError("Data de faturamento inválida");
        return;
      }

      if (!this.customerId) {
        this.showValidationError("Por favor, escolha um cliente");
        return;
      }
      const currentDate = new Date().toLocaleString("pt-BR");
      const saleData = {
        customerId: this.customerId,
        billingDate: this.sale.invoiceDate,
        saleDate: currentDate,
        totalAmount: this.totalAmount,
        items: this.itens.map((item) => ({
          description: item.description,
          unitPrice: unformat(item.unitaryValue, this.config),
          quantity: item.saleRate,
        })),
      };
      if (this.editing == false) {
        this.saveSale(saleData);
      } else {
        this.updateSale(saleData);
      }
    },
    saveSale(saleData) {
      saleService
        .post(saleData)
        .then(() => {
          this.showValidationSuccess("Venda criada com sucesso");
          setTimeout(this.routerRedirect, 1000);
        })
        .catch((error) => {
          console.error("Erro ao criar venda:", error);
          this.showValidationError("Erro ao criar venda");
        });
    },
    updateSale(saleData) {
      saleService
        .update(this.saleId, saleData)
        .then((response) => {
          this.sale = response.data;
          response.data;
          this.showValidationSuccess("Venda Atualizada com sucesso");
          setTimeout(this.routerRedirect, 1000);
        })
        .catch((error) => {
          console.error("Erro ao atualizar", error);
        });
    },
    async fetchSaleById(saleId) {
      try {
        const response = await saleService.getById(saleId);
        const saleData = response.data;
        this.sale.invoiceDate = new Date(saleData.billingDate)
          .toISOString()
          .split("T")[0];
        this.itens = saleData.items.map((item) => ({
          description: item.description,
          unitaryValue: format(item.unitPrice, this.config),
          saleRate: item.quantity,
          totalValue: format(item.unitPrice * item.quantity, this.config),
        }));
        this.customerId = saleData.customerId;
        const customerResponse = await customerService.getById(this.customerId);
        const customerData = customerResponse.data;
        this.selectedOption = customerData.customerId;

        this.editing = true;
      } catch (error) {
        console.error("Erro ao buscar venda:", error);
      }
    },
    routerRedirect() {
      router.push("/sales");
    },
    addItem() {
      let hasErrors = false;

      if (!this.sale.description) {
        this.$refs.inputDescription.validateModelValue();
        hasErrors = true;
      }

      if (!this.sale.saleRate) {
        this.$refs.inputSaleRate.validateModelValue();
        hasErrors = true;
      }

      if (!this.sale.unitaryValue || this.sale.unitaryValue === "R$ 0,0") {
        this.$refs.inputUnitaryValue.validateMoney();
        hasErrors = true;
      }

      if (hasErrors) {
        return;
      }
      const unitaryValue = unformat(this.sale.unitaryValue);
      const saleRate = parseFloat(this.sale.saleRate);
      const totalValue = unitaryValue * saleRate;

      const newItem = {
        description: this.sale.description,
        unitaryValue: format(unitaryValue, this.config),
        saleRate: saleRate,
        totalValue: format(totalValue, this.config),
      };
      this.itens.push(newItem);

      this.clearSaleInputs();
      this.showModal = true;
      this.showValidationSuccess("item adicionado com sucesso");
    },
    updateTotalValue(newRate) {
      const unitaryValue = this.sale.unitaryValue
        ? unformat(this.sale.unitaryValue)
        : 0;

      const saleRate = parseFloat(newRate);
      const totalValue = unitaryValue * saleRate;

      this.sale.totalValue = format(totalValue, this.config);
    },
    fetchCustomers() {
      return customerService
        .getAll()
        .then((response) =>
          response.data.map((customer) => ({
            value: customer.customerId,
            label: customer.name,
          }))
        )
        .catch((error) => {
          console.error("Erro ao buscar clientes;", error);
          return [];
        });
    },

    clearSaleInputs() {
      this.sale.description = "";
      this.sale.unitaryValue = "";
      this.sale.saleRate = "";
      this.sale.totalValue = "";
    },
    calcularTotal() {
      this.totalAmount = this.itens.reduce((total, item) => {
        const unformattedTotal = unformat(item.totalValue, this.config);
        return total + parseFloat(unformattedTotal.replace(",", "."));
      }, 0);
      return this.totalAmount;
    },

    removeItem(index) {
      this.itens.splice(index, 1);
    },
    showValidationError(message) {
      this.modalMessage = message;
      this.showModal = true;
      this.error = true;
    },
    showValidationSuccess(message) {
      this.modalMessage = message;
      this.showModal = true;
      this.error = false;
    },
    editMode() {
      this.editMode = true;
      this.headerH1 = "Editar Venda";
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
  margin: 20px;
  padding: 0 40px;
}
.grid {
  display: flex;
  margin-left: -15px;
  margin-right: -5px;
  flex: 1;
}
.line {
  border-bottom: 2px dotted rgba(229, 235, 241, 1);
  margin-top: 20px;
  width: 100%;
}
.result-container {
  display: flex;
  justify-content: space-between;
  margin-top: 10px;
}
.result-container button {
  width: 78px;
  height: 45px;
}
.grid-item {
  text-align: left;
}
.add-items {
  text-align: right;
}
#result {
  text-align: left;
}
#add-items {
  margin-top: 20px;
  font-weight: bold;
  background-color: rgba(255, 255, 255, 1);
  border-radius: 5px;
  border: 2px solid rgba(39, 74, 157, 1);
  color: rgba(39, 74, 157, 1);
  cursor: pointer;
  width: 160px;
  height: 33px;
  transition: 0.5s;
}
#add-items:hover {
  background: rgba(39, 74, 157, 1);
  color: white;
}
.add-sales,
.order-items,
.add-items,
.total {
  grid-column: 1 / span 2;
}
.h2-items {
  text-align: left;
}
@media screen and (max-width: 720px) {
  .grid {
    display: flex;
    flex-direction: column;
  }
}
</style>
