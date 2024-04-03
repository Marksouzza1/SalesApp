import { createRouter, createWebHistory } from "vue-router";
import Layout from "../layouts/Layout.vue";

const routes = [
  {
    path: "/",
    redirect: "/dashboard",
    component: Layout,
    children: [
      {
        path: "/customer/Form/:id?",
        name: "edit-customer-Form",
        props: true,
        component: () => import("../views/customers/Form.vue"),
      },
      {
        path: "/dashboard",
        name: "dashboard",
        component: () => import("../views/dashboard/Index.vue"),
      },
      {
        path: "/customers/Form",
        name: "customers-Form",
        component: () => import("../views/customers/Form.vue"),
      },
      {
        path: "/sales",
        name: "sales",
        component: () => import("@/views/sales/Index.vue"),
      },
      {
        path: "/sales/Form",
        name: "sales-Form",
        component: () => import("@/views/sales/Form.vue"),
      },

      {
        path: "/sales/Form/:id?",
        name: "edit-sales-Form",
        props: true,
        component: () => import("@/views/sales/Form.vue"),
      },
      {
        path: "/customers",
        name: "customers",
        component: () => import("@/views/customers/Index.vue"),
      },
    ],
  },
  {
    path: "/login",
    name: "login",
    component: () => import("@/views/authentication/Login.vue"),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
