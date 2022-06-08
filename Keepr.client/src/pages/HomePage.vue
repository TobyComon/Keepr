<template>
  <div class="container">
    <div class="masonry-with-columns">
      <div v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, watchEffect } from '@vue/runtime-core'
import Pop from '../utils/Pop.js'
import { logger } from '../utils/Logger.js'
import { keepsService } from '../services/KeepsService.js'
import { AppState } from '../AppState.js'
export default {
  setup() {
    watchEffect(async () => {
      try {
        await keepsService.getAll()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    })

    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
// .masonry-container {
//   --gap: 17em;
//   --columns: 4;
//   // columns: 4;
//   // column-gap: 20rem;
//   max-width: 60rem;
//   // margin: 0 auto;
//   // display: column;
//   // gap: var(--gap);
//   columns: var(--columns);

//   div {
//     break-inside: avoid;

//     // width: 150px;
//     // margin: 0 1rem 1rem 0;
//     display: flex;
//     flex-direction: column;
//     flex-wrap: wrap;
//     // flex-grow: 1;

//     // text-align: center;
//   }
// }

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
