import { AppState } from "../AppState"
import { Keeps } from "../models/Keeps"
import { api } from "./AxiosService"



class KeepService{

    async getAllKeeps(){
        let response = await api.get('api/keeps')
        let allKeeps = await response.data.map(keeps => new Keeps(keeps))
        AppState.allKeeps = allKeeps
    }

    async getKeepById(keepId){
        let response = await api.get(`api/keeps/${keepId}`)
        AppState.activeKeep = new Keeps(response.data)
    }
}

export const keepService = new KeepService()