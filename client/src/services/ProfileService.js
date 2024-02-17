import { AppState } from "../AppState"
import { Account } from "../models/Account"
import { api } from "./AxiosService"



class ProfileService{
    async getProfileById(profileId){
        let response = await api.get(`api/profiles/${profileId}`)
        AppState.activeUserProfile = new Account(response.data)
    }
}

export const profileService = new ProfileService()