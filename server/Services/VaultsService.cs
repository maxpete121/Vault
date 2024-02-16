namespace Vault.Services;


public class VaultService(VaultsRepository repo){
    private readonly VaultsRepository repo = repo;

    internal Vaults CreateVault(Vaults vaultData){
        Vaults vaults = repo.CreateVault(vaultData);
        return vaults;
    }
}