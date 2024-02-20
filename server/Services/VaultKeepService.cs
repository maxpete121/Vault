namespace Vault.Services;

public class VaultKeepService(VaultKeepRepository repo){
    private readonly VaultKeepRepository repo = repo;

    internal VaultKeeps CreateVaultKeep(VaultKeeps newData, string userId){
        Vaults vault = GetVaultById(newData.VaultId);
        if(vault.CreatorId == userId){
        VaultKeeps newVKeep = repo.CreateVaultKeep(newData);
        return newVKeep;
        }else{throw new Exception("You are not authorized to make this request.");}
    }

    internal Vaults GetVaultById(int vaultId){
        Vaults vault = repo.GetVaultById(vaultId);
        return vault;
    }

    internal List<VaultedKeep> GetKeepsInVault(int vaultId){
        Vaults vault = GetVaultById(vaultId);
        if(vault.IsPrivate == false){
        List<VaultedKeep> vaultedKeeps = repo.GetKeepsInVault(vaultId);
        return vaultedKeeps;
        }else{throw new Exception("These keeps are private.");}
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