namespace Vault.Repositories;



public class TagsRepository(IDbConnection db){
    private readonly IDbConnection db = db;

    internal Tags CreateTag(Tags tagData){
        string sql = @"
        INSERT INTO tags
        (creatorId , keepId, name)
        VALUES
        (@creatorId , @keepId, @name);

        SELECT
        tags.*,
        accounts.*
        FROM tags
        JOIN accounts ON tags.creatorId = accounts.id
        WHERE tags.id = LAST_INSERT_ID();
        ";

        Tags tag = db.Query<Tags, Account, Tags>(sql, (tag, account)=>{
            tag.Creator = account;
            return tag;
        }, tagData).FirstOrDefault();
        return tag;
    }
}