<template>
    <div @click="getVaultById()" :style="newBg" class="vault d-flex align-items-end p-2">
        <div class=" align-items-center">
            <h6 class="me-4">{{ userVault.name }}</h6>
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
        async function getVaultById(){
            await vaultService.getVaultById(props.userVault.id)
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