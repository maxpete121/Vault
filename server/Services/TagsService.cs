namespace Vault.Services;


public class TagsService(TagsRepository repo){
    private readonly TagsRepository repo = repo;

    internal Tags CreateTag(Tags tagData, string userId){
        Tags tags = repo.CreateTag(tagData);
        return tags;
    }
}