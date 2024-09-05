<script setup lang="ts">
import { ref, computed } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { storeToRefs } from 'pinia';
import { useCouponStore } from '@/stores/coupon';
import { useConnectionMessage } from '@/composables/useConnectionMessage';
import { useHandleConnectionData } from '@/composables/useHandleConnectionData';
import BaseCardlabel from '@/components/atoms/BaseCardlabel.vue';
import { CardLabelType } from '@/components/atoms/BaseCardlabel.vue';
import BaseDialog from '@/components/atoms/BaseDialog.vue';
import BaseButton from '@/components/atoms/BaseButton.vue';

const route = useRoute();
const router = useRouter();

</script>

<template>
  <div></div>
</template>
