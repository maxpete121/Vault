<template>
  <nav class="navbar navbar-expand-sm navbar-dark bg-light px-3 new-nav align-items-center d-flex">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div>
        <h4 class="text-dark">Home</h4>
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse align-self-center" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <div class="dropdown">
            <h5 class="text-dark" type="button" data-bs-toggle="dropdown">Create</h5>
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
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <div>
        <button class="btn text-light" @click="toggleTheme">
          <i class="mdi" :class="theme == 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'"></i>
        </button>
      </div>
      <Login />
    </div>
  </nav>
  <ModalWrapper id="createModal"/>
  <ModalWrapper id="createVModal"/>
</template>

<script>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import ModalWrapper from './ModalWrapper.vue';
export default {
  setup() {

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      theme,
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      }
    }
  },
  components: { Login, ModalWrapper }
}
</script>

<style scoped>
.new-nav{
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
}
</style>
