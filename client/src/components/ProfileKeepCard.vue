<template>
    <div @click="getKeepById(), getTagsByKeep()" type="button" :title="keep.name" :style="styleBg" class="keep-card d-flex flex-column justify-content-end p-2">
        <div class="d-flex align-items-center justify-content-between">
                <h5 class="">{{ keep.name }}</h5>
        </div>
    </div>
    <DetailModalWrapper id="detailModal"/>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { Keeps } from '../models/Keeps';
import { keepService } from '../services/KeepService';
import DetailModalWrapper from './DetailModalWrapper.vue';
import { tagsService } from '../services/TagsService';
import { Modal } from 'bootstrap';
import { vaultService } from '../services/VaultService';
import { profileService } from '../services/ProfileService';
export default {
    props: {keep: {type: Keeps, required: true}},
    setup(props){
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
.keep-card{
    height: 200px;
    background-position: center;
    background-size: cover;
    color: white;
    text-shadow: 2px 2px 2px black;
    box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.455);
}
.keep-card:hover{
    height: 200px;
    background-position: center;
    background-size: cover;
    color: white;
    text-shadow: 2px 2px 2px black;
    box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.455);
    transform: scale(1.02);
}
.profile-pic{
    height: 50px;
    width: 50px;
}
</style>