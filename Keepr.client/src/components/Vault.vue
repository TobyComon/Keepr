<template>
  <div class="vault shadow my-2 selectable" @click="goToVault">
    <div class="vaultt">
      <img
        v-if="vault.image != null"
        class="card rounded img-fluid shadow"
        :src="vault.image"
        alt=""
      />
      <img
        v-else
        class="rounded img-fluid shadow"
        src="https://thiscatdoesnotexist.com"
        alt=""
      />
      <h5 class="text-light title">
        {{ vault.name }}
      </h5>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState.js'
export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    return {
      // vault: computed(() => AppState.profileVaults),
      goToVault() {
        AppState.activeVault = props.vault
        router.push({ name: "Vault", params: { id: props.vault.id } })
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.vaultt {
  position: relative;
  //   text-align: center;
}

.title {
  position: absolute;
  bottom: 8px;
  left: 16px;
}
</style>