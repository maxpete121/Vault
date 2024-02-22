<template>
<div class="modal fade" id="detailModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h1 v-if="activeKeep !== null" class="modal-title fs-5" id="exampleModalLabel">{{activeKeep.name}}</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <div class="d-flex justify-content-center mb-4">
          <div class="stat-container d-flex align-items-center">
            <div v-if="activeKeep !== null" class="d-flex stat-child-view pt-1 pe-2 ps-2 align-items-center">
              <h6 class="fst-italic">Views</h6>
              <h6 class="ms-2">{{ activeKeep.views }}</h6>
            </div>
            <div v-if="activeKeep !== null" class="d-flex stat-child-kept pt-1 pe-2 ps-2 align-items-center">
              <h6 class="fst-italic">K</h6>
              <h6 class="ms-2">{{ activeKeep.kept }}</h6>
            </div>
          </div>
        </div>
        <div v-if="activeKeep !== null" class="d-lg-flex">
          <div class="d-flex flex-column align-items-center">
            <router-link :to="{ name: 'Profile', params: {profileId: activeKeep.creatorId} }">
                <div @click="getProfileById(activeKeep.creator.id)" type="button" class="d-flex justify-content-center align-items-center mb-2" >
                    <img class="profile-pic rounded-circle" :src="activeKeep.creator.picture" alt="User picture.">
                    <h5 class="ms-2 text-dark">{{ activeKeep.creator.name }}</h5>
                </div>
            </router-link>
            <img class="image-resize" :src="activeKeep.img" alt="Keep image.">
          </div>
            <div class="mt-2 d-flex flex-column align-items-center justify-content-between">
              <div>
                <div class="d-flex justify-content-center">
                  <h4 class="fst-italic">{{ activeKeep.name }}</h4>
                </div>
                <div class="ms-4 mt-4">
                  <h5 class="fst-italic">Description</h5>
                  <p>{{ activeKeep.description }}</p>
                </div>
              </div>
              <div class="mb-2 text-center">
                <h5 class="fst-italic">Tags</h5>
                <div v-if="account.id == activeKeep.creatorId && account.id">
                  <form action="">
                    <input class="tags-input" type="text" maxlength="30">
                    <button class="tag-button">Add a Tag</button>
                  </form>
                </div>
                <div>
                  <div v-for="tag in tags"></div>
                </div>
              </div>
            </div>
        </div>
      </div>
      <div class="modal-footer">
        <div v-if="activeKeep !== null && account.id">
          <form @submit.prevent="createVaultKeep()" action="">
            <div class="d-flex add-keep">
              <select v-model="vaultData" class="form-control w-50 me-lg-2" name="" id="">
                <option v-for="yourVault in yourVaults" :value="yourVault.id">{{yourVault.name}}</option>
              </select>
              <button class="btn btn-secondary">Add to Vault</button>
            </div>
          </form>
        </div>
        <div v-if="activeKeep !== null"> 
          <button @click="deleteKeep()" v-if="account.id == activeKeep.creatorId" class="btn btn-danger">Delete</button>
        </div>
        <button @click="clearActive()" type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>
</template>


<script>
import { Modal } from 'bootstrap';
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import {profileService} from '../services/ProfileService.js';
import {vaultKeepService} from '../services/VaultKeepService.js';
import {keepService} from '../services/KeepService.js'
import Pop from '../utils/Pop';
export default {
    setup(){
      let useActiveKeep = computed(()=> AppState.activeKeep)
      let vaultData = ref('')
        async function getProfileById(profileId){
            Modal.getOrCreateInstance("#detailModal").hide()
            await profileService.getProfileById(profileId)
        }

        async function clearActive(){
          AppState.activeKeep = null
        }

        async function createVaultKeep(){
          await vaultKeepService.createVaultKeep(vaultData.value, useActiveKeep.value.id)
          await keepService.updateKept(useActiveKeep.value.id)
          await keepService.getKeepById(useActiveKeep.value.id)
          vaultData.value = ''
          Pop.success('Added to vault.')
        }

        async function deleteKeep(){
          if(window.confirm(`Are you sure you want to delete this vault?`)){
            await keepService.deleteKeep(useActiveKeep.value.id)
            Modal.getOrCreateInstance("#detailModal").hide()
            clearActive()
          }
        }
    return { 
      clearActive,
      deleteKeep,
      createVaultKeep,
        vaultData,
        getProfileById,
        activeKeep: computed(()=> AppState.activeKeep),
        vaults: computed(()=> AppState.userVaults),
        yourVaults: computed(()=> AppState.yourVaults),
        account: computed(()=> AppState.account),
        tags: computed(()=> AppState.activeTag),
     }
    }
};
</script>


<style lang="scss" scoped>
.add-keep{
  width: 300px;
}
.stat-container{
  outline: solid 1px black;
  border-radius: 15px;
}
.stat-child-view{
  border-right: solid 1px black;
}
.image-resize{
    max-width: 300px;
    max-height: 700px;
    box-shadow: 4px 5px 5px rgba(0, 0, 0, 0.393);
}
.profile-pic{
    height: 50px;
    width: 50px;
}
.tag-container{
  background-color: whitesmoke;
  outline: solid 1px black;
}

.tags-input{
  all: unset;
  border-top-left-radius: 10px;
  border-bottom-left-radius: 10px;
  padding-top: 3px;
  padding-bottom: 3px;
  padding-left: 10px;
  border: solid 1px black;
  background-color: white;
  margin-right: 1px;
}
.tags-input:focus{
  all: unset;
  border-top-left-radius: 10px;
  border-bottom-left-radius: 10px;
  padding-top: 3px;
  padding-bottom: 3px;
  padding-left: 10px;
  border: solid 1px black;
  background-color: whitesmoke;
  margin-right: 1px;
}
.tag-button{
  all: unset;
  outline: solid 2px black;
  border-top-right-radius: 10px;
  border-bottom-right-radius: 10px;
  padding-left: 7px;
  padding-right: 7px;
  padding-bottom: 2px;
  padding-top: 2px;
}

</style>