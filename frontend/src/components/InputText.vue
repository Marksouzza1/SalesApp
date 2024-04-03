<template>
  <div>
    <label class="label-input">{{ labelName }}</label>
    <input
      :class="[
        inputform,
        {
          inputForm: true,
          searchInput: false,
          inputError,
        },
      ]"
      :type="type"
      :placeholder="placeholder"
      :value="modelValue"
      :data-maska="maska"
      :disabled="disabled"
      @input="sendInputValue"
    />
    <div v-if="!isInputValid" class="textDanger">
      {{ validationMessage }}
    </div>
    <i id="search-icon" class="iClassName"></i>
  </div>
</template>

<script>
import helpers from "@/common/Helper/helpers";

export default {
  name: "InputText",
  props: {
    labelName: {
      type: String,
      default: "",
    },
    placeholder: {
      type: String,
      default: "",
    },
    modelValue: {
      type: String,
      default: "",
    },
    type: {
      type: String,
      default: "",
    },
    maska: {
      type: String,
      default: "",
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    inputform: {
      type: Object,
      default: () => ({}),
    },
    isEmail: {
      type: Boolean,
      default: false,
    },
    isCPF: {
      type: Boolean,
      default: false,
    },
    isTel: {
      type: Boolean,
      default: false,
    },
    required: {
      type: Boolean,
      default: false,
    },
  },
  emits: ["focus", "maska", "update:modelValue", "blur", "input-error"],
  data() {
    return {
      validationMessage: "",
      inputError: false,
    };
  },
  computed: {
    isInputValid() {
      if (this.inputError == true) {
        this.validateModelValue();
      }
      return !this.inputError;
    },
  },
  methods: {
    sendInputValue(event) {
      this.$emit("update:modelValue", event.target.value);
    },
    validateModelValue() {
      if (this.modelValue.length === 0) {
        this.validationMessage = "Campo não pode ficar vazio";
        this.inputError = true;
      } else if (this.isEmail && !helpers.validateEmail(this.modelValue)) {
        this.validationMessage = "E-mail inválido";
        this.inputError = true;
      } else if (this.isCPF && !helpers.validateCPF(this.modelValue)) {
        this.inputError = true;
        this.validationMessage = "CPF inválido";
      } else if (this.isTel && !helpers.validateTel(this.modelValue)) {
        this.inputError = true;
        this.validationMessage = "Número inválido";
      } else {
        this.validationMessage = "";
        this.inputError = false;
      }
      return !this.inputError;
    },
    validate() {
      this.validateModelValue();
    },
  },
};
</script>
