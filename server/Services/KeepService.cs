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

    internal Keeps UpdateKeep(Keeps updateKeepData, int keepId,string userId){
        Keeps originalKeep = GetOneKeepById(keepId);
        if(originalKeep.CreatorId == userId){
        originalKeep.Name = updateKeepData.Name?.Length > 0 ? updateKeepData.Name : originalKeep.Name;
        originalKeep.Description = updateKeepData.Description?.Length > 0 ? updateKeepData.Description : originalKeep.Description;
        originalKeep.Img = updateKeepData.Img?.Length > 0 ? updateKeepData.Img : originalKeep.Img;
        Keeps newKeep = repo.UpdateKeep(originalKeep);
        return newKeep;
        }else{throw new Exception("You are not authorized to make this request.");}
    }

    internal Keeps UpdateKeepView(Keeps keepData, int keepId){
        Keeps originalKeep = GetOneKeepById(keepId);
        originalKeep.Kept = keepData.Kept > 0 ? keepData.Kept : originalKeep.Kept;
        originalKeep.Views = keepData.Views > 0 ? keepData.Views : originalKeep.Views;
        Keeps newKeep = repo.UpdateKeepView(originalKeep);
        return newKeep;
    }

    internal string DeleteKeep(int keepId, string userId){
        Keeps keepToDelete = GetOneKeepById(keepId);
        if(keepToDelete.CreatorId == userId){
        repo.DeleteKeep(keepId);
        return $"{keepToDelete.Name} was deleted.";
        }else{throw new Exception("You can only delete things you own!");}
    }

    internal List<Keeps> SearchKeeps(string query){
        query += '%';
        List<Keeps> keeps = repo.SearchKeeps(query);
        return keeps;
    }
}