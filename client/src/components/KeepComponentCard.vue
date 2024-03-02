<template>
    <div @click="getKeepById(), getTagsByKeep()" type="button" :title="keep.name" class="keepCard d-flex flex-column d-inline-block">
        <img class="card-img" :src="keep.img" alt="">
        <div class="section-bottom d-flex card-child align-items-center justify-content-between pt-2 pb-2 ps-lg-3 pe-lg-3 ps-2 pe-2 d-inline-block">
            <div>
                <h6 class="">{{ keep.name }}</h6>
            </div>
            <div class="d-flex">
                <img id="profile-picture" class="profile-pic rounded-circle" :src="keep.creator.picture" alt="User profile picture.">
            </div>
        </div>
    </div>
    <DetailModalWrapper id="detailModal"/>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted, watch } from 'vue';
import { Keeps } from '../models/Keeps';
import DetailModalWrapper from '../components/DetailModalWrapper.vue'
import {keepService} from '../services/KeepService.js'
import { vaultService } from '../services/VaultService';
import { profileService } from '../services/ProfileService';
import { tagsService } from '../services/TagsService';
import { Modal } from 'bootstrap';
export default {
    props: {keep: {type: Keeps, required: true}},
    setup(props){
        let useAccount = computed(()=> AppState.account)
        let useKeeps = computed(()=> AppState.allKeeps)
        watch(useKeeps, imageCheck)
        async function imageCheck(){
            try {
                console.log('wow')
                let img = document.getElementById("profile-picture")
                img.addEventListener("error", function(event){
                    event.target.src = "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTA1L3Y5MzctYWV3LTEzOS5qcGc.jpg"
                    event.onerror = null
                })     
            } catch (error) {
                
            }
        }
        async function getKeepById(){
            await keepService.updateViews(props.keep.id)
            await keepService.getKeepById(props.keep.id)
            await profileService.myActiveVaults()
            openModal()
        }

        async function getTagsByKeep(){
            await tagsService.getTagsByKeep(props.keep.id)
        }

        async function openModal(){
            Modal.getOrCreateInstance('#detailModal').show()
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
    border-radius: 10px;
    box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.462);
    overflow: hidden;
    outline: solid 1px black;
    -webkit-column-break-inside: avoid; 
}
.keepCard:hover{
    background-position: center;
    background-size: cover;
    border-radius: 10px;
    overflow: hidden;
    box-shadow: 3px 3px 3px rgba(0, 0, 0, 0.462);
    outline: solid 1px black;
    -webkit-column-break-inside: avoid; 
    transform: scale(1.02);
    cursor: pointer;
}

.section-bottom{
    outline: solid 1px black;
}
.profile-pic{
    height: 50px;
    width: 50px;
    outline: solid 1px white;
}
</style>