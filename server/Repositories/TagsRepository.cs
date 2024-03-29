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

    internal List<Tags> GetTagsByKeep(int keepId){
        string sql = @"
        SELECT
        tags.*,
        accounts.*
        FROM tags
        JOIN accounts ON tags.creatorId = accounts.id
        WHERE tags.keepId = @keepId;
        ";
        List<Tags> tags = db.Query<Tags, Account, Tags>(sql, (tag, account)=>{
            tag.Creator = account;
            return tag;
        }, new{keepId}).ToList();
        return tags;
    }

    internal Tags GetTagById(int tagId){
        string sql = @"
        SELECT
        *
        FROM tags
        WHERE tags.id = @tagId;
        ";
        Tags tags = db.Query<Tags>(sql, new{tagId}).FirstOrDefault();
        return tags;
    }

    internal void DeleteTag(int tagId){
        string sql = @"
        DELETE FROM tags
        WHERE id = @tagId
        ";
        db.Execute(sql, new{tagId});
    }

     internal List<Tag> SearchKeepTag(string query){
        string sql = @"
        SELECT
        keeps.*,
        tags.*,
        accounts.*
        FROM tags
        JOIN keeps ON tags.keepId = keeps.id
        JOIN accounts ON tags.creatorId = accounts.id
        WHERE tags.name LIKE @query;
        ";
        List<Tag> newTag = db.Query<Tag, Tags, Account, Tag>(sql, (tag, tags, account)=>{
            tag.Creator = account;
            tag.TagId = tags.Id;
            return tag;
        }, new{query}).ToList();
        return newTag;
    }
}