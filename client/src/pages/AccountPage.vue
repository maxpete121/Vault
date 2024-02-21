<template>
<section class="container-fluid">
  <div class="row justify-content-center">
    <div :style="coverImg" class="col-10 cover d-flex align-items-end justify-content-center">
      <div class="text-center mb-3 profile-info">
        <img class="rounded-circle img" :src="account.picture" alt="Your user picture.">
        <h4 class="text-light">{{ account.name }}</h4>
      </div>
    </div>
  </div>
  <div class="row justify-content-center mt-2">
    <div class="d-flex justify-content-center">
      <button @click="goToProfile()" class="btn btn-outline-dark">View Profile Page <i class="mdi mdi-account"></i></button>
    </div>
  </div>
  <div class="row mt-2 justify-content-center">
    <div class="col-lg-3 col-11 text-center d-flex flex-column align-items-center edit-card">
      <h4 class="fst-italic">Edit your profile...</h4>
      <form @submit.prevent="updateAccount()" class="d-flex flex-column">
        <div class="d-flex justify-content-between align-items-baseline mt-2 form-field">
          <h5 class="me-2 fst-italic">Name:</h5>
          <input v-model="userData.name" type="text" maxlength="40">
        </div>
        <div class="d-flex justify-content-between align-items-baseline mt-4 form-field">
          <h5 class="me-2 fst-italic">Profile Picture:</h5>
          <input v-model="userData.picture" type="text" maxlength="500">
        </div>
        <div class="d-flex justify-content-between align-items-baseline mt-4 form-field">
          <h5 class="me-2 fst-italic">Cover Image:</h5>
          <input v-model="userData.coverImg" type="text" maxlength="500">
        </div>
        <div class="mt-3">
          <button name="save button" class="btn btn-outline-dark">Save</button>
        </div>
      </form>
    </div>
  </div>
</section>
</template>

<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState';
import {accountService} from '../services/AccountService.js'
import { router } from '../router';
export default {
  setup() {
    let useAccount = computed(()=> AppState.account)
    let userData = ref({})

    async function goToProfile(){
      router.push({ name: 'Profile', params: {profileId: useAccount.value.id}})
    }

    async function updateAccount(){
      if(window.confirm('Do you want to save these changes?')){
        await accountService.updateAccount(userData.value)
        userData.value = {}
      }
    }
    return {
      goToProfile,
      updateAccount,
      userData,
      coverImg: computed(()=>{
        let style = `background-image: url('${useAccount.value.coverImg}');`
        return style
      }),
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
img {
  width: 100px;
  height: 100px;
  outline: solid 2px black;
}
.cover{
  height: 370px;
  background-position: center;
  background-size: cover;
}

.edit-card{
  outline: solid 1px rgb(95, 4, 121);
  background-color: white;
  border-radius: 10px;
  padding-bottom: 20px;
  padding-top: 10px;
}

.form-field{
  border-bottom: dashed 2px rgb(95, 4, 121);
}

.profile-info{
  text-shadow: 2px 2px 2px black;
}
</style>
