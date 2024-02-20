import { AppState } from "../AppState"
import { api } from "./AxiosService"


class VaultKeepService{
    async createVaultKeep(vaultID, keepID){
        let postData = {vaultId: vaultID, keepId: keepID}
        let response = await api.post('api/vaultkeeps', postData)
    }

    async deleteVaultKeep(vaultKeepId){
        let response = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        let keepIndex = AppState.vaultsKeeps.findIndex(vault => vault.vaultKeepId == vaultKeepId)
        AppState.vaultsKeeps.splice(keepIndex, 1)
        return response.data
    }
}

export const vaultKeepService = new VaultKeepService()