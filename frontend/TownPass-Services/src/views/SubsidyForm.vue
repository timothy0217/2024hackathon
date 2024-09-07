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
import type { Baby,Subsidies} from '@/ts/baby';

import type { User } from '@/stores/user';
import axios from 'axios';


const calculateAge = (birthDate: string): number => {
  // 將出生日期字串轉換為 Date 對象
  const birth = new Date(birthDate);
  const today = new Date();

  let age = today.getFullYear() - birth.getFullYear();
  const monthDifference = today.getMonth() - birth.getMonth();
  
  // 如果月份差異為負，或者當月份為出生月份但當前日期在出生日期之前，則年齡應減少 1
  if (monthDifference < 0 || (monthDifference === 0 && today.getDate() < birth.getDate())) {
    age--;
  }

  return age;
};

const Baby123 = ref<Partial<Baby>>({
  Birth:'',
  SubsidyName: '' , // 初始化為空字串以顯示「請選擇」
  Daycare: '',
  BirthOrder:'1',
  Age:"1",
  Subsidies:{
    Details: [],
    Amount: ''
  }
});


const store = useFormStore();

store.reset();

const userStore = useUserStore();

const { user } = storeToRefs(userStore);

const handleUserInfo = (event: { data: string }) => {
  const result: { name: string; data: User } = JSON.parse(event.data);

  user.value = result.data;
};

const ResetAll = () =>{
  Baby123.value.Birth ='';
  Baby123.value.BirthOrder ='';
  Baby123.value.SubsidyName ='';
  Baby123.value.Daycare ='';
}

const test = () => {
  if(formattedBirth.value === ''){
    const label = document.querySelector('.birthvali');
    label.classList.remove('vali');
  }
  if(Baby123.value.SubsidyName === ''){
    const label = document.querySelector('.Subsidyvali');
    label.classList.remove('vali');
  }
if(Baby123.value.Daycare === ''){
  const label = document.querySelector('.Daycare');
  label.classList.remove('vali');
}
  ReadFireStore1();

  // console.log(Baby123.value.Birth);
  // console.log(Baby123.value.BirthOrder);
  // console.log(Baby123.value.SubsidyName);
  // console.log(Baby123.value.Daycare);
  // 你可以在這裡執行更多邏輯
};

async function ReadFireStore1(ageFilter: number) {
  try {
    const querySnapshot = await getDocs(collection(db, 'Subsidies'));

    querySnapshot.forEach((doc) => {
      const data = doc.data() as Subsidies;
      console.log('Subsidy:', data);
      // 假設我們要檢查 subsidies[0].details.age 是否等於 ageFilter
      if (data.subsidies[Number(Baby123.value.BirthOrder)- 1].details.length > 0) {
        const matchingDetail = data.subsidies[Number(Baby123.value.BirthOrder)- 1].details.filter(detail => detail.subsidyName === Baby123.value.SubsidyName && detail.age === calculateAge(Baby123.value.Birth) && (detail.daycare === Baby123.value.Daycare||Baby123.value.SubsidyName === '育兒津貼'));
        Baby123.value.Subsidies.Details = matchingDetail;
        if (matchingDetail) {
          console.log('Matching Subsidy:', matchingDetail);
        } else {
          console.log(`No matching subsidies with age ${ageFilter}`);
        }
      } else {
        console.log('No details available in this subsidy.');
      }
    });
  } catch (error) {
    console.error('Error reading Firestore:', error);
  }
}

const ResetDaycareVali = () =>{

  const label = document.querySelector('.Daycare');
    label.classList.add('vali');
}

const ResetDateVali = () =>{

const label = document.querySelector('.birthvali');
  label.classList.add('vali');
}

const ResetDaycare = () =>{
  Baby123.value.Daycare = '';
  const label = document.querySelector('.Subsidyvali');
    label.classList.add('vali');
}

/// 處理日期為 YYYY-MM-DD 格式的計算屬性
const formattedBirth = computed({
  get() {
    // 如果有日期，將它轉換為 YYYY-MM-DD 格式
    if (Baby123.value.Birth) {
      const date = new Date(Baby123.value.Birth);
      const year = date.getFullYear();
      const month = String(date.getMonth() + 1).padStart(2, '0'); // 月份補零
      const day = String(date.getDate()).padStart(2, '0'); // 天數補零
      return `${year}-${month}-${day}`; // 返回 YYYY-MM-DD 格式
    }
    return ''; // 沒有日期時返回空
  },
  set(value: string) {
    // 當用戶選擇日期時，將其保存為 YYYY-MM-DD 格式的字符串
    Baby123.value.Birth = value;
  }
});

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
    <div class="form-container">
      <form>
        <!-- 出生日期 -->
        <label for="birthday">出生日期</label>
        <input type="date" v-model="formattedBirth" @change="ResetDateVali()" required>
        <div class="birthvali vali" >
          <label style="color: red;">請輸入出生日期</label>
        </div>
        

        <!-- 第幾胎 -->
        <label for="birth-order">第幾胎</label>
        <select v-model="Baby123.BirthOrder" required>
          <option value="1">一胎</option>
          <option value="2">二胎</option>
          <option value="3">三胎以上</option>
        </select>

        <!-- 扶養類型 -->
    <label for="SubsidyName">扶養類型</label>
    <select v-model="Baby123.SubsidyName" @change="ResetDaycare()" required>
      <option value="">請選擇</option>
      <option value="育兒津貼">育兒</option>
      <option value="托育補助">托育</option>
      <option value="幼兒園補助">幼兒園</option>
    </select>
    <div class="Subsidyvali vali" >
          <label style="color: red;">請輸入扶養類型</label>
        </div>

    <!-- 根據 SubsidyName 顯示不同的下拉選單 -->
    
    <!-- 育兒類型 -->
    <div v-if="Baby123.SubsidyName === '育兒津貼'">
      <label for="Parenting-type">育兒類型</label>
      <select v-model="Baby123.Daycare" @change="ResetDaycareVali()" required>
        <option value="">請選擇</option>
        <option value="父母">父母、親人</option>
        <option value="保母">保母(無政府簽約)</option>
        <option value="托嬰中心">托嬰中心(無政府簽約)</option>
      </select>
    <div class="Daycare vali" >
          <label style="color: red;">請輸入育兒類型</label>
        </div>
    </div>

    <!-- 托育類型 -->
    <div v-if="Baby123.SubsidyName === '托育補助'">
      <label for="Daycare-type">托育類型</label>
      <select v-model="Baby123.Daycare" @change="ResetDaycareVali()" required>
        <option value="">請選擇</option>
        <option value="公托">公托</option>
        <option value="準公托">準公托</option>
      </select>
    <div class="Daycare vali" >
          <label style="color: red;">請輸入托育類型</label>
        </div>
    </div>



    <!-- 幼兒園類型 -->
    <div v-if="Baby123.SubsidyName === '幼兒園補助'">
      <label for="kindergarten-type">幼兒園類型</label>
      <select v-model="Baby123.Daycare" @change="ResetDaycareVali()" required>
        <option value="">請選擇</option>
        <option value="公幼">公幼</option>
        <option value="非營利">非營利</option>
        <option value="準公幼">準公幼</option>
      </select>
    <div class="Daycare vali" >
          <label style="color: red;">請輸入幼兒園類型</label>
        </div>
    </div>



        <!-- 查詢和清除按鈕 -->
        <div class="button-group">
          <button type="button" class="btn-query" @click="test()">查詢</button>
          <button type="reset" class="btn-clear" @click="ResetAll()">清除</button>
        </div>
      </form>

      <div v-if=" Baby123.Subsidies.Details.length > 0 ">
        <ul>
      <li v-for="detail in Baby123.Subsidies.Details" :key="detail.subsidyName">
        <details>
        <summary>{{detail.unit}} 可補助<span class="amount">$ {{ detail.amount }}</span></summary>
        <div class="subsidy-info">
          <div v-if="Baby123.SubsidyName === '育兒津貼'">
            <a href="https://e-service.k12ea.gov.tw/" target="_blank">育兒補助連結</a>
          <p class="stars">育兒津貼 0~5 歲</p>
          <p>
請填具申請書並檢附相關文件，親送、郵寄，向幼兒現戶籍所在地的以下地點提出申請：
嘉義市：請親送或郵寄至聯合里辦公處。
其他21縣（市）：請親送或郵寄至幼兒戶籍所在地的鄉（鎮、市、區）公所。
</p>
</div>

<div v-if="Baby123.SubsidyName === '托育補助'">
  
  <a href="https://cwisonline.sfaa.gov.tw/" target="_blank">托育補助連結</a>
          <p class="stars">申請資格</p>
          <p>
一、0至未滿3歲之幼兒、沒有正在領育兒津貼或接受政府安置收容，且有送公共化托育、準公托、保母送托或與政府簽約之公設民營托嬰中心者。
</p>
<p>二、2024年起調高，公共托育的補助將從每月5,500元提升至7,000元，而準公共托育的補助將從每月8,500元增加到1萬3,000元。</p>
<p>備註:第二胎加1000、第三胎以上加2000。</p>
</div>

<div v-if="Baby123.SubsidyName === '幼兒園補助'">
  
  <a href="https://e-service.k12ea.gov.tw/" target="_blank">幼兒園補助連結</a>
          <p class="stars">申辦資格</p>
          <p>
一、2至未滿5歲幼兒育兒津貼之幼兒為實際年齡滿2歲至未滿5歲學齡的我國籍幼兒(113學年度為108年9月2日(含)以後出生至生理年齡滿2歲)。
</p>
<p>
二、5歲至入國民小學前幼兒就學補助之幼兒為「學齡滿5歲」至「學齡未滿6歲」的我國籍幼兒（113學年度為107年9月2日(含)至108年9月1日間出生，即尚未就讀國民小學者）。
</p>
</div>
</div>
      </details>
      </li>
    </ul>
</div>
</div>
  </main>
</template>

<style scoped>
body {
  font-family: Arial, sans-serif;
  background-color: #EAF8FB;
  padding: 20px;
  margin: 0;
}

.vali{
  display: none;
}

.form-container {
  background-color: #ffffff;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  max-width: 400px;
  margin: 0 auto;
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

/* 下拉補助資訊區域 */
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
</style>
