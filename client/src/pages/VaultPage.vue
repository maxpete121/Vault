<template>
    <section class="container-fluid">
        <div class="row justify-content-center mt-4">
            <div class="col-5 d-flex flex-column align-items-center">
                <img class="img-resize" :src="activeVault.img" alt="Vault image.">
                <h4 class="fst-italic mt-2">{{ activeVault.name }}</h4>
            </div>
        </div>
        <div class="row justify-content-center">
            <div class="col-2 text-center">
                <h4>Keeps</h4>
            </div>
        </div>
        <div class="keep-container">
            <div class="m-2" v-for="keep in keeps">
            <KeepComponentCard :keep="keep"/>
            </div>
        </div>
    </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { vaultService } from '../services/VaultService';
import { useRoute } from 'vue-router';
import KeepComponentCard from '../components/KeepComponentCard.vue';
export default {
    setup(){
        let route = useRoute()
        onMounted(()=>{
            vaultService.getVaultById(route.params.vaultId)
            getVaultKeeps()
        })

        async function getVaultKeeps(){
            await vaultService.getVaultKeeps(route.params.vaultId)
        }
    return { 
        activeVault: computed(()=> AppState.activeVault),
        keeps: computed(()=> AppState.vaultsKeeps)
     }
    }, components: { KeepComponentCard }
};
</script>


<style lang="scss" scoped>
.img-resize{
    max-height: 300px;
}
.keep-container{
    columns: 300px;
}
</style>