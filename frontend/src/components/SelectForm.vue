<template>
  <div>
    <select
      :class="[
        {
          inputForm,
          inputError,
        },
      ]"
      @change="handleSelectChange($event.target.value)"
    >
      <option selected="true" disabled="disabled">Selecione Um Cliente</option>
      <option
        v-for="option in options"
        :key="option.value"
        :value="option.value"
        :selected="option.value === selectedOption"
      >
        {{ option.label }}
      </option>
    </select>
    <p v-if="isSelectEmpty" class="textDanger">
      Por favor, escolha um cliente.
    </p>
  </div>
</template>

<script>
export default {
  name: "SelectForm",
  props: {
    selectedOption: {
      type: String,
      default: null,
    },
    fetchData: {
      type: Function,
      required: true,
    },
    customerId: {
      type: String,
      default: null,
    },
  },
  emits: ["customer-selected"],
  data() {
    return {
      isSelectEmpty: false,
      inputError: false,
      inputForm: true,
      options: [],
    };
  },
  mounted() {
    this.fetchData()
      .then((options) => {
        this.options = options;
        this.validateSelect();
      })
      .catch((error) => console.log("Erro ao buscar dados;", error));
  },
  methods: {
    handleSelectChange(value) {
      this.$emit("customer-selected", value);
    },
    validateSelect() {
      if (this.selectedOption && this.options.length > 0) {
        this.$emit("customer-selected", this.selectedOption);
      }
    },
  },
};
</script>
