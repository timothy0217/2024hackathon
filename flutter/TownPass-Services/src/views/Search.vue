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

import BaseRadio from '@/components/atoms/BaseRadio.vue';
import BaseDialog from '@/components/atoms/BaseDialog.vue';

const props = defineProps<{
  submitForm: any;
}>();


const store = useFormStore();

store.reset();

const userStore = useUserStore();

const { user } = storeToRefs(userStore);

const handleUserInfo = (event: { data: string }) => {
  const result: { name: string; data: User } = JSON.parse(event.data);

  user.value = result.data;
};
useConnectionMessage('userinfo', null);

useHandleConnectionData(handleUserInfo);

const route = useRoute();

const activeTab = ref(0);

if (route.query.isSearch) {
  activeTab.value = 1;
}

/**
 * tab0 JS start
 */
const searchValue = ref('');
const serviceList = ref(serviceListJson);
const isSearch = ref(false);

const flatServiceList = computed(() =>
  serviceList.value.data
    .map((item) => item.agency)
    .reduce((prev, acc) => [...acc, ...prev], [])
    .map((item) => item.options)
    .reduce((prev, acc) => [...acc, ...prev], [])
);

const expandList = ref<string[]>([]);
const expandListSet = computed(() => new Set(expandList.value.map((name) => name)));

const onExpandClick = (name: string) => {
  if (expandListSet.value.has(name)) {
    const index = expandList.value.findIndex((el) => el === name);
    expandList.value.splice(index, 1);
  } else {
    expandList.value.push(name);
  }
};

const searchResult = ref<
  {
    id: string;
    title: string;
    subtitle: string;
    type: string;
    agency_type: string;
  }[]
>();

const searchResultTypeSet = computed(() => new Set(searchResult.value?.map((item) => item.type)));
const searchResultAgencyTypeSet = computed(
  () => new Set(searchResult.value?.map((item) => item.agency_type))
);
const searchResultTitle = computed(() => searchResult.value?.map((item) => item.title));

const onSearchClick = () => {
  const result = flatServiceList.value.filter((option) => option.title.includes(searchValue.value));

  searchResult.value = result;
  expandList.value = Array.from(new Set(result.map((item) => item.type)));

  if (searchValue.value) {
    isSearch.value = true;
  } else {
    isSearch.value = false;
  }
};
const ReadRealtime=()=>{
  get(realtimeRef).then((snapshot) => {
        if (snapshot.exists()) {
            console.log(snapshot.val());
        } else {
            console.log('No data available');
        }
    }).catch((error) => {
        console.error(error);
    });  
}

const ReadFireStore=async ()=>{
  const querySnapshot = await getDocs(collection(db, "users"));
querySnapshot.forEach((doc) => {
  console.log(`${doc.id} => ${JSON.stringify(doc.data(), null, 2)}`);
});
// querySnapshot.forEach((doc) => {
//   console.log(`${doc.id} => ${doc.data()}`);
// });
}

//local開發
const GetAPI = () => {
  axios.get('/api/v1/dataset/1c404c84-0e89-4e2f-bd26-3b8c427d550c?scope=resourceAquire&offset=0')
    .then(response => {
      console.log(response.data);
    })
    .catch(error => {
      console.error(error);
    });
}

const activeSituation = ref('apply');

const caseProgress = ref(caseProgressJson);

const applyRecord = computed(() =>
  caseProgress.value.data.filter((item) => item.current_step !== item.total_step)
);
const finishRecord = computed(() =>
  caseProgress.value.data.filter((item) => item.current_step === item.total_step)
);

const activeRecord = computed(() =>
  activeSituation.value === 'apply' ? applyRecord.value : finishRecord.value
);
/**
 * tab1 JS end
 */
</script>

<template>
  <main>
    <div class="navbar">
        <span>生育補助查詢</span>
        <img src="/src/assets/images/person-icon.svg">
    </div>

    <section class="flex items-center px-4">
        <BaseInput v-model="searchValue" placeholder="輸入關鍵字" class="flex-grow" />
        <button class="search-button" @click="onSearchClick">
            <img src="@/assets/images/search-icon.svg" alt="搜尋" />
        </button>
    </section>

    <ul class="px-4 py-2 flex flex-col gap-y-4">
      <li class="preview-item">
        <div class="list-data">
          <span class="field-name"><a href="#">臺北市嬰幼兒照顧服務_公辦民營托嬰中心</a></span>
          <p>嬰幼兒照顧服務_公辦民營托嬰中心</p>
        </div>
        <div class="btn-download">下載</div>
      </li>
      <li class="preview-item">
        <div class="list-data">
          <span class="field-name"><a href="#">臺北市社區公共保母政策評估研究案</a></span>
          <p>一、研究緣起本案之執行目的，在於了解臺北市推動社區公共保母政策之成效，是否達成平價、優質、普及等目標...</p>
        </div>
        <div class="btn-download">下載</div>
      </li>
    </ul>
    <footer>
      <div class="footer-tab active">
        <span>
          <img src="@/assets/images/search-icon.svg" alt="搜尋"/>查詢
        </span>
      </div>
      <div class="footer-tab">
        <span>
          <img src="../assets/images/icon-history.svg" alt="紀錄"/>下載紀錄
        </span>
      </div>
    </footer>
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

.field-name {
    @apply text-gray-500;
  }
  
  .preview-item {
    @apply flex justify-between;
  
    &--attachment {
      @apply flex-col justify-start;
    }
  }

template{
  background-color: #e3e7e9;
  min-height: calc(100% - 60px);
}

.navbar{
  display: flex;
  flex-direction: row;
  color: #475259;
  background-color: #fff;
  margin-bottom: 16px;
}

.list-data p{
  font-size: 12px;
  color: #738995;
}

.btn-download{
  background-color: #76a732;
  border-radius: 40px;
  padding: 4px 8px;
}

footer{
  display: flex;
  flex-direction: row;
  background-color: #fff;
  height: 60px;
}
.footer-tab{
  display: flex;
  flex-direction: column;
  width: 50%;
  justify-content: center;
  align-items: center;
}
.footer-tab.active{
  color: #5ab4c5;
}
</style>