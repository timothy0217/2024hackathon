<script setup lang="ts">
import { ref, watch } from 'vue';
import { storeToRefs } from 'pinia';
import ServiceStep from '@/components/molecules/ServiceStep.vue';
import GeneralInfoForm from '@/components/molecules/GeneralInfoForm.vue';
import ApplyServiceInfoForm from '@/components/molecules/ApplyServiceInfoForm.vue';
import BaseButton from '@/components/atoms/BaseButton.vue';
import FormPreview from '@/components/organisms/FormPreview.vue';
import type { BasicForm } from '@/components/molecules/GeneralInfoForm.vue';
import { useFormStore } from '@/stores/form';
import { useUserStore } from '@/stores/user';
import formJSON from '../../public/mock/form.json';



</script>

<template>
 <div></div>
</template>
