import { AppState } from "../AppState"
import { Account } from "../models/Account"
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
}

export const profileService = new ProfileService()