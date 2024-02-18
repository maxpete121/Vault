<template>
<div class="modal fade" id="detailModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h1 v-if="activeKeep !== null" class="modal-title fs-5" id="exampleModalLabel">{{activeKeep.name}}</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <div v-if="activeKeep !== null" class="d-flex">
            <img class="image-resize" :src="activeKeep.img" alt="Keep image.">
            <div class="ms-3">
                <router-link :to="{ name: 'Profile', params: {profileId: activeKeep.creatorId} }">
                    <div @click="getProfileById(activeKeep.creator.id)" type="button" class="d-flex align-items-center mb-2" >
                        <img class="profile-pic rounded-circle" :src="activeKeep.creator.picture" alt="">
                        <h5 class="ms-2 text-dark">{{ activeKeep.creator.name }}</h5>
                    </div>
                </router-link>
                <h6>Description</h6>
                <p>{{ activeKeep.description }}</p>
            </div>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>
</template>


<script>
import { Modal } from 'bootstrap';
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import {profileService} from '../services/ProfileService.js'
export default {
    setup(){
        async function getProfileById(profileId){
            Modal.getOrCreateInstance("#detailModal").hide()
            await profileService.getProfileById(profileId)
        }
    return { 
        getProfileById,
        activeKeep: computed(()=> AppState.activeKeep)
     }
    }
};
</script>


<style lang="scss" scoped>
.image-resize{
    max-width: 300px;
    max-height: 700px;
    box-shadow: 4px 5px 5px rgba(0, 0, 0, 0.393);
}
.profile-pic{
    height: 50px;
    width: 50px;
}
</style>