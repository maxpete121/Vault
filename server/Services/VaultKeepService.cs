namespace Vault.Services;

public class VaultKeepService(VaultKeepRepository repo){
    private readonly VaultKeepRepository repo = repo;

    internal VaultKeeps CreateVaultKeep(VaultKeeps newData){
        VaultKeeps newVkeep = repo.CreateVaultKeep(newData);
        return newVkeep;
    }

    internal List<VaultedKeep> GetKeepsInVault(int vaultId){
        List<VaultedKeep> vaultedKeeps = repo.GetKeepsInVault(vaultId);
        return vaultedKeeps;
    }

    internal VaultKeeps GetOneVaultKeepById(int vaultKeepId){
        VaultKeeps vaultKeeps = repo.GetOneVaultKeepById(vaultKeepId);
        if(vaultKeeps == null)throw new Exception("Item not found.");
        return vaultKeeps;
    }

    internal string DeleteVaultKeep(int vaultKeepId, string userId){
        VaultKeeps vaultKeeps = GetOneVaultKeepById(vaultKeepId);
        if(vaultKeeps.CreatorId == userId){
            repo.DeleteVaultKeep(vaultKeepId);
            return "Vault keep deleted.";
        }else{
            throw new Exception("You are not authorized to delete this item.");
        }
    }
}