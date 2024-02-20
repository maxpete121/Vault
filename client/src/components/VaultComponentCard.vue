<template>
    <div v-if="userVault.isPrivate == false" @click="getVaultById(userVault.id)" :style="newBg" class="vault d-flex flex-column justify-content-end p-2">
        <div class="d-flex justify-content-between align-items-center">
            <h5 class="me-4">{{ userVault.name }}</h5>
            <button class="btn btn-danger"><i class="mdi mdi-delete"></i></button>
        </div>
    </div>
    <div v-else-if="userVault.isPrivate == true" @click="getVaultById(userVault.id)" :style="newBg" class="vault d-flex flex-column justify-content-between p-2">
        <div class="d-flex justify-content-start">
            <h3><i class="mdi mdi-lock"></i></h3>
        </div>
        <div class="d-flex justify-content-between align-items-center">
            <h5 class="me-4">{{ userVault.name }}</h5>
            <button class="btn btn-danger"><i class="mdi mdi-delete"></i></button>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { Vaults } from '../models/Vaults';
import { vaultService } from '../services/VaultService';
import { router } from '../router';
export default {
    props: {userVault: {type: Vaults, required: true}},
    setup(props){
        async function getVaultById(vaultId){
            await vaultService.getVaultById(vaultId)
            router.push({ name: 'Vault', params: { vaultId: props.userVault.id } })
        }
    return { 
        getVaultById,
        account: computed(()=> AppState.account),
        newBg: computed(()=>{
            let style = `background-image: url('${props.userVault.img}');`
            return style
        })
     }
    }
};
</script>


<style lang="scss" scoped>
.vault{
    height: 200px;
    background-position: center;
    background-size: cover;
    color: white;
    text-shadow: 3px 3px 4px black;
    box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.466);
    overflow-x: auto;
}
.vault:hover{
    height: 200px;
    background-position: center;
    background-size: cover;
    color: white;
    text-shadow: 3px 3px 4px black;
    box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.466);
    overflow-x: auto;
    transform: scale(1.03);
    cursor: pointer;
}
</style>