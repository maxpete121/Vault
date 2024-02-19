<template>
    <section class="container-fluid">
        <div class="row justify-content-center section-main ms-4 me-4">
            <div :style="newBg" class="col-10 bg-resize d-flex justify-content-center align-items-end p-2">
                <div class="">
                    <div class="text-center">
                        <img class="profile-pic rounded-circle mb-2" :src="userAccount.picture" alt="User picture.">
                        <h5>{{ userAccount.name }}</h5>
                    </div>
                </div>
            </div>
        </div>
        <div class="row mt-2">
            <h3 class="ms-4 fst-italic mt-2">Vaults</h3>
        </div>
        <div class="row mt-2 justify-content-center vault-container ms-4 me-4 p-1">
                <div class="col-3 m-3" v-for="userVault in userVaults">
                    <VaultComponentCard class="" :userVault="userVault"/>
                </div>
        </div>
        <div class="row">
            <h3 class="ms-4 fst-italic mt-2">Keeps</h3>
        </div>
        <div class="row justify-content-center vault-container ms-4 me-4 p-1">
            <div class="col-3 m-3" v-for="keep in keeps">
                <ProfileKeepCard :keep="keep"/>
            </div>
        </div>
    </section>
</template>


<script>
import { useRoute } from 'vue-router';
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { profileService } from '../services/ProfileService';
import VaultComponentCard from '../components/VaultComponentCard.vue';
import KeepComponentCard from '../components/KeepComponentCard.vue';
import ProfileKeepCard from '../components/ProfileKeepCard.vue';
export default {
    setup() {
        onMounted(()=>{
            getUserVaults()
            getUserKeeps()
        })
        let useUserAccount = computed(() => AppState.activeUserProfile)
        const route = useRoute()
        async function getUserVaults(){
            await profileService.getProfileById(route.params.profileId)
            await profileService.getUserVaults(route.params.profileId)
        }
        async function getUserKeeps(){
            await profileService.getUserKeeps(route.params.profileId)
        }
        return {
            keeps: computed(()=> AppState.userKeeps),
            userAccount: computed(() => AppState.activeUserProfile),
            userVaults: computed(()=> AppState.userVaults),

            newBg: computed(() => {
                let style = `background-image: url('${useUserAccount.value.coverImg}');`
                return style
            })
        }
    }, components: {VaultComponentCard, ProfileKeepCard}
};
</script>


<style lang="scss" scoped>
.section-main{
    border-bottom: solid 2px black;
    // box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.441);
}
.vault-container{
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
}

.profile-pic {
    height: 80px;
    width: 80px;
    outline: solid 1px black;
    box-shadow: 3px 3px 3px rgba(0, 0, 0, 0.488);
}
</style>