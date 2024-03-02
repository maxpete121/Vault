<template>
<div class="modal fade" id="detailModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h1 v-if="activeKeep !== null" class="modal-title fs-5" id="exampleModalLabel">{{activeKeep.name}}</h1>
        <button @click="clearActive()" type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
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
          <div class="d-flex flex-column align-items-center container-fit">
            <router-link :to="{ name: 'Profile', params: {profileId: activeKeep.creatorId} }">
                <div @click="getProfileById(activeKeep.creator.id)" type="button" class="d-flex justify-content-center align-items-center mb-2" >
                    <img class="profile-pic rounded-circle" :src="activeKeep.creator.picture" alt="User picture.">
                    <h5 class="ms-2 text-dark">{{ activeKeep.creator.name }}</h5>
                </div>
            </router-link>
            <img class="image-resize" :src="activeKeep.img" alt="Keep image.">
          </div>
            <div class="mt-2 d-flex flex-column align-items-center justify-content-between container-fit-two">
              <div class="top-info w-100">
                <div class="d-flex justify-content-center">
                  <h4 class="fst-italic">{{ activeKeep.name }}</h4>
                </div>
                <div class="ms-2 mt-4">
                  <h5 class="fst-italic">Description</h5>
                  <p>{{ activeKeep.description }}</p>
                </div>
              </div>
              <div class="mb-2 text-center mt-3 d-flex flex-column align-items-center w-100">
                <div class="d-flex tag-style justify-content-center">
                  <h5 class="fst-italic me-2">Tags</h5>
                  <h5 class="" v-if="account.id == activeKeep.creatorId">{{ tags.length }}/10</h5>
                </div>
                <div v-if="account.id == activeKeep.creatorId && account.id">
                  <form @submit.prevent="tagCheck()" class="d-flex align-items-center" action="">
                    <input v-model="tagData" class="tags-input" type="text" maxlength="30" required>
                    <button class="tag-button">Add a Tag</button>
                  </form>
                </div>
                <div class="w-100 ps-2 pe-2 mt-2 tag-holder">
                  <TagsComponent v-for="tag in tags" class="m-1" :tag="tag"/>
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
import {tagsService}from '../services/TagsService.js';
import TagsComponent from './TagsComponent.vue';
export default {
    setup(){
      let useActiveKeep = computed(()=> AppState.activeKeep)
      let useAccount = computed(()=> AppState.account)
      let useTag = computed(()=> AppState.activeTag)
      let vaultData = ref('')
      let tagData = ref('')
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

        async function tagCheck(){
          let exists = 'no'
          let newTagLower = tagData.value
          let newTag = newTagLower.toUpperCase()
          for(let i = 0; AppState.activeTag.length > i; i++){
            let string = AppState.activeTag[i].name
            let testString = string.toUpperCase()
            if(testString == newTag){
              exists = 'yes'
              Pop.error('This tag already exists.')
            }
          }
          if(exists !== 'yes'){
            createTag()
          }
        }

        async function createTag(){
          let tagName = tagData.value
          let isMade = AppState.activeTag.find(tag => tag.name == tagName)
          if(useTag.value.length < 10 && !isMade){
            let newTagData = {keepId: useActiveKeep.value.id, creatorId: useAccount.value.id, name: tagData.value}
            await tagsService.createTag(newTagData)
            tagData.value = ''
          }else if(useTag.value.length == 10){
            Pop.error('This keep has the maximum amount of tags.')
          }else if(isMade && string == tagName){
            Pop.error('This tag already exists.')
          }
        }
    return { 
      tagCheck,
      createTag,
      clearActive,
      deleteKeep,
      createVaultKeep,
      tagData,
        vaultData,
        getProfileById,
        activeKeep: computed(()=> AppState.activeKeep),
        vaults: computed(()=> AppState.userVaults),
        yourVaults: computed(()=> AppState.addToVault),
        account: computed(()=> AppState.account),
        tags: computed(()=> AppState.activeTag),
     }
    },};
</script>


<style lang="scss" scoped>
.top-info{
  border-bottom: solid 2px black;
}
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
.tag-container-main{
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

.tag-button:focus{
  all: unset;
  outline: solid 2px black;
  border-top-right-radius: 10px;
  border-bottom-right-radius: 10px;
  padding-left: 7px;
  padding-right: 7px;
  padding-bottom: 2px;
  padding-top: 2px;
  cursor: pointer;
}

.tag-button:hover{
  all: unset;
  outline: solid 2px black;
  border-top-right-radius: 10px;
  border-bottom-right-radius: 10px;
  padding-left: 7px;
  padding-right: 7px;
  padding-bottom: 2px;
  padding-top: 2px;
  cursor: pointer;
}

.container-fit-two{
  width: 50%;
}

@media screen and (min-width: 576px){
  .container-fit{
  width: 50%;
}
.container-fit-two{
  width: 50%;
}
}
@media screen and (max-width: 576px){
  .container-fit{
  width: 100%;
}
.container-fit-two{
  width: 100%;
}
}

.tag-style{
  border-bottom: solid 2px black;
  margin-bottom: 5px;
}
</style>