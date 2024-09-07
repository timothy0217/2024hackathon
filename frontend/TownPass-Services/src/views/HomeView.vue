<script setup lang="ts">
import { computed, onMounted, ref } from 'vue';
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
import { collection, getDocs } from "firebase/firestore"; 
import { db } from '../../firebaseConfig';
import type { User } from '@/stores/user';
import axios from 'axios';
import SubsidyForm from './SubsidyForm.vue';

const showModal = ref(false);
const year = ref('');
const month = ref('');
const activeTab = ref(1);
const store = useFormStore();

store.reset();

const userStore = useUserStore();

const { user } = storeToRefs(userStore);

const handleUserInfo = (event: { data: string }) => {
  const result: { name: string; data: User } = JSON.parse(event.data);

  user.value = result.data;
};

// 打開彈窗
const openModal=()=> {
  showModal.value = true;
}

// 關閉彈窗
const closeModal=()=> {
  showModal.value = false;
}

// 設置疫苗搜尋功能
const searchVaccine = (entrySelector: string) => {
  if (!year.value || !month.value) {
    alert('请选择年份和月份');
    return;
  }

  const entries = document.querySelectorAll<HTMLDivElement>(`.${entrySelector} .date`);
  let found = false;

  entries.forEach(entry => {
    const entryDate = entry.textContent?.trim() ?? '';
    const [entryYear, entryMonth] = entryDate.split('/');

    if (entryYear === year.value && entryMonth === month.value) {
      entry.scrollIntoView({ behavior: 'smooth' });
      found = true;
    }
  });

  if (!found) {
    alert('未找到匹配的疫苗记录');
  }
}

// 切換選項卡
const showPage = (pageNumber: number) => {
  activeTab.value = pageNumber;
  localStorage.setItem('activeTab', pageNumber.toString());
}


// 初始化頁面
onMounted(() => {
  const savedTab = localStorage.getItem('activeTab');
  if (savedTab) {
    showPage(parseInt(savedTab));
  } else {
    showPage(1);
  }
});

</script>

<template>
  <main>
    <!-- Header -->
    <div class="header index">
        <img src="/image/hamber.png" class="nav" id="open-modal" @click="openModal">
      <span>王大明</span>
      <div class="space"></div>
    </div>

    <!-- Form Container -->
    <div class="form-container no-style">
      <video autoplay muted>
        <source src="/image/2.mp4" type="video/mp4">
      </video>
    </div>

    <!-- Alert -->
    <div class="alert">
      <span>2024/09/03 將要施打Ｂ型肝炎疫苗(HepB)疫苗</span>
    </div>

    <!-- Modal -->
    <div class="nav-modal" v-if="showModal">
  <div class="modal-content">
    <div class="details-modal-title">
      <img src="/image/people.png">
      <span>王大明</span>
    </div>
    <RouterLink to="/addchild" class="details-modal-content">
  <img src="/image/add.png" alt="Add Child">
  <span>新增孩童</span>
</RouterLink>
    <button id="close-modal" @click="closeModal">Close</button>
    <button id="tab-1">
  <RouterLink to="/subsidyform" style="color: #007B80">補助查詢</RouterLink>
</button>
  </div>
</div>


    <!-- Footer -->
    <div class="footer">
      <button id="tab-1">
  <RouterLink to="/subsidyform" style="color: #007B80">補助查詢</RouterLink>
</button>
      <button id="tab-2">
  <RouterLink to="/vaccine-timeline" style="color: #007B80">疫苗軌跡</RouterLink>
</button>
    </div>
  </main>
</template>




<style scoped>
body {
    font-family: Arial, sans-serif;
    background-color: #EAF8FB;
    padding: 0;
    margin: 0;
}

.header {
    background-color: #ffffff;
    text-align: center;
    padding: 15px;
    font-size: 18px;
    font-weight: bold;
    color: #333;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.header.index{
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.footer {
    position: fixed;
    bottom: 0;
    width: 100%;
    background-color: #ffffff;
    display: flex;
    justify-content: space-around;
    padding: 10px;
    box-shadow: 0 -2px 4px rgba(0, 0, 0, 0.1);
}

.footer button {
    background-color: #EAF8FB;
    border: none;
    padding: 10px;
    font-size: 16px;
    cursor: pointer;
    color: #007B80;
    font-weight: bold;
}

.footer button.active {
    color: #007B80;
    border-bottom: 2px solid #007B80;
}

.form-container {
    background-color: #ffffff;
    padding: 20px;
    border-radius: 10px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    max-width: 400px;
    margin: 80px auto 80px;
}

.form-container.no-style{
    background-color: transparent;
    box-shadow: none;
    position: relative;
    margin: 0;
    padding: 0;
    display: contents;
}

.form-container.no-style video{
    width: 100%;
}

.hidden {
    display: none;
}

.form-container label {
    display: block;
    margin-bottom: 8px;
    font-size: 14px;
    color: #333;
}

.form-container input[type="date"],
.form-container select {
    width: 100%;
    padding: 10px;
    margin-bottom: 20px;
    border: 1px solid #ccc;
    border-radius: 5px;
    font-size: 16px;
    box-sizing: border-box;
}

.form-container input[type="date"] {
    -webkit-appearance: none;
    -moz-appearance: none;
    appearance: none;
}

.radio-group {
    margin-bottom: 20px;
}

.radio-group label {
    font-size: 14px;
    margin-right: 15px;
    color: #333;
}

.radio-options {
    display: flex;
    align-items: center;
    margin-bottom: 10px;
}

.radio-options input[type="radio"] {
    margin-right: 5px;
    accent-color: #4DAECB;
}

.form-container button {
    width: 48%;
    padding: 10px;
    font-size: 16px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
}

/* 按鈕的通用樣式 */
.btn-query {
    background-color: #4DAECB;
    color: white;
    padding: 10px 20px;
    font-size: 16px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.btn-query:hover {
    background-color: #007B80;
}

.form-container .btn-query {
    background-color: #4DAECB;
    color: white;
}

.form-container .btn-clear {
    background-color: #E0F5FA;
    color: #4DAECB;
    border: 1px solid #4DAECB;
}

.form-container .button-group {
    display: flex;
    justify-content: space-between;
}

.form-container

details {
    background-color: #F5F5F5;
    border-radius: 10px;
    margin-top: 10px;
    padding: 10px;
    color: #333;
}

summary {
    font-size: 18px;
    color: #007B80;
    cursor: pointer;
    margin-bottom: 10px;
    font-weight: bold;
}

details[open] summary {
    margin-bottom: 15px;
}

.subsidy-info p {
    font-size: 14px;
    margin: 5px 0;
}

.subsidy-info .amount {
    color: #E63946;
    font-weight: bold;
}

.subsidy-info .stars {
    color: #333;
}


/* Vaccine-timeline*/
/* 篩選區域樣式，確保按鈕與選擇框對齊 */
.filter-section {
    display: flex;
    justify-content: center;
    align-items: center; /* 確保按鈕和下拉選單在同一水平線 */
    gap: 10px;
}

.filter-section select {
    padding: 10px;
    font-size: 16px;
    border: 1px solid #ccc;
    border-radius: 5px;
    height: 40px; /* 確保下拉選單與按鈕高度一致 */
}

/* 查詢按鈕風格 */
.btn-search {
    background-color: #4DAECB;
    border: none;
    border-radius: 50%;
    width: 40px;
    height: 40px;
    display: flex;
    justify-content: center;
    align-items: center;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.btn-search img {
    width: 20px;
    height: 20px;
}

.btn-search:hover {
    background-color: #007B80;
}

/* 日期和內容的佈局 */
.vaccine-entry {
    display: flex;
    align-items: flex-start;
    margin-bottom: 20px;
}

/* 日期樣式，左側對齊 */
.vaccine-entry .date {
    font-size: 16px;
    font-weight: bold;
    color: #333;
    background-color: #EAF8FB;
    padding: 10px;
    border-radius: 5px;
    width: 100px;
    text-align: center;
    margin-right: 20px;
}

/* 內容樣式，右側對齊 */
.vaccine-info {
    background-color: #fff;
    border-radius: 5px;
    padding: 20px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    flex: 1;
}

.vaccine-info h3 {
    font-size: 18px;
    font-weight: bold;
    color: #007B80;
    margin-bottom: 10px;
}

/* 日期輸入框與儲存按鈕樣式 */
.input-section {
    margin-bottom: 15px; /* 增加與內容的間距 */
    display: flex;
    align-items: center;
}

.vaccine-date-input {
    padding: 10px;
    font-size: 14px;
    border: 1px solid #ccc;
    border-radius: 5px;
    margin-right: 10px;
    width: 150px;
}

.btn-save {
    background-color: #4DAECB;
    color: white;
    padding: 10px 20px;
    font-size: 14px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.btn-save:hover { /* 日期和內容的佈局 */

    .vaccine-entry {
        display: flex;
        align-items: flex-start;
        margin-bottom: 20px;
    }

    /* 日期樣式，左側對齊 */

    .vaccine-entry .date {
        font-size: 16px;
        font-weight: bold;
        color: #333;
        background-color: #EAF8FB;
        padding: 10px;
        border-radius: 5px;
        width: 100px;
        text-align: center;
        margin-right: 20px;
    }

    /* 內容樣式，右側對齊 */

    .vaccine-info {
        background-color: #fff;
        border-radius: 5px;
        padding: 20px;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        flex: 1;
        text-align: left; /* 保持內容文字左對齊 */
    }

    .vaccine-info h3 {
        font-size: 18px;
        font-weight: bold;
        color: #007B80;
        margin-bottom: 10px;
    }

    /* 日期和內容的佈局 */

    .vaccine-entry {
        display: flex;
        align-items: flex-start;
        margin-bottom: 20px;
    }

    /* 日期樣式，左側對齊 */

    .vaccine-entry .date {
        font-size: 16px;
        font-weight: bold;
        color: #333;
        background-color: #EAF8FB;
        padding: 10px;
        border-radius: 5px;
        width: 100px;
        text-align: center;
        margin-right: 20px;
    }

    /* 內容樣式，右側對齊 */

    .vaccine-info {
        background-color: #fff;
        border-radius: 5px;
        padding: 20px;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        flex: 1;
        text-align: left; /* 保持內容文字左對齊 */
    }

    .vaccine-info h3 {
        font-size: 18px;
        font-weight: bold;
        color: #007B80;
        margin-bottom: 10px;
    }
    /* 調整篩選區域的 select 和 button 對齊 */
    .filter-section {
        display: flex;
        align-items: center; /* 確保 select 和 button 在垂直方向對齊 */
        gap: 10px; /* 控制 select 和 button 之間的間距 */
    }

    .filter-section select {
        padding: 0 10px; /* 移除上下 padding 並保持左右內邊距 */
        font-size: 16px; /* 保持字體大小一致 */
        height: 42px; /* 設定固定高度 */
        line-height: 42px; /* 讓文字在高度內垂直居中 */
        border: 1px solid #ccc;
        border-radius: 5px;
    }

    .btn-search {
        background-color: #4DAECB;
        color: white;
        padding: 0 15px; /* 調整 padding 與 select 高度一致，移除上下 padding */
        font-size: 16px; /* 保持字體大小一致 */
        height: 42px; /* 設定固定高度 */
        line-height: 42px; /* 讓按鈕文字或圖標垂直居中 */
        border: none;
        border-radius: 5px;
        display: flex;
        justify-content: center;
        align-items: center; /* 確保圖標在按鈕內垂直居中 */
        cursor: pointer;
    }

    .btn-search img {
        width: 20px;
        height: 20px;
    }

    /* Media query for mobile devices */
    @media (max-width: 768px) {
        .filter-section {
            flex-direction: column;
            align-items: stretch;
        }

        .filter-section select,
        .filter-section .btn-search {
            width: 100%; /* 在手機端全寬 */
            margin-bottom: 10px;
        }

        .filter-section .btn-search {
            width: auto; /* 設定為自適應寬度 */
        }
    }


}

a {
    text-decoration: none; /* no underline */
}
/* 彈窗背景 */
.nav-modal {
  display: flex;
  align-items: center;
  justify-content: center;
  position: fixed; /* 确保弹窗浮动在页面上方 */
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5); /* 半透明背景 */
}
  
  /* 彈窗內容 */
  .modal-content {
  background-color: #fff;
  border-radius: 8px;
  padding: 20px;
  width: 80%;
  max-width: 500px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}
  /* 彈窗標題 */
  .details-modal-title {
    display: flex;
    align-items: center;
  }
  
  /* 彈窗內容 */
  .details-modal-content {
    margin-top: 10px;
    display: flex;
    align-items: center;
  }
  
  .alert{
    position: absolute; /* 絕對定位 */
    top: 20%;
    left: 50%; /* 水平居中，根據 .form-container 的寬度 */
    transform: translate(-50%, -50%); /* 調整元素位置，使其真正居中 */
    background-color: #ffffff;
    padding: 20px;
    border-radius: 10px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    max-width: 400px; /* 根據需要調整最大寬度 */
    z-index: 10; /* 確保在其他內容上面 */
    text-align: center; /* 使 alert 內容居中 */
}

.modal-content button{
    display: none;
}

.alert span{
    color: #d45251;
}
</style>
