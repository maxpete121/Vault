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
        if(keeps == null)throw new Exception("Item not found.");
        return keeps;
    }

    internal Keeps UpdateKeep(Keeps updateKeepData, int keepId){
        Keeps originalKeep = GetOneKeepById(keepId);
        originalKeep.Name = updateKeepData.Name?.Length > 0 ? updateKeepData.Name : originalKeep.Name;
        originalKeep.Views = updateKeepData.Views > 0 ? updateKeepData.Views : originalKeep.Views;
        originalKeep.Description = updateKeepData.Description?.Length > 0 ? updateKeepData.Description : originalKeep.Description;
        originalKeep.Img = updateKeepData.Img?.Length > 0 ? updateKeepData.Img : originalKeep.Img;
        Keeps newKeep = repo.UpdateKeep(originalKeep);
        return newKeep;
    }

    internal string DeleteKeep(int keepId, string userId){
        Keeps keepToDelete = GetOneKeepById(keepId);
        if(keepToDelete.CreatorId == userId){
        repo.DeleteKeep(keepId);
        return $"{keepToDelete.Name} was deleted.";
        }else{throw new Exception("You can only delete things you own!");}
    }
}