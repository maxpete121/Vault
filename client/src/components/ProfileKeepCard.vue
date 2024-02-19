<template>
    <div @click="getKeepById()" type="button" data-bs-toggle="modal" data-bs-target="#detailModal" :title="keep.name" :style="styleBg" class="keep-card d-flex align-items-end p-2">
        <div class="d-flex align-items-center justify-content-between">
            <div>
                <h5 class="">{{ keep.name }}</h5>
            </div>
            <div class="d-flex">
                <img class="profile-pic rounded-circle" :src="keep.creator.picture" alt="User profile picture.">
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { Keeps } from '../models/Keeps';
export default {
    props: {keep: {type: Keeps, required: true}},
    setup(props){
    return { 
        styleBg: computed(()=>{
            let style = `background-image: url('${props.keep.img}');`
            return style
        })
     }
    }
};
</script>


<style lang="scss" scoped>
.keep-card{
    height: 200px;
}
.profile-pic{
    height: 50px;
    width: 50px;
}
</style>