namespace Vault.Services;


public class KeepService(KeepRepository repo){
    private readonly KeepRepository repo = repo;

    internal Keeps CreateKeep(Keeps keepData){
        Keeps keeps = repo.CreateKeep(keepData);
        return keeps;
    }
}