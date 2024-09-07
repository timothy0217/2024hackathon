<script setup lang="ts">
import { computed, ref } from 'vue';
import { storeToRefs } from 'pinia';
import { useRoute, RouterLink } from 'vue-router';
import BaseInput from '@/components/atoms/BaseInput.vue';
import ServiceTabsView from '@/components/organisms/ServiceTabsView.vue';
import HotList from '@/components/organisms/HotList.vue';
import { useCouponStore } from '@/stores/coupon';
import couponDataJson from '../../public/mock/coupon_data.json';

</script>

<template>
  <div>
  </div>
</template>

<style lang="postcss">
.ticket-hot-button {
  @apply rounded-md shadow-md relative;
  @apply bg-primary-100;
  @apply p-4;

  & > img {
    @apply absolute top-0 right-2;
  }
}
</style>
