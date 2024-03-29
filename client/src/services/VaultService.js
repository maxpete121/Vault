import { AppState } from "../AppState"
import { Keeps } from "../models/Keeps"
import { Vaults } from "../models/Vaults"
import { api } from "./AxiosService"


class VaultService{

    async getVaultById(vaultId){
        let response = await api.get(`api/vaults/${vaultId}`)
        AppState.activeVault = new Vaults(response.data)
    }

    async createVault(vaultData){
        let response = await api.post(`api/vaults`, vaultData)
        let newVault = new Vaults(response.data)
        AppState.userVaults.unshift(newVault)
    }

    async deleteVault(vaultId){
        let response = await api.delete(`api/vaults/${vaultId}`)
        return response.data
    }

    async getVaultKeeps(vaultId){
        let response = await api.get(`api/vaults/${vaultId}/keeps`)
            let vaultKeeps = await response.data.map(keep => new Keeps(keep))
            AppState.vaultsKeeps = vaultKeeps
    }

    async updatePrivate(vaultId){
        let vaultData = AppState.activeVault
        if(vaultData.isPrivate == false){
            vaultData.isPrivate = true
        }else{
            vaultData.isPrivate = false
        }
        let response = api.put(`api/vaults/${vaultId}`, vaultData)
        AppState.activeVault = new Vaults((await response).data)
    }
}


export const vaultService = new VaultService()