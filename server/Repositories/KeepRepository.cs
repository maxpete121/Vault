namespace Vault.Repositories;

public class KeepRepository(IDbConnection db){
    private readonly IDbConnection db = db;

    internal Keeps CreateKeep(Keeps keepData){
        string sql =@"
        INSERT INTO keeps
        (creatorId ,name, description, img, views)
        VALUES
        (@creatorId, @name, @description, @img, @views);

        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE keeps.id = LAST_INSERT_ID();
        ";
        Keeps keeps = db.Query<Keeps, Account, Keeps>(sql, (keeps, account)=>{
            keeps.Creator = account;
            keeps.Kept = 0;
            return keeps;
        }, keepData).FirstOrDefault();
        return keeps;
    }

    internal List<Keeps> GetAllKeeps(){
        string sql =@"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id";
        List<Keeps> keeps = db.Query<Keeps, Account, Keeps>(sql, (keep, account)=>{
            keep.Creator = account;
            return keep;
        }).ToList();
        return keeps;
    }

    internal Keeps GetOneKeepById(int keepId){
        string sql = @"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE keeps.id = @keepId
        ";
        Keeps keeps = db.Query<Keeps, Account, Keeps>(sql, (keep, account)=>{
            keep.Creator = account;
            return keep;
        }, new{keepId}).FirstOrDefault();
        return keeps;
    }

    internal Keeps UpdateKeep(Keeps updateKeepData){
        string sql = @"
        UPDATE keeps SET
        name = @name,
        description = @description,
        img = @img,
        views = @views,
        kept = @kept
        WHERE id = @id;

        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE keeps.id = @id
        ";
        Keeps keeps = db.Query<Keeps, Account, Keeps>(sql, (keeps, account)=>{
            keeps.Creator = account;
            return keeps;
        }, updateKeepData).FirstOrDefault();
        return keeps;
    }

    internal void DeleteKeep(int keepId){
        string sql = @"
        DELETE FROM keeps
        WHERE id = @keepId
        ";
        db.Execute(sql, new{keepId});
    }
}