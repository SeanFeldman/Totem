<template>
  <ModalWindow
    ref="buildContractFromMessageModalWindow"
    title="Import Contract From Message"
    :success-btn="{
      id: 'importContract',
      text: 'Import Contract',
      clicked: importContract
    }"
    :cancel-btn="{
      id: 'cancelBtn',
      text: 'Cancel',
      clicked: close
    }"
  >
    <template v-slot:body>
      <textarea id="import-message" v-model="message" class="form-control tall mono" />
    </template>
  </ModalWindow>
</template>

<script>
import $ from 'jquery';
import ModalWindow from '../../components/ModalWindow.vue';
import { isValidJSON } from './dataHelpers';

export default {
  name: 'BuildContractFromMessageModalWindow',
  components: {
    ModalWindow
  },
  props: {
    title: { type: String, default: '' }
  },
  data() {
    return {
      message: ''
    };
  },
  methods: {
    importContract() {
      if (isValidJSON($('#import-message')[0].value)) {
        this.$emit('importContract');
      } else {
        const $validationField = $(`#contract-string-validation`);
        $validationField.html('Invalid JSON');
        this.$emit('close');
      }
    },
    close() {
      $('#import-message')[0].value = '';
      this.$emit('close');
      $('#import-message').scrollTop(0);
    }
  }
};
</script>
