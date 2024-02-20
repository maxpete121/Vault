import { AppState } from "../AppState"
import { Keeps } from "../models/Keeps"
import { api } from "./AxiosService"



class KeepService{
    async createKeep(keepData){
        let response = await api.post('api/keeps', keepData)
        let newKeep = new Keeps(response.data)
        AppState.allKeeps.unshift(newKeep)
    }
    async getAllKeeps(){
        let response = await api.get('api/keeps')
        let allKeeps = await response.data.map(keeps => new Keeps(keeps))
        AppState.allKeeps = allKeeps.reverse()
    }

    async getKeepById(keepId){
        let response = await api.get(`api/keeps/${keepId}`)
        AppState.activeKeep = new Keeps(response.data)
    }

    async updateViews(keepId){
        let foundKeep = AppState.allKeeps.find(keep => keep.id == keepId)
        foundKeep.views++
        let response = await api.put(`api/keeps/${keepId}/views`, foundKeep)
    }

    async updateKept(keepId){
        let foundKeep = AppState.allKeeps.find(keep => keep.id == keepId)
        foundKeep.kept++
        let response = await api.put(`api/keeps/${keepId}`, foundKeep)
    }

    async deleteKeep(keepId){
        let response = await api.delete(`api/keeps/${keepId}`)
        let keepIndex = AppState.allKeeps.findIndex(keep => keep.id == keepId)
        let userKeepIndex = AppState.userKeeps.findIndex(keep => keep.id == keepId)
        AppState.userKeeps.splice(userKeepIndex, 1)
        AppState.allKeeps.splice(keepIndex, 1)
        return response.data
    }
}

export const keepService = new KeepService()