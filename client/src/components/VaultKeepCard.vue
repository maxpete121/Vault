
import DetailModalWrapper from './DetailModalWrapper.vue';
<template>
    <div @click="getKeepById()" type="button" :title="keeper.name" :style="newBg" class="keep-card p-2 d-flex flex-column justify-content-end">
        <div class="d-flex justify-content-between text-light">
            <h4>{{ keeper.name }}</h4>
            <button v-if="account.id == activeVault.creatorId" @click="deleteVaultKeep(keeper.vaultKeepId)" class="btn btn-danger">Remove</button>
        </div>
    </div>
    <DetailModalWrapper id="detailModal"/>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { Keeps } from '../models/Keeps';
import DetailModalWrapper from './DetailModalWrapper.vue';
import { keepService } from '../services/KeepService';
import {vaultKeepService} from '../services/VaultKeepService.js'
import Pop from '../utils/Pop';
import { Modal } from 'bootstrap';
import { router } from '../router';
import { profileService } from '../services/ProfileService';
export default {
    props: {keeper: {type: Keeps, required: true}},
    setup(props){
        let keeps = computed(()=> AppState.vaultsKeeps)
        async function getKeepById(){
            await keepService.updateViews(props.keeper.id)
            await keepService.getKeepById(props.keeper.id)
            await profileService.myActiveVaults()
            openModal()
        }

        async function openModal(){
            if(keeps.value.length > 1){
                Modal.getOrCreateInstance("#detailModal").show()
            }
        }


        async function deleteVaultKeep(vaultId){
            try {
                let message = await vaultKeepService.deleteVaultKeep(vaultId)
                setTimeout(()=> Modal.getOrCreateInstance("#detailModal").hide(), 300)
                Pop.success(message)
            } catch (error) {
                router.push({ name: 'Home'})
            }
        }
    return { 
        activeVault: computed(()=> AppState.activeVault),
        account: computed(()=> AppState.account),
        openModal,
        deleteVaultKeep,
        getKeepById,
        newBg: computed(()=>{
            let style = `background-image: url('${props.keeper.img}');`
            return style
        })
     }
    }, components: {DetailModalWrapper}
};
</script>


<style lang="scss" scoped>
.keep-card{
    height: 250px;
    background-size: cover;
    background-position: center;
    box-shadow: 4px 4px 5px rgba(0, 0, 0, 0.5);
    text-shadow: 2px 2px 2px black;
}

.keep-card:hover{
    height: 250px;
    background-size: cover;
    background-position: center;
    box-shadow: 4px 4px 5px rgba(0, 0, 0, 0.5);
    text-shadow: 2px 2px 2px black;
    transform: scale(1.02);
}
</style>