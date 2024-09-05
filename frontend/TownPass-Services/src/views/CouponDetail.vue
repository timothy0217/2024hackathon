<script setup lang="ts">
import { ref, computed } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { storeToRefs } from 'pinia';
import { useCouponStore } from '@/stores/coupon';
import { useConnectionMessage } from '@/composables/useConnectionMessage';
import { useHandleConnectionData } from '@/composables/useHandleConnectionData';
import ServiceTabsView from '@/components/organisms/ServiceTabsView.vue';
import BaseDialog from '@/components/atoms/BaseDialog.vue';
import BaseButton from '@/components/atoms/BaseButton.vue';

const router = useRouter();
const route = useRoute();

</script>

<template>
 <div></div>>
</template>
