<script setup lang="ts">
import { computed } from 'vue';
import { ref } from 'vue';
import qnaListJson from '../../public/mock/qna_list.json';


</script>

<template>
 <div></div>>
</template>

<style lang="postcss">
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

.tab {
  @apply bg-white border border-grey-400 text-grey-400 text-center py-2 px-4 rounded-xl rounded-bl-none;
  &--active {
    @apply bg-primary-500 text-white border-primary-500;
  }
}
</style>
