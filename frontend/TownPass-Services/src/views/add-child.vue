<script setup lang="ts">
import { computed, ref } from 'vue';
import { db } from '../../firebaseConfig';
import { collection, setDoc, doc } from 'firebase/firestore';

// 表單資料模型
const Baby123 = ref({
  Birthday :'',
  SubsidyName: '' , // 初始化為空字串以顯示「請選擇」
  Daycare: '',
  BirthOrder:'1',
  Age:"1",
  Subsidies:{
    Details: [],
    Amount: ''
  }
});

// Firestore 的 "Childe" collection 參考
const childeCollection = collection(db, 'Baby');

// 表單提交處理
const addChild = async () => {
  try {
    if (!Baby123.value.Birthday  || !Baby123.value.SubsidyName|| !Baby123.value.Daycare|| !Baby123.value.BirthOrder||!Baby123.value.Age) {
      alert('請填寫所有欄位');
      return;
    }

    // 新增文件到 Firestore
    const docRef = doc(childeCollection);
    await setDoc(docRef, Baby123.value);
    
    alert('資料新增成功');
    // 清空表單
    Baby123.value.Birthday  = '';
    goHome();
  } catch (error) {
    console.error('新增資料錯誤:', error);
    alert('新增資料失敗');
  }
};

// 頁面切換
const showPage = (page: number) => {
  console.log(`切換到頁面 ${page}`);
};

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


const ResetAll = () =>{
  Baby123.value.Birthday  ='';
  Baby123.value.BirthOrder ='';
  Baby123.value.SubsidyName ='';
  Baby123.value.Daycare ='';
}


const ResetDaycareVali = () =>{

  const label = document.querySelector('.Daycare');
  if (label) {
  label.classList.add('vali');
} else {
  console.error('Element with class "Daycare" not found');
}
}

const ResetDateVali = () =>{

const label = document.querySelector('.birthvali');
if (label) {
  label.classList.add('vali');
} else {
  console.error('Element with class "Daycare" not found');
}
}

const ResetDaycare = () =>{
  Baby123.value.Daycare = '';
  const label = document.querySelector('.Subsidyvali');
  if (label) {
  label.classList.add('vali');
} else {
  console.error('Element with class "Daycare" not found');
}
}

/// 處理日期為 YYYY-MM-DD 格式的計算屬性
const formattedBirth = computed({
  get() {
    // 如果有日期，將它轉換為 YYYY-MM-DD 格式
    if (Baby123.value.Birthday ) {
      const date = new Date(Baby123.value.Birthday );
      const year = date.getFullYear();
      const month = String(date.getMonth() + 1).padStart(2, '0'); // 月份補零
      const day = String(date.getDate()).padStart(2, '0'); // 天數補零
      return `${year}-${month}-${day}`; // 返回 YYYY-MM-DD 格式
    }
    return ''; // 沒有日期時返回空
  },
  set(value: string) {
    // 當用戶選擇日期時，將其保存為 YYYY-MM-DD 格式的字符串
    Baby123.value.Birthday  = value;
  }
});
import { useRouter } from 'vue-router';
const router = useRouter();
const goHome = () => {
  router.push('/');
};
</script>

<template>
  <div>
    <!-- Header -->
    <div class="header index">
      <RouterLink to="/">
      <img src="/image/arrow.png" alt="arrow" />
    </RouterLink>
      <span>新增孩童</span>
      <div class="space"></div>
    </div>

    <!-- main -->
    <div class="form-container">
      <form id="queryForm">
        <!-- 出生日期 -->
        <label for="Birth">出生日期</label>
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
      </form>
      <div class="button-group">
        <button type="button" class="btn-query" @click="addChild">新增</button>
        <button type="reset" class="btn-clear" @click="goHome">取消</button>
      </div>
    </div>

    <!-- Footer -->
    <div class="footer">
      <button id="tab-1" class="active" @click="showPage(1)">補助查詢</button>
      <button id="tab-2" @click="showPage(2)">疫苗軌跡</button>
    </div>
  </div>
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
