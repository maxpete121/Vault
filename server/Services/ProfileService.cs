namespace Vault.Services;

public class ProfileService(ProfileRepository repo){
    private readonly ProfileRepository repo = repo;

    internal Account GetUsersProfile(string profileId){
        Account profile = repo.GetUsersProfile(profileId);
        return profile;
    }

    internal List<Keeps> GetUsersKeeps(string profileId){
        List<Keeps> keeps = repo.GetUsersKeeps(profileId);
        return keeps;
    }

    internal List<Vaults> GetUserVaults(string profileId){
        List<Vaults> vaults = repo.GetUserVaults(profileId);
        return vaults;
    }
}