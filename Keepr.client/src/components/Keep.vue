<template>
  <div class="keep text-white my-5 selectable">
    <div class="keepp">
      <img
        @click="setActive"
        class="card rounded img-fluid shadow"
        :src="keep.img"
        alt=""
      />
      <h5 class="text-light title">
        {{ keep.name }}
      </h5>
      <img
        @click="goToProfile"
        class="pfp selectable"
        :src="keep.creator.picture"
        alt=""
      />
    </div>
  </div>
</template>


<script>
import { Modal } from 'bootstrap'
import { AppState } from '../AppState.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
import { computed } from '@vue/reactivity'
import { useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService.js'
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },

  setup(props) {
    const router = useRouter()
    return {
      goToProfile() {
        Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
        router.push({ name: 'Profile', params: { id: props.keep.creatorId } })
      },
      setActive() {
        try {
          // TODO this is a valid way of handling setting up your keep modal but it makes updating the view count a little trickier. If you switch this to a get keep by Id api call, then you will need to adjust your SQL statement like we discussed
          // AppState.activeKeep = props.keep
          keepsService.getKeepById(props.keep.id)
          Modal.getOrCreateInstance(document.getElementById('keep-modal')).show()
          // EDIT KEEP api call - and it's main purpose is to update view count. AppState.activeKeep.views++ THEN call edit keep
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
.keepp {
  position: relative;
  text-align: center;
}

.title {
  position: absolute;
  bottom: 8px;
  left: 16px;
}

.pfp {
  position: absolute;
  bottom: 8px;
  right: 16px;
  border-radius: 50%;
  max-inline-size: 50px;
  cursor: pointer;
}
</style>