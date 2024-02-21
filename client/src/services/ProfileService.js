import { AppState } from "../AppState"
import { Account } from "../models/Account"
import { Keeps } from "../models/Keeps"
import { Vaults } from "../models/Vaults"
import { api } from "./AxiosService"



class ProfileService{
    async getProfileById(profileId){
        let response = await api.get(`api/profiles/${profileId}`)
        AppState.activeUserProfile = new Account(response.data)
    }

    async getUserVaults(profileId){
        let response = await api.get(`api/profiles/${profileId}/vaults`)
        let newVaults = await response.data.map(vault => new Vaults(vault))
        AppState.userVaults = newVaults
    }

    async getYourVaults(profileId){
        let response = await api.get(`api/profiles/${profileId}/vaults`)
        let newVaults = await response.data.map(vault => new Vaults(vault))
        AppState.yourVaults = newVaults
    }

    async getUserKeeps(profileId){
        let response = await api.get(`api/profiles/${profileId}/keeps`)
        let userKeeps = await response.data.map(keep => new Keeps(keep))
        AppState.userKeeps = userKeeps
    }

    async getMyVaults(){
        let response = await api.get('/account/vaults')
        let newVaults = await response.data.map(vault => new Vaults(vault))
        AppState.userVaults = newVaults
    }
}

export const profileService = new ProfileService()