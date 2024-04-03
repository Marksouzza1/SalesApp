<template>
  <div>
    <label class="label-input">{{ labelName }}</label>
    <input
      ref="moneyInput"
      v-money3="config"
      :value="modelValue"
      :class="[
        inputform,
        {
          inputForm: true,
          inputError,
        },
      ]"
      :disabled="disabled"
      @input="sendInputValue"
    />
  </div>
  <div v-if="validationMessage" class="textDanger">
    {{ validationMessage }}
  </div>
</template>

<script>
import { Money3Directive } from "v-money3";
export default {
  name: "InputMoney",
  components: {},
  directives: {
    money3: Money3Directive,
  },
  props: {
    modelValue: {
      type: [String, Number],
      required: true,
    },
    labelName: {
      type: String,
      default: "",
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    placeholder: {
      type: String,
      default: "",
    },
    inputform: {
      type: Object,
      default: () => ({}),
    },
  },
  emits: ["update:modelValue", "rawInput"],
  data() {
    return {
      validationMessage: "",
      inputError: false,
    };
  },
  computed: {
    isInputValid() {
      if (this.inputError == true) {
        this.validateMoney();
      }
      return !this.inputError;
    },
    config() {
      return {
        decimal: ",",
        thousands: ".",
        prefix: "R$ ",
        suffix: "",
        precision: 2,
        masked: false,
      };
    },
  },
  methods: {
    sendInputValue(event) {
      this.$nextTick(() => {
        const rawValue = this.getRawValue();
        this.$emit("update:modelValue", event.target.value);
        this.$emit("rawInput", rawValue);
        this.validateMoney();
      });
    },
    validateMoney() {
      if (!this.modelValue || this.modelValue.length === "") {
        this.validationMessage = "Campo não pode ficar vazio";
        this.inputError = true;
      } else {
        this.validationMessage = "";
        this.inputError = false;
      }
      return !this.inputError;
    },
    getRawValue() {
      const rawValue = this.$refs.moneyInput.value.replace(/[^\d.-]/g, "");
      return rawValue;
    },
  },
};
</script>

<style></style>
