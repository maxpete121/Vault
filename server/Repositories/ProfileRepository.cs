namespace Vault.Repositories;

public class ProfileRepository(IDbConnection db){
    private readonly IDbConnection db = db;

    internal Account GetUsersProfile(string profileId){
        string sql = @"
        SELECT
        *
        FROM accounts
        WHERE id = @profileId;
        ";
        Account account = db.Query<Account>(sql, new{profileId}).FirstOrDefault();
        return account;
    }

    internal List<Keeps> GetUsersKeeps(string profileId){
        string sql = @"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE keeps.creatorId = @profileId;
        ";
        List<Keeps> keeps = db.Query<Keeps, Account, Keeps>(sql, (keep, account)=>{
            keep.Creator = account;
            return keep;
        }, new{profileId}).ToList();
        return keeps;
    }

    internal List<Vaults> GetUserVaults(string profileId){
        string sql = @"
        SELECT
        *
        FROM vaults
        WHERE creatorId = @profileId;
        ";
        List<Vaults> vaults = db.Query<Vaults>(sql, new{profileId}).ToList();
        return vaults;
    }
}