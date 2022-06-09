<template>
  <div class="container">
    <!-- TODO make sure to set up deleting a vaultKeep here.  -->
    <!-- <h1>{{ vault.name }}</h1> -->
    <div class="masonry-with-columns">
      <div v-for="k in vaultKeeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core';
import { useRoute, useRouter } from 'vue-router'
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { keepsService } from '../services/KeepsService.js';
import { AppState } from '../AppState.js';
import { vaultsService } from '../services/VaultsService.js';
export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    onMounted(async () => {
      try {
        //   await vaultsService
        await keepsService.getVaultKeeps(route.params.id);
        // TODO - Once your tests are passing and your server rejects a user trying to access another users private vaults, Think about what the catch below is doing. What is it that you want to do with the user if this request fails?
      } catch (error) {
        router.push()
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })
    return {
      vaultKeeps: computed(() => AppState.vaultKeeps)
    }
  }
}
</script>


<style lang="scss" scoped>
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