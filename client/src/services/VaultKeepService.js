import { api } from "./AxiosService"


class VaultKeepService{
    async createVaultKeep(vaultID, keepID){
        let postData = {vaultId: vaultID, keepId: keepID}
        let response = await api.post('api/vaultkeeps', postData)
    }
}

export const vaultKeepService = new VaultKeepService()