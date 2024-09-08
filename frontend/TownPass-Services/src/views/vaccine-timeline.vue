<script setup lang="ts">
import { computed, onBeforeMount, onMounted, reactive, ref, type Ref } from 'vue';
import { RouterLink, useRoute, useRouter } from 'vue-router';
import { storeToRefs } from 'pinia';
import { useCouponStore } from '@/stores/coupon';
import { useConnectionMessage } from '@/composables/useConnectionMessage';
import { useHandleConnectionData } from '@/composables/useHandleConnectionData';
import BaseDialog from '@/components/atoms/BaseDialog.vue';
import CouponResultItem from '@/components/molecules/CouponResultItem.vue';
import { vaccines } from '@/zipcode/vaccine';
import type { Baby, VaccineTracking} from '@/ts/baby';  
import { collection, getDocs } from 'firebase/firestore';
import { db } from '../../firebaseConfig';
// 將需要的 class 從 babyClasses.ts 中引入

const year =ref();
const month =ref();

const store = useCouponStore();
const route = useRoute();
const router = useRouter();
const lists =ref<VaccineTracking[]>();
    const user =ref<Baby>();
const baby = ref<Partial<Baby>>({
Baby:{
    Age: '0',
  Birthday: '',
  BirthOrder: '2024-01-01',//第幾胎
  Height: '',
  Weight: '',
  LowIncomeHousehold: '',
  IsIndigenous: false,
  SubsidyName: '',
  Daycare: '',
  VaccineTracking: [
    {
        Vaccinated: 0,
        Date: '',
        VaccineNameCN: '',
        VaccineNameEN: '',
        VaccinationSchedule: '',
        Dose: 0,
        Contraindications: ''
    }
  ],
  Subsidies: {
    Details:[],
    Amount:''
  }
}
  

});

// 从 Firestore 读取数据并更新 baby 变量
async function ReadFireStore1() {
  try {
    const querySnapshot = await getDocs(collection(db, 'Baby'));
    querySnapshot.forEach((doc) => {
       
      const cc = doc.data() as Baby;

      // 更新响应式对象
      console.log(cc.Baby.VaccineTracking);
      lists.value =cc.Baby.VaccineTracking;
      user.value =cc;
const bir ='2023-09-07';
lists.value.forEach((x=>{
        if(x.Dose==1){
            x.VaccinationSchedule = addDaysToBirthDay(bir, x.VaccinationSchedule);
        
        }
        else if(x.Dose==2){
            x.VaccinationSchedule = addDaysToBirthDay(bir, x.VaccinationSchedule);
        }
        else if(x.Dose==3){
            x.VaccinationSchedule = addDaysToBirthDay(bir, x.VaccinationSchedule);
        }
        else if(x.Dose==4){
            x.VaccinationSchedule = addDaysToBirthDay(bir, x.VaccinationSchedule);
        }
        
        //x.VaccinationSchedule = user.value?.Birth+x.VaccinationSchedule.split('|')[0];
      }));
      //baby.value.VaccineTracking =lists.value;
      console.log(user.value);
      //Object.assign(baby.value, data);
    });
  } catch (error) {
    console.error('Error reading Firestore:', error);
  }
}

function addDaysToBirthDay(birthDay: string, vaccinationSchedule: string): string {
  // Convert BirthDay string to a Date object
   var num = vaccinationSchedule.split('|')[0];

  var year = parseInt(birthDay.split('-')[0]);
  var month = parseInt(birthDay.split('-')[1]);
  var day = parseInt(birthDay.split('-')[2]);


   month =(month)+parseInt(num);

   if (month>12)
   {
    month -=12;
    year +=1;
   }

   if (month>12)
   {
    month -=12;
    year +=1;
   }

  // Add VaccinationSchedule days to the BirthDay
  
  return `${year}-${month}-${day}`;
}

const querydata=()=>{
    
    var month_ ="1";
    if (month.value=="10"|| month.value=="11"|| month.value=="12")
    {
        month_ = month.value;
    } 
    else
    {
        month_ = month.value.charAt(month.value.length - 1);
    }

    console.log( month.value);
    console.log( year.value);

    console.log( month_);

    lists.value = lists.value?.filter(m => m.VaccinationSchedule.split('-')[0]===year.value&&m.VaccinationSchedule.split('-')[1]===month_);
    console.log( lists.value);
}

onMounted(() => {
  ReadFireStore1();
  
});
</script>

<template>
  <!DOCTYPE html>
<html lang="zh-Hant">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>補助查詢表單</title>
  <link rel="stylesheet" href="styles.css">
</head>
<body>
<!-- Header -->
<div class="header">補助查詢</div>

<div class="form-container">
  <div class="vaccine-timeline-header">
    <h2>疫苗軌跡</h2>
      <div class="filter-section">
        <form id="vaccineSearchForm">
          <select v-model="year">
            <option value="">請選擇年份</option>
            <option value="2024">2024</option>
          </select>

          <select v-model="month">
            <option value="">請選擇月份</option>
            <option value="01">一月</option>
            <option value="02">二月</option>
            <option value="03">三月</option>
            <option value="04">四月</option>
            <option value="05">五月</option>
            <option value="06">六月</option>
            <option value="07">七月</option>
            <option value="08">八月</option>
            <option value="09">九月</option>
            <option value="10">十月</option>
            <option value="11">十一月</option>
            <option value="12">十二月</option>
          </select>

          <div class="button-group">
            <button type="button" class="btn-query" @click="querydata">查詢</button>
            <button type="reset" class="btn-clear">清除</button>
          </div>
        </form>
</div>
    </div>
    <br><br>

    <div class="vaccine-timeline">
    <div v-for="(baby, index) in lists" :key="index"  class="vaccine-entry">
      <div class="vaccine-info">
        <div class="date">{{ baby.VaccinationSchedule }}</div>
        <h3>{{ baby.VaccineNameCN }} ({{ baby.VaccineNameEN }})</h3>

        <!-- <div class="input-section">

          <input
            type="date"
            class="vaccine-date-input"
            v-model="lists[1].Date"
            placeholder="請選擇日期"
          />
          <button class="btn-save">儲存</button>
        </div> -->

        <!-- <p>施打疫苗日期: {{ lists[1].Date }}</p> -->
        <p>1. 疑似結核病人及疑似被結核桿菌感染者，勿直接接種卡介苗。</p>
        <p>2. 發燒、皮膚病、嚴重濕疹、慢性病、免疫機能不全等情況下不可接種。</p>
      </div>
    </div>
  </div>
  </div>
  <!-- Footer -->
  <div class="footer">
    <button id="tab-1" ><a href="subsidy-form.html" style="color: #007B80">補助查詢</a> </button>
    <button id="tab-2" ><a href="vaccine-timeline.html" style="color: #007B80">疫苗軌跡</a></button>
  </div>
</body>
</html>
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
    display: none; /* 初始狀態為隱藏 */
    position: fixed;
    z-index: 1;
    left: 0;
    top: 0;
    width: 100%;
    height: 100%;
    overflow: auto;
    background-color: rgb(0,0,0);
    background-color: rgba(0,0,0,0.4); /* 半透明背景 */
  }
  
  /* 彈窗內容 */
  .modal-content {
    background-color: #fefefe;
    margin: 15% auto; /* 中間對齊 */
    padding: 20px;
    border: 1px solid #888;
    width: 80%; /* 可以根據需要調整大小 */
    border-radius: 8px;
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