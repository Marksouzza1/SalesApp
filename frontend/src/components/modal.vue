<template>
  <transition name="modal">
    <div v-if="show" class="modalMask">
      <div :class="{ modalContainer: !hasError, modalError: hasError }">
        <span class="modal-default-button" @click="$emit('close')">
          <i class="bi bi-x"></i>
        </span>
        <div class="modalHeader">
          <slot name="header"></slot>
          <div v-if="!hasError" class="iconBody">
            <i class="bi bi-check-lg"></i>
          </div>
          <div v-if="hasError" class="iconBody">
            <i class="bi bi-exclamation-triangle"></i>
          </div>
        </div>
        <div class="modalBody">
          <slot name="body">default body</slot>
        </div>

        <div class="modalFooter">
          <slot name="footer"> </slot>
        </div>
      </div>
    </div>
  </transition>
</template>
<script>
export default {
  name: "Modal",
  props: {
    show: Boolean,
    hasError: {
      type: Boolean,
      default: true,
    },
  },
  emits: ["close"],
};
</script>
<style>
.iconBody {
  text-align: center;
  font-size: 40px;
  cursor: pointer;
}
.modalMask {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  transition: opacity 0.3 ease;
}

.modalContainer {
  width: 400px;
  padding: 20px 20px;
  margin: auto;
  background-color: white;
  border-radius: 4px;
  box-shadow: 0 2px rgna(0, 0, 0, 0.33);
  transition: all 0.3 ease;
}

.modalError {
  width: 400px;
  padding: 20px 20px;
  margin: auto;
  color: white;
  background-color: #1c3b85;
  border-radius: 4px;
  box-shadow: 0 2px rgna(0, 0, 0, 0.33);
  transition: all 0.3 ease;
}

.modalHeader h3 {
  margin-top: 0;
  color: rgba(28, 59, 133, 1);
}

.modalBody {
  margin: 20px 0;
  text-align: center;
  font-size: 20px;
}

.modal-default-button {
  float: right;
  cursor: pointer;
}
@media screen and (max-width: 600px) {
  .modalContainer,
  .modalError {
    width: 70%;
    padding: 10px;
    border-radius: 0;
    box-shadow: none;
  }

  .modalHeader h3 {
    font-size: 18px;
  }

  .modalBody {
    font-size: 16px;
  }
}
</style>
