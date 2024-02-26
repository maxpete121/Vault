<template>
    <section class="container-fluid">
        <div class="row justify-content-center section-main ms-lg-4 me-lg-4 mt-2">
            <div :style="newBg" class="col-lg-10 col-12 bg-resize d-flex justify-content-center align-items-end p-2 mb-3">
                <div class="">
                    <div class="text-center">
                        <img class="profile-pic rounded-circle mb-2" :src="userAccount.picture" alt="User picture.">
                        <h4>{{ userAccount.name }}</h4>
                    </div>
                </div>
            </div>
        </div>
        <div class="row justify-content-between">
            <div class="col-lg-2 col-9">
                <h3 class="ms-lg-4 fst-italic mt-2">Keeps</h3>
                <span class="d-flex">
                    <h3 class="ms-4 me-2">Total Views</h3>
                    <h3>{{ TotalView }}</h3>
                </span>
            </div>
            <div class="col-lg-2 col-9 d-flex flex-column align-items-center me-lg-4 mb-2">
                <h5 class="most-text mt-2">Most Viewed</h5>
                <div :style="topViewBg" class="top-view mt-1 w-75 p-1">
                    <h6>{{ mostViewed.name }}</h6>
                </div>
            </div>
        </div>
        <div class="row justify-content-center vault-container ms-4 me-4 p-1">
            <div class="col-lg-3 col-9 m-3" v-for="keep in keeps">
                <ProfileKeepCard :keep="keep" />
            </div>
        </div>
        <h3 class="ms-4 fst-italic mt-2">Vaults</h3>
        <div class="row mt-2 justify-content-center vault-container ms-4 me-4 p-1">
            <div class="col-lg-3 col-9 m-3" v-for="userVault in userVaults">
                <VaultComponentCard class="" :userVault="userVault" />
            </div>
        </div>
    </section>
</template>


<script>
import { useRoute } from 'vue-router';
import { AppState } from '../AppState';
import { computed, ref, onMounted, watch } from 'vue';
import { profileService } from '../services/ProfileService';
import VaultComponentCard from '../components/VaultComponentCard.vue';
import KeepComponentCard from '../components/KeepComponentCard.vue';
import ProfileKeepCard from '../components/ProfileKeepCard.vue';
import { keepService } from '../services/KeepService';
export default {
    setup() {
        onMounted(() => {
            AppState.totalViews = 0
            getUserVaults()
            getUserKeeps()
            keepService.getAllKeeps()
        })
        let useAccount = computed(() => AppState.account)
        let useUserAccount = computed(() => AppState.activeUserProfile)
        let useKeeps = computed(()=> AppState.userKeeps)
        let useMostViewed = computed(()=> AppState.mostViewed)
        watch(useAccount, getUserVaults)
        watch(useKeeps, totalView)
        const route = useRoute()
        async function totalView(){
            AppState.totalViews = 0
            for(let i = 0; AppState.userKeeps.length > i; i++){
            AppState.totalViews += AppState.userKeeps[i].views
        }
        }
        async function getUserVaults() {
            if (useAccount.value.id == route.params.profileId) {
                await profileService.getMyVaults()
            } else {
                await profileService.getProfileById(route.params.profileId)
                await profileService.getUserVaults(route.params.profileId)
            }
        }
        async function getUserKeeps() {
            await profileService.getUserKeeps(route.params.profileId)
            setMostViewed()
        }

        async function setMostViewed(){
            await keepService.setMostViewed()
        }
         
        return {
            TotalView: computed(()=> AppState.totalViews),
            keeps: computed(() => AppState.userKeeps),
            userAccount: computed(() => AppState.activeUserProfile),
            userVaults: computed(() => AppState.userVaults),
            mostViewed: computed(()=> AppState.mostViewed),
            newBg: computed(() => {
                let style = `background-image: url('${useUserAccount.value.coverImg}');`
                return style
            }),
            topViewBg: computed(()=>{
                let style = `background-image: url('${useMostViewed.value.img}');`
                return style
            })
        }
    }, components: { VaultComponentCard, ProfileKeepCard }
};
</script>


<style lang="scss" scoped>
.section-main {
    border-bottom: solid 2px black;
    // box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.441);
}

.vault-container {
    max-height: 250px;
    overflow-y: scroll;
    border-bottom: solid 2px black;
}

.bg-resize {
    height: 300px;
    text-shadow: 3px 3px 3px black;
    color: white;
    background-position: center;
    background-size: cover;
    box-shadow: 3px 4px 7px rgba(0, 0, 0, 0.462);
}

.profile-pic {
    height: 80px;
    width: 80px;
    outline: solid 1px black;
    box-shadow: 3px 3px 3px rgba(0, 0, 0, 0.488);
}

.top-view{
    height: 100px;
    background-position: center;
    background-size: cover;
    color: white;
    text-shadow: 2px 2px 2px black;
    box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.418);
}

.most-text{
    border-bottom: solid 1px black;
}
</style>