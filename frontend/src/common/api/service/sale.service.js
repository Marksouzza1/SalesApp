import apiService from "./api.service";
const ROUTE = "V1";
function getPaginationFromResponse(response) {
  return {
    pageSize: parseInt(response.headers["x-page-size"]),
    totalPages: parseInt(response.headers["x-total-pages"]),
  };
}
const saleService = {
  getAll(params) {
    return apiService.get(`${ROUTE}/sale`, params);
  },
  getById(id) {
    return apiService.get(`${ROUTE}/sale/${id}`);
  },
  delete(id) {
    return apiService.delete(`${ROUTE}/sale/${id}`);
  },
  post(params) {
    return apiService.post(`${ROUTE}/sale`, params);
  },
  update(id, params) {
    return apiService.update(`${ROUTE}/sale/${id}`, params);
  },
  async getPaginatedData(params) {
    const response = await apiService.post(`${ROUTE}/getall`, params);
    const pagination = getPaginationFromResponse(response);
    return { data: response.data, pagination };
  },
};
export default saleService;
