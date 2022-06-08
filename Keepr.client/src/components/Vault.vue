<template>
  <div class="vault shadow my-5 selectable" @click="goToVault">
    <div class="vaultt">
      <img class="card rounded img-fluid shadow" :src="vault.image" alt="" />
      <h5 class="text-light title">
        {{ vault.name }}
      </h5>
    </div>
  </div>
</template>


<script>
import { useRouter } from 'vue-router'
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
      goToVault() {
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