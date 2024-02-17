<template>
    <section class="container-fluid">
        <div class="row justify-content-center">
            <div :style="newBg" class="col-10 bg-resize d-flex justify-content-center align-items-end p-2">
                <div class="">
                    <div class="text-center">
                        <img class="profile-pic rounded-circle mb-2" :src="userAccount.picture" alt="User picture.">
                        <h5>{{ userAccount.name }}</h5>
                    </div>
                </div>
            </div>
        </div>
        <h3 class="ms-4 fst-italic mt-2">Vaults</h3>
        <div class="row mt-2">
            <div></div>
        </div>
    </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
export default {
    setup() {
        let useUserAccount = computed(() => AppState.activeUserProfile)
        return {
            userAccount: computed(() => AppState.activeUserProfile),
            userVaults: computed(()=> AppState.userVaults),

            newBg: computed(() => {
                let style = `background-image: url('${useUserAccount.value.coverImg}');`
                return style
            })
        }
    }
};
</script>


<style lang="scss" scoped>
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