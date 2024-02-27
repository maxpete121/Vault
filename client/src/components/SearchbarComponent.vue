<template>
    <div>
        <form @submit.prevent="searchKeeps()" action="">
            <select v-model="paramData" class="search-select" name="search-filter" id="">
                <option selected disabled value="filter">Filter ↓</option>
                <option value="tag">Tags</option>
                <option value="keep">Name</option>
            </select>
            <input name="search-bar" v-model="searchData" class="search-input" type="text">
            <button class="search-button">Search</button>
        </form>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import {keepService} from '../services/KeepService.js'
import Pop from '../utils/Pop';
import { router } from '../router';
export default {
    setup(){
        let paramData = ref('filter')
        let searchData = ref('')

        async function searchKeeps(){
            router.push({name: 'Home'})
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
@media screen and (min-width: 576px){
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
}
@media screen and (max-width: 576px){
    .search-input{
    all: unset;
    padding-left: 10px;
    padding-right: 8px;
    padding-top: 3px;
    padding-bottom: 3px;
    border: solid 1px black;
    width: 100px;
}
.search-input:focus{
    all: unset;
    padding-left: 10px;
    padding-right: 8px;
    padding-top: 3px;
    padding-bottom: 3px;
    border: solid 1px black;
    width: 100px;
}
.search-button{
    all: unset;
    border: solid 1px black;
    border-top-right-radius: 15px;
    border-bottom-right-radius: 15px;
    padding-top: 3px;
    padding-bottom: 3px;
    padding-right: 4px;
    padding-left: 4px;
}
.search-button:hover{
    all: unset;
    border: solid 1px black;
    border-top-right-radius: 15px;
    border-bottom-right-radius: 15px;
    padding-top: 3px;
    padding-bottom: 3px;
    padding-right: 4px;
    padding-left: 4px;
    background-color: rgb(221, 221, 221);
    cursor: pointer;
}
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