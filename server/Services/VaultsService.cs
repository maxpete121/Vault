namespace Vault.Services;


public class VaultService(VaultsRepository repo){
    private readonly VaultsRepository repo = repo;

    internal Vaults CreateVault(Vaults vaultData){
        Vaults vaults = repo.CreateVault(vaultData);
        return vaults;
    }

    internal Vaults GetVaultById(int vaultId){
        Vaults vaults = repo.GetVaultById(vaultId);
        if(vaults == null || vaults.IsPrivate == true){throw new Exception("Item not found.");}
        else{
        return vaults;
        }
    }

    internal Vaults UpdateVault(Vaults vaultData, int vaultId, string userId){
        Vaults originalVault = GetVaultById(vaultId);
        if(originalVault == null)throw new Exception("Item not found.");
        if(originalVault.CreatorId == userId){
         originalVault.Name = vaultData.Name?.Length > 0 ? vaultData.Name : originalVault.Name;
         originalVault.Description = vaultData.Description?.Length > 0 ? vaultData.Description : originalVault.Description;
         originalVault.Img = vaultData.Img?.Length > 0 ? vaultData.Img : originalVault.Img;
         originalVault.IsPrivate = vaultData.IsPrivate != originalVault.IsPrivate ? vaultData.IsPrivate : originalVault.IsPrivate;
         Vaults newVault = repo.UpdateVault(originalVault);
         return newVault;
        }else{
            throw new Exception("You are not authorized to update this Vault.");
        }
    }

    internal string DeleteVault(int vaultId, string userId){
        Vaults foundVault = GetVaultById(vaultId);
        if(foundVault.CreatorId == userId){
            repo.DeleteVault(vaultId);
            return $"{foundVault.Name} was deleted.";
        }else{throw new Exception("You are not authorized to delete this Vault.");}
    }

    internal List<Vaults> GetMyVaults(string userId){
        if(userId != null){
        List<Vaults> myVaults = repo.GetMyVaults(userId);
        return myVaults;
        }else{throw new Exception("You must be logged in to make this request");}
    }

    internal List<Vaults> GetPrivateVaults(string userId){
        List<Vaults> vaults = repo.GetPrivateVaults(userId);
        return vaults;
    }
}