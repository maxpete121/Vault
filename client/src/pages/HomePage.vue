<template>
  <section class="container-fluid">
    <div class=" justify-content-center container-main mt-3">
      <div class="m-2" v-for="keep in keeps">
        <KeepComponentCard :keep="keep"/>
      </div>
    </div>
  </section>
</template>

<script>
import { computed, onMounted } from 'vue'
import {vaultService} from '../services/VaultService.js'
import {keepService} from '../services/KeepService.js'
import { AppState } from '../AppState'
import KeepComponentCard from '../components/KeepComponentCard.vue'
export default {
  setup() {
    onMounted(()=>{
      getAllKeeps()
    })
    async function getAllKeeps(){
      await keepService.getAllKeeps()
    }
    return {
      keeps: computed(()=> AppState.allKeeps)
    }
  }, components: {KeepComponentCard}
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

.container-main{
  columns: 300px;
}
</style>
