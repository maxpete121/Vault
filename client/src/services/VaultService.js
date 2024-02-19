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
    }

    async getVaultKeeps(vaultId){
        let response = await api.get(`api/vaults/${vaultId}/keeps`)
        let vaultKeeps = await response.data.map(keep => new Keeps(keep))
        AppState.vaultsKeeps = vaultKeeps
        console.log(vaultKeeps)
    }
}


export const vaultService = new VaultService()