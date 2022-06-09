<template>
  <Modal id="keep-modal">
    <template #modal-body>
      <div class="container-fluid position-relative">
        <button
          type="button"
          class="btn-close close-button position-absolute"
          data-bs-dismiss="modal"
          aria-label="Close"
        ></button>
        <div class="row">
          <div class="col-md-6 col-12 p-0">
            <img
              class="object-fit w-100 rounded-start"
              :src="keep.img"
              alt=""
            />
          </div>
          <div class="col-md-6 col-12 p-5">
            <div class="d-flex flex-column justify-content-center ms-3">
              <div title="Views" class="mdi mdi-eye mdi-36px d-flex">
                <h5 class="mt-3 ms-3">{{ keep.views }}</h5>
                <img
                  title="Times Kept"
                  class="keep"
                  src="src/assets/img/Keep.png"
                  alt=""
                />
                <h5 class="mt-3 ms-2">{{ keep.kept }}</h5>
              </div>

              <div
                class="border-bottom border-dark d-flex justify-content-between"
              >
                <h1 class="">{{ keep.name }}</h1>
              </div>
              <p class="mt-3">{{ keep.description }}</p>
            </div>
            <!-- TODO check if in users vault -->
            <div v-if="keep.vaultKeepId">
              <button class="btn btn-outline-primary" @click="remove">
                REMOVE FROM VAULT
              </button>
            </div>
            <div class="text-start" v-else>
              <select
                class="form-control"
                name="vault"
                id="vault"
                v-model="vaultId"
              >
                <option v-for="v in vaults" :key="v.id" :value="v.id">
                  {{ v.name }}
                </option>
              </select>
              <button class="btn btn-outline-success ease-in-out" @click="add">
                ADD TO VAULT
              </button>
            </div>
          </div>
        </div>
      </div>
    </template>
    <!-- TODO check the reference code I sent you - but we will need a dropdown here that displays all of the logged in users vaults. Then I'll leave it up to you to handle the functional side of it.  -->
  </Modal>
</template>


<script>
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState.js'
import Pop from '../utils/Pop.js'
import { logger } from '../utils/Logger.js'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const vaultId = ref(0)

    return {
      vaultId,
      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.profileVaults),

      async add() {
        try {
          const newVk = { keepId: props.keep.id, vaultId: vaultId.value }
          await vaultKeepsService.add(newVk)
          vaultId.value = 0
          Pop.toast("Added")
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      },

      async remove() {
        try {
          if (await Pop.confirm()) {
            await vaultKeepsService.remove(props.keep.vaultKeepId)
            Pop.toast("Removed")
          }
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
.keep {
  inline-size: 4em;
}
</style>