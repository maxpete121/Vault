<template>
    <div @click="getKeepById(), getTagsByKeep()" type="button" data-bs-toggle="modal" data-bs-target="#detailModal" :title="keep.name" :style="styleBg" class="keepCard d-flex justify-content-center align-items-end p-2">
        <img class="card-img" :src="keep.img" alt="">
        <div class="d-flex card-child align-items-center justify-content-between">
            <div>
                <h5 class="">{{ keep.name }}</h5>
            </div>
            <div class="d-flex">
                <img class="profile-pic rounded-circle" :src="keep.creator.picture" alt="User profile picture.">
            </div>
        </div>
    </div>
    <DetailModalWrapper id="detailModal"/>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { Keeps } from '../models/Keeps';
import DetailModalWrapper from '../components/DetailModalWrapper.vue'
import {keepService} from '../services/KeepService.js'
import { vaultService } from '../services/VaultService';
import { profileService } from '../services/ProfileService';
import { tagsService } from '../services/TagsService';
export default {
    props: {keep: {type: Keeps, required: true}},
    setup(props){
        let useAccount = computed(()=> AppState.account)
        async function getKeepById(){
            await keepService.updateViews(props.keep.id)
            await keepService.getKeepById(props.keep.id)
            await profileService.getYourVaults(useAccount.value.id)
        }

        async function getTagsByKeep(){
            await tagsService.getTagsByKeep(props.keep.id)
        }
    return { 
        getTagsByKeep,
        getKeepById,
        styleBg: computed(()=>{
            let style = `background-image: url('${props.keep.img}');`
            return style
        })
     }
    }, components: {DetailModalWrapper}
};
</script>


<style lang="scss" scoped>
.keepCard{
    background-position: center;
    background-size: cover;
    border-radius: 5px;
    box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.462);
    color: white;
    text-shadow: 2px 3px 3px black;
}
.keepCard:hover{
    background-position: center;
    background-size: cover;
    border-radius: 5px;
    box-shadow: 3px 3px 3px rgba(0, 0, 0, 0.462);
    color: white;
    text-shadow: 2px 3px 3px black;
    transform: scale(1.02);
    cursor: pointer;
}

@media screen and (min-width: 576px){
    .card-child{
        width: 310px;
        position: absolute;
    }
}
@media screen and (max-width: 576px){
    .card-child{
        width: 170px;
        position: absolute;
    }
}
.card-img{
    visibility: hidden;
}
.profile-pic{
    height: 50px;
    width: 50px;
    outline: solid 1px white;
}
</style>