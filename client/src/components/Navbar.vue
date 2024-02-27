<template>
  <nav class="navbar navbar-expand-sm navbar-dark bg-light px-3 new-nav">
    <div class="d-lg-flex align-items-center justify-content-lg-between w-100">
      <div class="d-flex align-items-center justify-content-between">
        <div class="d-flex align-items-center">
          <img class="logo" src="../assets/img/keepr-co-high-resolution-logo-transparent.png" alt="Logo">
        </div>
        <router-link class="navbar-brand d-flex justify-content-center" :to="{ name: 'Home' }">
          <div v-if="account.id" @click="getAllKeeps()" class="me-2 d-flex align-items-center">
            <h4 class="text-dark">Home</h4>
          </div>
          <div v-else @click="getAllKeeps()" class="text-center ms-3 ms-lg-1 me-2 d-flex align-items-center">
            <h4 class="text-dark">Home</h4>
          </div>
        </router-link>
        <div v-if="account.id" class="dropdown">
          <div class="d-flex align-items-center me-4" type="button" data-bs-toggle="dropdown">
            <h4 class="text-dark d-flex align-items-center">Create</h4>
            <i class="mdi mdi-arrow-down-drop-circle ms-1"></i>
          </div>
          <div class="dropdown-menu dropdown-menu-sm-start dropdown-menu-end p-0" aria-labelledby="authDropdown">
            <div class="list-group">
              <div class="list-group-item dropdown-item list-group-item-action">
                <h6 type="button" class="" data-bs-toggle="modal" data-bs-target="#createModal">Create Keep</h6>
              </div>
              <div class="list-group-item dropdown-item list-group-item-action selectable">
                <h6 type="button" class="" data-bs-toggle="modal" data-bs-target="#createVModal">Create Vault</h6>
              </div>
            </div>
          </div>
        </div>
        <Login class="login-small"/>
      </div>
      <div class="d-flex align-items-lg-center justify-content-center">
        <SearchbarComponent class="me-lg-4"/>
        <Login class="login-large"/>
      </div>
    </div>
  </nav>
  <ModalWrapper id="createModal" />
  <ModalWrapper id="createVModal" />
</template>

<script>
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import ModalWrapper from './ModalWrapper.vue';
import { AppState } from '../AppState';
import SearchbarComponent from './SearchbarComponent.vue';
import { keepService } from '../services/KeepService';
export default {
  setup() {

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    async function getAllKeeps() {
      await keepService.getAllKeeps()
    }

    return {
      getAllKeeps,
      theme,
      account: computed(() => AppState.account),
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      }
    }
  },
  components: { Login, ModalWrapper, SearchbarComponent }
}
</script>

<style scoped>
.logo {
  height: 55px;
  width: 90px;
}

.new-nav {
  border-bottom: solid 1px black;
  box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.301);
}

a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 576px) {
  nav {
    height: 64px;
  }
  .login-small{
    display: none;
  }
  .login-large{
    display: inline;
  }
}
@media screen and (max-width: 576px) {
  .login-small{
    display: inline;
  }
  .login-large{
    display: none;
  }
}
</style>
