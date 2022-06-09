<template>
  <div class="container">
    <form @submit.prevent="create" id="keepForm" class="text-center">
      <div class="mb-3">
        <label for="name" class="form-label"></label>
        <input
          type="text"
          class="form-control"
          name="name"
          id="name"
          placeholder="Title..."
          v-model="editable.name"
          required
        />
      </div>
      <div class="mb-3">
        <label for="img" class="form-label"></label>
        <input
          type="text"
          class="form-control"
          name="img"
          id="img"
          aria-describedby="helpId"
          placeholder="URL..."
          v-model="editable.img"
          required
        />
      </div>
      <div class="mb-3">
        <label for="description" class="form-label"></label>

        <textarea
          name="description"
          id="description"
          cols="30"
          rows="10"
          v-model="editable.description"
        ></textarea>
      </div>
      <button class="btn btn-success" type="submit">Submit</button>
    </form>
  </div>
</template>


<script>
import { ref } from '@vue/reactivity'
import { useRouter } from 'vue-router';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { keepsService } from '../services/KeepsService.js';
export default {
  setup() {
    const editable = ref({});

    return {
      editable,
      async create() {
        try {
          await keepsService.create(editable.value)

        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>