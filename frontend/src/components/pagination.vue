<template>
  <div class="pagination">
    <a @click="prevPage">&laquo;</a>
    <a class="active">{{ pageNumber }}</a>
    <a @click="nextPage">&raquo;</a>
  </div>
</template>
<script>
export default {
  name: "Pagination",
  props: {
    totalPages: {
      type: Number,
      required: true,
    },
  },
  emits: ["value-updated"],
  data() {
    return {
      pageNumber: 1,
    };
  },
  methods: {
    nextPage() {
      if (this.pageNumber < this.totalPages) {
        this.pageNumber++;
        this.$emit("value-updated", this.pageNumber);
      }
    },
    prevPage() {
      if (this.pageNumber > 1) {
        this.pageNumber--;
        this.$emit("value-updated", this.pageNumber);
      }
    },
  },
};
</script>
<style scoped>
a {
  cursor: pointer;
}
.pagination {
  display: inline-block;
}

.pagination a {
  color: black;
  float: left;
  padding: 8px 16px;
  text-decoration: none;
}

.pagination a.active {
  background-color: #1655df;
  color: white;
}

.pagination a:hover:not(.active) {
  background-color: #ddd;
}
</style>
