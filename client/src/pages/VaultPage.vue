<template>
    <section class="container-fluid">
        <div class="row justify-content-center mt-4">
            <div class="col-5 d-flex flex-column align-items-center">
                <img class="img-resize" :src="activeVault.img" alt="Vault image.">
                <h4 class="fst-italic mt-2">{{ activeVault.name }}</h4>
            </div>
        </div>
        <div v-if="activeVault.creatorId == account.id" class="row justify-content-center">
            <div class="col-4 d-flex justify-content-center">
                <button v-if="activeVault.isPrivate == false" class="btn btn-outline-dark">Make Private</button>
                <button v-if="activeVault.isPrivate == true" class="btn btn-outline-dark">Make Public</button>
                <button class="btn btn-outline-danger ms-2">Delete</button>
            </div>
        </div>
        <div class="row justify-content-center mt-2">
            <div class="col-2 text-center">
                <h4>Keeps</h4>
            </div>
        </div>
        <div class="row justify-content-center">
            <div class="col-3 mt-2" v-for="keep in keeps">
            <VaultKeepCard :keeper="keep"/>
            </div>
        </div>
    </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { vaultService } from '../services/VaultService';
import { useRoute } from 'vue-router';
import VaultKeepCard from '../components/VaultKeepCard.vue';
import { keepService } from '../services/KeepService';
export default {
    setup(){
        let route = useRoute()
        onMounted(()=>{
            keepService.getAllKeeps()
            vaultService.getVaultById(route.params.vaultId)
            getVaultKeeps()
        })

        async function updatePrivate(){}

        async function getVaultKeeps(){
            await vaultService.getVaultKeeps(route.params.vaultId)
        }
    return { 
        activeVault: computed(()=> AppState.activeVault),
        keeps: computed(()=> AppState.vaultsKeeps),
        account: computed(()=> AppState.account)
     }
    }, components: { VaultKeepCard }
};
</script>


<style lang="scss" scoped>
.img-resize{
    max-height: 300px;
}

</style>