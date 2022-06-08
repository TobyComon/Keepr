<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
    <KeepModal />
  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import KeepModal from './components/KeepModal.vue'
export default {
  name: "App",
  setup() {
    return {
      appState: computed(() => AppState)
    };
  },
  components: { KeepModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
</style>
