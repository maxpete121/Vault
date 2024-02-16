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
}