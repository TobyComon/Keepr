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
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState.js'
export default {
  setup() {

    return {
      keep: computed(() => AppState.activeKeep),

    }
  }
}
</script>


<style lang="scss" scoped>
.keep {
  inline-size: 4em;
}
</style>