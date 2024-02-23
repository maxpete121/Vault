<template>
    <div>
        <form @submit.prevent="searchKeeps()" action="">
            <select v-model="paramData" class="search-select" name="" id="">
                <option selected disabled value="filter">Filter ↓</option>
                <option value="tag">Tags</option>
                <option value="keep">Name</option>
            </select>
            <input v-model="searchData" class="search-input" type="text">
            <button class="search-button">Search</button>
        </form>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import {keepService} from '../services/KeepService.js'
import Pop from '../utils/Pop';
export default {
    setup(){
        let paramData = ref('filter')
        let searchData = ref('')

        async function searchKeeps(){
            if(searchData.value.length > 0 && paramData.value == 'keep'){
                await keepService.searchKeeps(searchData.value)
                searchData.value = ''
                paramData.value = 'filter'
            }else if(searchData.value.length > 0 && paramData.value == 'tag'){
                await keepService.searchKeepsByTag(searchData.value)
                searchData.value = ''
                paramData.value = 'filter'
            }else{
                Pop.error('You must fill out the required fields to continue.')
            }
        }
    return { 
        paramData,
        searchData,
        searchKeeps,
     }
    }
};
</script>


<style lang="scss" scoped>
.search-input{
    all: unset;
    padding-left: 10px;
    padding-right: 8px;
    padding-top: 3px;
    padding-bottom: 3px;
    border: solid 1px black;
}
.search-input:focus{
    all: unset;
    padding-left: 10px;
    padding-right: 8px;
    padding-top: 3px;
    padding-bottom: 3px;
    border: solid 1px black;
}

.search-button{
    all: unset;
    border: solid 1px black;
    border-top-right-radius: 15px;
    border-bottom-right-radius: 15px;
    padding-top: 3px;
    padding-bottom: 3px;
    padding-right: 8px;
    padding-left: 8px;
}
.search-button:hover{
    all: unset;
    border: solid 1px black;
    border-top-right-radius: 15px;
    border-bottom-right-radius: 15px;
    padding-top: 3px;
    padding-bottom: 3px;
    padding-right: 8px;
    padding-left: 8px;
    background-color: rgb(221, 221, 221);
    cursor: pointer;
}

.search-select{
    all: unset;
    border: solid 1px black;
    border-top-left-radius: 15px;
    border-bottom-left-radius: 15px;
    padding-top: 3px;
    padding-bottom: 3px;
    padding-right: 6px;
    padding-left: 8px;
}
.search-select:hover{
    all: unset;
    border: solid 1px black;
    border-top-left-radius: 15px;
    border-bottom-left-radius: 15px;
    padding-top: 3px;
    padding-bottom: 3px;
    padding-right: 6px;
    padding-left: 8px;
    background-color: rgb(221, 221, 221);
    cursor: pointer;
}
</style>