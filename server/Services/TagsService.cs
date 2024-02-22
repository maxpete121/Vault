namespace Vault.Services;


public class TagsService(TagsRepository repo){
    private readonly TagsRepository repo = repo;

    internal Tags CreateTag(Tags tagData, string userId){
        Tags tags = repo.CreateTag(tagData);
        return tags;
    }

    internal List<Tags> GetTagsByKeep(int keepId){
        List<Tags> tags = repo.GetTagsByKeep(keepId);
        return tags;
    }

    internal Tags GetTagById(int tagId){
        Tags tag = repo.GetTagById(tagId);
        return tag;
    }

    internal string DeleteTag(int tagId, string userId){
        Tags foundTag = GetTagById(tagId);
        if(userId == foundTag.CreatorId){
        repo.DeleteTag(tagId);
        string message = $"{foundTag.Name} was removed.";
        return message;
        }else{throw new Exception("You are not the owner of this tag.");}
    }
}