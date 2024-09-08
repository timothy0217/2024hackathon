<script setup lang="ts">
import { computed, onMounted, ref, watchEffect } from 'vue';
import { useRoute, RouterLink } from 'vue-router';
import { storeToRefs } from 'pinia';
import { useCouponStore } from '@/stores/coupon';
import BaseInput from '@/components/atoms/BaseInput.vue';
import CouponListResult from '@/components/organisms/CouponListResult.vue';
import HotSpotList from '@/components/organisms/HotSpotList.vue';
import type { Ticket, Coupon } from '@/views/CouponView.vue';

const route = useRoute();

</script>

<template>
  <div></div>
</template>
