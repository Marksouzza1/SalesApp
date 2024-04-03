import apiService from "./api.service";

const ROUTE = "v1";
function getPaginationFromResponse(response) {
  return {
    pageSize: parseInt(response.headers["x-page-size"]),
    totalPages: parseInt(response.headers["x-total-pages"]),
  };
}
const CustomerService = {
  getAll(params) {
    return apiService.get(`${ROUTE}/customer`, params);
  },
  getById(id) {
    return apiService.get(`${ROUTE}/customer/${id}`);
  },
  delete(id) {
    return apiService.delete(`${ROUTE}/customer/${id}`);
  },
  post(params) {
    return apiService.post(`${ROUTE}/customer`, params);
  },
  update(id, params) {
    return apiService.update(`${ROUTE}/customer/${id}`, params);
  },
  async getPaginatedData(params) {
    const response = await apiService.post(`${ROUTE}/customers`, params);
    const pagination = getPaginationFromResponse(response);
    return { data: response.data, pagination };
  },
};
export default CustomerService;
