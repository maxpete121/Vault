namespace Vault.Models;



public class Vaults : RepoItem<int>{
    public string CreatorId {get; set;}
    public string Name { get; set;}
    public string Description {get; set;}
    public string Img {get; set;}
    public bool IsPrivate {get; set;}
    public Account Creator {get; set;}
}