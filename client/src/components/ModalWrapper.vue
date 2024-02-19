<template>
    <div class="modal fade" id="createModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Post a Keep</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createKeep()" action="" class="text-center">
                        <div class="container-fluid">
                            <div class="row justify-content-center">
                                <div class="col-8 text-center">
                                    <label for="">Name</label>
                                    <input v-model="keepData.name" class="form-control" required maxlength="40" type="text">
                                </div>
                            </div>
                            <div class="row mt-2 justify-content-center">
                                <div class="col-8 text-center">
                                    <label for="">Image URL</label>
                                    <input v-model="keepData.img" class="form-control" required maxlength="400" type="url">
                                </div>
                            </div>
                            <div class="row mt-2 justify-content-center">
                                <div class="col-10 text-center">
                                    <label for="">Description</label>
                                    <textarea v-model="keepData.description" class="form-control" required name="description" id="" cols="40" rows="5"></textarea>
                                </div>
                            </div>
                        </div>
                        <button class="btn btn-outline-dark mt-3">Post</button>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Back</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="createVModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Post a Vault</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createVault()" action="" class="text-center">
                        <div class="container-fluid">
                            <div class="row justify-content-center">
                                <div class="col-8 text-center">
                                    <label for="">Name</label>
                                    <input v-model="vaultData.name" class="form-control" required maxlength="40" type="text">
                                </div>
                            </div>
                            <div class="row mt-2 justify-content-center">
                                <div class="col-8 text-center">
                                    <label for="">Image URL</label>
                                    <input v-model="vaultData.img" class="form-control" required maxlength="400" type="url">
                                </div>
                            </div>
                            <div class="row mt-2 justify-content-center">
                                <div class="col-10 text-center">
                                    <label for="">Description</label>
                                    <textarea v-model="vaultData.description" class="form-control" required name="descriptionTwo" id="" cols="40" rows="5"></textarea>
                                </div>
                            </div>
                        </div>
                        <button class="btn btn-outline-dark mt-3">Post</button>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Back</button>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, ref, onMounted } from 'vue';
import { keepService } from '../services/KeepService.js';
import {vaultService} from '../services/VaultService.js';
export default {
    setup() {
        let keepData = ref({})
        let vaultData = ref({})
        async function createKeep(){
            await keepService.createKeep(keepData.value)
            keepData.value = {}
        }
        async function createVault(){
            await vaultService.createVault(vaultData.value)
            vaultData.value = {}
        }
        return {
            createVault,
            createKeep,
            keepData,
            vaultData
        }
    }
};
</script>


<style lang="scss" scoped></style>