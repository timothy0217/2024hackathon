import { createRouter, createWebHistory } from 'vue-router';
// import HomeView from '../views/vaccine-timeline.vue';
import FormView from '../views/FormView.vue';
import SurroundingServiceView from '../views/SurroundingServiceView.vue';
import TopQnAListView from '../views/TopQnAListView.vue';
import QnAListView from '../views/QnAListView.vue';
import CouponView from '../views/CouponView.vue';
import CouponListView from '../views/CouponListView.vue';
// import HotSpotView from '../views/vaccine-timeline.vue';
import TicketDetail from '../views/TicketDetail.vue';
import CouponDetail from '../views/CouponDetail.vue';
import HomeView from '@/views/HomeView.vue';
import SubsidyForm from '@/views/SubsidyForm.vue';
import addChild from '@/views/add-child.vue';
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/subsidyForm',
      name: 'subsidyForm',
      component: SubsidyForm
    },
    {
      path: '/addChild',
      name: 'addChild',
      component: addChild
    },
  ]
});

export default router;
