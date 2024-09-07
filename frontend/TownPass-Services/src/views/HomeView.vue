<script setup lang="ts">
import { computed, ref } from 'vue';
import { RouterLink, useRoute } from 'vue-router';
import { useFormStore } from '@/stores/form';
import { useUserStore } from '@/stores/user';
import { storeToRefs } from 'pinia';
import { useConnectionMessage } from '@/composables/useConnectionMessage';
import { useHandleConnectionData } from '@/composables/useHandleConnectionData';
import ServiceTabsView from '@/components/organisms/ServiceTabsView.vue';
import BaseInput from '@/components/atoms/BaseInput.vue';
import ServiceStep from '@/components/molecules/ServiceStep.vue';
import serviceListJson from '../../public/mock/service_list.json';
import caseProgressJson from '../../public/mock/case_progress.json';
import BaseButton from '@/components/atoms/BaseButton.vue';
import { get, child } from 'firebase/database';
import { realtimeRef } from '../../firebaseConfig';
import { collection, getDocs } from "firebase/firestore"; 
import { db } from '../../firebaseConfig';


import type { User } from '@/stores/user';
import axios from 'axios';


const store = useFormStore();

store.reset();

const userStore = useUserStore();

const { user } = storeToRefs(userStore);

const handleUserInfo = (event: { data: string }) => {
  const result: { name: string; data: User } = JSON.parse(event.data);

  user.value = result.data;
};

/**
 * 同頁面要處理多個雙向連結資料參考
 */
// const handleConnectionData = (event: { data: string }) => {
//   const result: { name: string; data: any } = JSON.parse(event.data);
//   const name = result.name;

//   switch (name) {
//     case 'userinfo':
//       handleUserInfo(event);
//       break;
//     case 'phone_call':
//       //....
//       break;
//     default:
//       break;
//   }
// };

useConnectionMessage('userinfo', null);

useHandleConnectionData(handleUserInfo);

/**
 * tab1 JS end
 */
</script>

<template>
  <main>
  </main>
</template>

<style lang="postcss">
.search-button {
  @apply bg-primary-500 p-1 rounded-lg;
  @apply h-11 w-11;
  @apply flex justify-center items-center;
  @apply -translate-x-1;
}

.option-title {
  @apply relative;
  @apply before:content-[''];
  @apply before:w-1.5 before:h-0.5;
  @apply before:bg-primary-500;
  @apply before:inline-block;
  @apply before:absolute before:-left-3.5 before:top-1/2 before:-translate-y-1/2;
}

.situation-button {
  @apply text-primary-500;
  @apply first:rounded-l last:rounded-r;
  @apply border border-primary-500;
  @apply py-0.5;

  &--active {
    @apply bg-primary-500 text-white;
  }
}
</style>
