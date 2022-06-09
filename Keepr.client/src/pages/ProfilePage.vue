<template>
  <div class="profile container">
    <div class="row mt-3">
      <div class="col-md-6 d-flex">
        <img class="rounded-circle" :src="profile.picture" alt="" />
        <div class="d-flex flex-column justify-content-center ms-2">
          <h1 class="m-0">{{ profile.name }}</h1>
          <h4 class="m-0">Keeps: {{ keepCount }}</h4>
          <h4 class="m-0">Vaults: {{ vaultCount }}</h4>
        </div>
      </div>
    </div>
  </div>

  <div class="container-fluid">
    <h1>Vaults:</h1>
    <div v-for="v in vaults" :key="v.id">
      <Vault :vault="v" />
    </div>
  </div>

  <div class="container">
    <h1>Keeps:</h1>
    <i class="mdi mdi-plus mdi-48px" @click="openModal"></i>
    <Modal id="newKeepModal">
      <template #modal-body>
        <KeepForm />
      </template>
    </Modal>
    <div class="masonry-with-columns">
      <div v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import Pop from '../utils/Pop.js'
import { logger } from '../utils/Logger.js'
import { AppState } from '../AppState.js'
import { profilesService } from '../services/ProfilesService.js'
import { Modal } from 'bootstrap'

export default {
  setup() {
    const route = useRoute();
    onMounted(async () => {
      try {
        await profilesService.getProfile(route.params.id);
      }
      catch (error) {
        logger.log("Profile..", error);
        Pop.toast(error.message, "error");
      }
    });
    return {
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.profileKeeps),
      vaults: computed(() => AppState.profileVaults),
      keepCount: computed(() => AppState.profileKeeps.length),
      vaultCount: computed(() => AppState.profileVaults.length),
      openModal() {

        Modal.getOrCreateInstance(document.getElementById("newKeepModal")).toggle();
      }
    };
  },

}
</script>


<style lang="scss" scoped>
body {
  margin: 0;
  padding: 1rem;
}

.masonry-with-columns {
  columns: 4 200px;
  column-gap: 1rem;
  div {
    width: 150px;
    // background: #ec985a;
    // color: white;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
    text-align: center;
    background-repeat: no-repeat;
    // font-family: system-ui;
    // font-weight: 900;
    // font-size: 2rem;
  }
  @for $i from 2 through 36 {
    div:nth-child(#{$i}) {
      $h: (random(400) + 100) + px;
      height: $h;
      line-height: $h;
    }
  }
}
</style>