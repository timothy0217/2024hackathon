<script setup lang="ts">
import { computed } from 'vue';
import { ref } from 'vue';
import qnaListJson from '../../public/mock/qna_list.json';
import BaseButton from '@/components/atoms/BaseButton.vue';
import { useRouter } from 'vue-router';

const router = useRouter();


</script>

<template>
  <div></div>>
</template>

<style lang="postcss">
.qna-wrapper {
  background-image: url('@/assets/images/bg-one.svg');
  background-size: 80%;
  background-position: -150% 0;
}

.title {
  position: relative;
  display: inline-block;
}

.title::after {
  content: '';
  display: block;
  width: 20%;
  height: 3px; /* 底線的粗細 */
  background-color: currentColor;
  position: absolute;
  left: 0;
  bottom: -5px; /* 根據需要調整底線與標題之間的距離 */
}
</style>
