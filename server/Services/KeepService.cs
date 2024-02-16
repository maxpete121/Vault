namespace Vault.Services;


public class KeepService(KeepRepository repo){
    private readonly KeepRepository repo = repo;

    internal Keeps CreateKeep(Keeps keepData){
        Keeps keeps = repo.CreateKeep(keepData);
        return keeps;
    }

    internal List<Keeps> GetAllKeeps(){
        List<Keeps> keeps = repo.GetAllKeeps();
        return keeps;
    }

    internal Keeps GetOneKeepById(int keepId){
        Keeps keeps = repo.GetOneKeepById(keepId);
        return keeps;
    }
}