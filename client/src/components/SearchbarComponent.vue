<template>
    <div>
        <form action="">
            <input class="search-input" type="text">
            <button class="search-button">Search</button>
        </form>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
export default {
    setup(){
    return {  }
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
    border-bottom-left-radius: 15px;
    border-top-left-radius: 15px;
    border: solid 1px black;
}
.search-input:focus{
    all: unset;
    padding-left: 10px;
    padding-right: 8px;
    padding-top: 3px;
    padding-bottom: 3px;
    border-bottom-left-radius: 15px;
    border-top-left-radius: 15px;
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
</style>