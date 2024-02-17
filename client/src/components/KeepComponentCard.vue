<template>
    <div :style="styleBg" class="keepCard">
        <h5>{{ keep.name }}</h5>
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
.keepCard{
    height: 200px;
    background-position: center;
    background-size: cover;
}
</style>