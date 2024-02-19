
import DetailModalWrapper from './DetailModalWrapper.vue';
<template>
    <div @click="getKeepById()" type="button" data-bs-toggle="modal" data-bs-target="#detailModal" :title="keeper.name" :style="newBg" class="keep-card p-2 d-flex flex-column justify-content-end">
        <div class="d-flex justify-content-between">
            <h4>{{ keeper.name }}</h4>
            <button class="btn btn-danger">Remove</button>
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
export default {
    props: {keeper: {type: Keeps, required: true}},
    setup(props){
        async function getKeepById(){
            await keepService.updateViews(props.keeper.id)
            await keepService.getKeepById(props.keeper.id)
        }
    return { 
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
}
</style>