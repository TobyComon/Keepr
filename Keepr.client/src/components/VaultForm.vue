<template>
  <div class="container">
    <form @submit.prevent="create" id="vaultForm" class="text-center">
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
        />
      </div>
      <div class="text-start mb-3 form-check">
        <label for="isPrivate" class="form-label"
          >Private? <br />
          <figure class="text-start mt-3">
            <figcaption class="blockquote-footer">
              <cite title="Private Vaults can only be seen by you "
                >Private Vaults can only be seen by you
              </cite>
            </figcaption>
          </figure>
        </label>

        <input
          type="checkbox"
          class="me-4 form-check-input"
          name="isPrivate"
          id="isPrivate"
          v-model="editable.isPrivate"
        />
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
import { vaultsService } from '../services/VaultsService.js';
import { Modal } from 'bootstrap';
export default {
  setup() {
    const editable = ref({});

    return {
      editable,
      async create() {
        try {
          await vaultsService.create(editable.value)
          Modal.getOrCreateInstance(document.getElementById("newVaultModal")).toggle();

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