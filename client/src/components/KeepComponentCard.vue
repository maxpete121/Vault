<template>
    <div @click="getKeepById()" type="button" data-bs-toggle="modal" data-bs-target="#detailModal" :title="keep.name" :style="styleBg" class="keepCard d-flex align-items-end p-2 justify-content-between">
        <div>
            <h5 class="">{{ keep.name }}</h5>
        </div>
        <div>
            <img class="profile-pic rounded-circle" :src="keep.creator.picture" alt="User profile picture.">
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
export default {
    props: {keep: {type: Keeps, required: true}},
    setup(props){
        async function getKeepById(){
            await keepService.getKeepById(props.keep.id)
        }
    return { 
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
    height: 200px;
    background-position: center;
    background-size: cover;
    border-radius: 5px;
    box-shadow: 3px 3px 3px rgba(0, 0, 0, 0.408);
    color: white;
    text-shadow: 2px 3px 3px black;
}
.keepCard:hover{
    height: 200px;
    background-position: center;
    background-size: cover;
    border-radius: 5px;
    color: white;
    text-shadow: 2px 3px 3px black;
    transform: scale(1.04);
    cursor: pointer;
}
.profile-pic{
    height: 50px;
    width: 50px;
}
</style>