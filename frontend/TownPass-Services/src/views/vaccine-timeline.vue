<script setup lang="ts">
import { computed, ref } from 'vue';
import { RouterLink, useRoute, useRouter } from 'vue-router';
import { storeToRefs } from 'pinia';
import { useCouponStore } from '@/stores/coupon';
import { useConnectionMessage } from '@/composables/useConnectionMessage';
import { useHandleConnectionData } from '@/composables/useHandleConnectionData';
import BaseDialog from '@/components/atoms/BaseDialog.vue';
import CouponResultItem from '@/components/molecules/CouponResultItem.vue';
import { vaccines } from '@/zipcode/vaccine';
import type { IBaby, ICareType, IVaccineTracking, ISubsidy, IBirthOrderSubsidy, IDaycareSubsidy, ISubsidyDetail } from '@/ts/baby';  
// 將需要的 class 從 babyClasses.ts 中引入


const store = useCouponStore();
const route = useRoute();
const router = useRouter();


</script>

<template>
  <div class="vaccine-timeline">
  <div class="vaccine-entry">
    <div class="date" v-model=""></div>
    <div class="vaccine-info">
      <h3>B 型肝炎疫苗 (HepB)</h3>
      <p>施打疫苗日期: 2024/09/06</p>
      <p>1. 疑似結核病人及疑似被結核桿菌感染者，勿直接接種卡介苗。</p>
      <p>2. 發燒、皮膚病、嚴重濕疹、慢性病、免疫機能不全等情況下不可接種。</p>
    </div>
  </div>

  <div class="vaccine-entry">
    <div class="date">2024/09/24</div>
    <div class="vaccine-info">
      <h3>卡介苗 (BCG)</h3>
      <p>施打疫苗日期: 2024/09/24</p>
      <p>1. 疑似結核病人及疑似被結核桿菌感染者，勿直接接種卡介苗。</p>
      <p>2. 發燒、皮膚病、嚴重濕疹、慢性病、免疫機能不全等情況下不可接種。</p>
    </div>
  </div>

  <div class="vaccine-entry">
    <div class="date">2024/10/10</div>
    <div class="vaccine-info">
      <h3>B 型肝炎疫苗 (HepB)</h3>
      <p>施打疫苗日期: 2024/10/10</p>
      <p>1. 疑似結核病人及疑似被結核桿菌感染者，勿直接接種卡介苗。</p>
      <p>2. 發燒、皮膚病、嚴重濕疹、慢性病、免疫機能不全等情況下不可接種。</p>
    </div>
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
