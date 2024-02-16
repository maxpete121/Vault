namespace Vault.Models;


public class Keeps : RepoItem<int>{
    public string CreatorId {get; set;}
    public string Name {get; set;}
    public string Description {get; set;}
    public string Img {get; set;}
    public int Views {get; set;}
    public Account Creator {get; set;}
    public int Kept {get; set;}
}

public class VaultedKeep : Keeps{
    public int VaultKeepId {get; set;}
}