namespace Vault.Models;


public class VaultKeeps : RepoItem<int>{
    public string CreatorId {get; set;}
    public int VaultId {get; set;}
    public int KeepId {get; set;}
}