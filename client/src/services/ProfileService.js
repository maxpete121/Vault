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

    async getUserKeeps(profileId){
        let response = await api.get(`api/profiles/${profileId}/keeps`)
        let userKeeps = await response.data.map(keep => new Keeps(keep))
        AppState.userKeeps = userKeeps
    }
}

export const profileService = new ProfileService()