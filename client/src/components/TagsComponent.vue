<template>
    <div class="d-inline-block">
        <div v-if="account.id == tag.creatorId" class="d-inline-block align-items-center">
            <div class="d-flex">
                <div class="tag-main-card">
                    <h6 class="mt-1 fst-italic">#{{ tag.name }}</h6>
                </div>
                <button class="tag-delete"><i type="button" class="mdi mdi-close fs-5 ms-1 me-1"></i></button>
            </div>
        </div>
        <div v-if="account.id !== tag.creatorId" class="d-inline-block align-items-center">
            <div class="tag-main-card-two">
                <h6 class="mt-1 fst-italic">#{{ tag.name }}</h6>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { Tags } from '../models/Tags';
export default {
    props: {tag: {type: Tags, required: true}},
    setup(){
    return { 
        account: computed(()=> AppState.account),
     }
    }
};
</script>


<style lang="scss" scoped>
.tag-main-card{
    outline: solid 1px black;
    border-top-left-radius: 10px;
    border-bottom-left-radius: 10px;
    padding-left: 8px;
    padding-right: 8px;
    background-color: rgba(212, 212, 212, 0.547);
}
.tag-main-card:hover{
    outline: solid 1px black;
    border-top-left-radius: 10px;
    border-bottom-left-radius: 10px;
    padding-left: 8px;
    padding-right: 8px;
    background-color: rgba(212, 212, 212, 0.547);
    cursor: pointer;
}

.tag-delete{
    all: unset;
    outline: solid 1px black;
    border-top-right-radius: 10px;
    border-bottom-right-radius: 10px;
    background-color: rgba(212, 212, 212, 0.547);
}

.tag-delete:hover{
    all: unset;
    outline: solid 1px black;
    border-top-right-radius: 10px;
    border-bottom-right-radius: 10px;
    background-color: rgba(255, 0, 0, 0.219);
}

.tag-main-card-two{
    outline: solid 1px black;
    border-radius: 10px;
    padding-left: 8px;
    padding-right: 8px;
    padding-top: 2px;
    padding-bottom: 2px;
    background-color: rgba(212, 212, 212, 0.547);
}
.tag-main-card-two:hover{
    outline: solid 1px black;
    border-radius: 10px;
    padding-left: 8px;
    padding-right: 8px;
    padding-top: 2px;
    padding-bottom: 2px;
    background-color: rgba(212, 212, 212, 0.547);
    cursor: pointer;
}
</style>