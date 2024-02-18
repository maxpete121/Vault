<template>
    <div :style="newBg" class="vault d-flex align-items-end p-2">
        <div class="d-flex justify-content-between">
            <h5>{{ userVault.name }}</h5>
            <button class="btn btn-outline-danger"><i class="mdi mdi-delete"></i></button>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { Vaults } from '../models/Vaults';
export default {
    props: {userVault: {type: Vaults, required: true}},
    setup(props){
    return { 
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
    height: 150px;
    background-position: center;
    background-size: cover;
    color: white;
    text-shadow: 3px 3px 4px black;
}
</style>