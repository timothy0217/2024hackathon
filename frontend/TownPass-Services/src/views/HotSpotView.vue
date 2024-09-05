<script setup lang="ts">
import { computed, ref } from 'vue';
import { RouterLink, useRoute, useRouter } from 'vue-router';
import { storeToRefs } from 'pinia';
import { useCouponStore } from '@/stores/coupon';
import { useConnectionMessage } from '@/composables/useConnectionMessage';
import { useHandleConnectionData } from '@/composables/useHandleConnectionData';
import BaseDialog from '@/components/atoms/BaseDialog.vue';
import CouponResultItem from '@/components/molecules/CouponResultItem.vue';

const store = useCouponStore();
const route = useRoute();
const router = useRouter();


</script>

<template>
  <div></div>>
</template>
