namespace Vault.Repositories;

public class VaultKeepRepository(IDbConnection db){
    private readonly IDbConnection db = db;

    internal VaultKeeps CreateVaultKeep(VaultKeeps newData){
        string sql = @"
        INSERT INTO vaultkeeps
        (creatorId, vaultId, keepId)
        VALUES
        (@creatorId, @vaultId, @keepId);

        SELECT
        vaultkeeps.*,
        accounts.*
        FROM vaultkeeps
        JOIN accounts ON vaultkeeps.creatorId = accounts.id
        WHERE vaultkeeps.id = LAST_INSERT_ID();
        ";
        VaultKeeps newVaultKeep = db.Query<VaultKeeps, Account, VaultKeeps>(sql, (vaultKeep, account)=>{
            return vaultKeep;
        }, newData).FirstOrDefault();
        return newVaultKeep;
    }

        internal List<VaultedKeep> GetKeepsInVault(int vaultId){
        string sql = @"
        SELECT
        keeps.*,
        vaultkeeps.*,
        accounts.*
        FROM vaultkeeps
        JOIN keeps ON vaultkeeps.keepId = keeps.id
        JOIN accounts ON vaultkeeps.creatorId = accounts.id
        WHERE vaultkeeps.vaultId = @vaultId;
        ";
        List<VaultedKeep> newVaultKeep = db.Query<VaultedKeep, VaultKeeps, Account, VaultedKeep>(sql, (vaultedKeep, vaultKeep, account)=>{
            vaultedKeep.Creator = account;
            vaultedKeep.VaultKeepId = vaultKeep.Id;
            return vaultedKeep;
        }, new{vaultId}).ToList();
        return newVaultKeep;
    }

    internal VaultKeeps GetOneVaultKeepById(int vaultKeepId){
        string sql = @"
        SELECT
        vaultkeeps.*,
        accounts.*
        FROM vaultkeeps
        JOIN accounts ON vaultkeeps.creatorId = accounts.id
        WHERE vaultkeeps.id = @vaultKeepId;
        ";
        VaultKeeps vaultKeeps = db.Query<VaultKeeps, Account, VaultKeeps>(sql, (vaultKeeps, account)=>{
            vaultKeeps.CreatorId = account.Id;
            return vaultKeeps;
        }, new{vaultKeepId}).FirstOrDefault();
        return vaultKeeps;
    }

    internal void DeleteVaultKeep(int vaultKeepId){
        string sql = @"
        DELETE FROM vaultkeeps
        WHERE id = @vaultKeepId
        ";
        db.Execute(sql, new{vaultKeepId});
    }
}