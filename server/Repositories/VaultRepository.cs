namespace Vault.Repositories;


public class VaultsRepository(IDbConnection db){
    private readonly IDbConnection db = db;

    internal Vaults CreateVault(Vaults vaultData){
        string sql = @"
        INSERT INTO vaults
        (creatorId ,name, description, img, isPrivate)
        VALUES
        (@creatorId, @name, @description, @img, @isPrivate);

        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON vaults.creatorId = accounts.id
        WHERE vaults.id = LAST_INSERT_ID();
        ";
        Vaults vaults = db.Query<Vaults, Account, Vaults>(sql, (vault, account)=>{
            vault.Creator = account;
            return vault;
        }, vaultData).FirstOrDefault();
        return vaults;
    }

    internal Vaults GetVaultById(int vaultId){
        string sql=@"
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON vaults.creatorId = accounts.id
        WHERE vaults.id = @vaultId
        ";
        Vaults vaults = db.Query<Vaults, Account, Vaults>(sql, (vault, account)=>{
            vault.Creator = account;
            return vault;
    }, new{vaultId}).FirstOrDefault();
    return vaults;
    }

    internal Vaults UpdateVault(Vaults vaultData){
        string sql = @"
        UPDATE vaults SET
        name = @name,
        description = @description,
        img = @img,
        isPrivate = @isPrivate
        WHERE id = @id;

        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON vaults.creatorId = accounts.id
        WHERE vaults.id = @id
        ";
        Vaults updatedVault = db.Query<Vaults, Account, Vaults>(sql, (vault, account)=>{
            vault.Creator = account;
            return vault;
        }, vaultData).FirstOrDefault();
        return updatedVault;
    }

    internal void DeleteVault(int vaultId){
        string sql = @"
        DELETE FROM vaults
        WHERE id = @vaultId
        ";
        db.Execute(sql, new{vaultId});
    }
}