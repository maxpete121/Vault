namespace Vault.Models;


public class Tags : RepoItem<int>{
    public string CreatorId {get; set;}
    public int KeepId {get; set;}
    public string Name {get; set;}
    public Account Creator {get; set;}
}