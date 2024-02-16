namespace Vault.Controllers;

[ApiController]
[Route("api/profiles")]

public class ProfileController : ControllerBase{
    private readonly Auth0Provider auth;
    private readonly ProfileService profileService;
    public ProfileController(Auth0Provider auth, ProfileService profileService){
        this.auth = auth;
        this.profileService = profileService;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Account> GetUsersProfile(string profileId){
        try
        {
            Account profile = profileService.GetUsersProfile(profileId);
            return profile;
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keeps>> GetUsersKeeps(string profileId){
        try
        {
            List<Keeps> keeps = profileService.GetUsersKeeps(profileId);
            return Ok(keeps);
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }

    [HttpGet("{profileId}/vaults")]
    public ActionResult<List<Vaults>> GetUserVaults(string profileId){
        try
        {
          List<Vaults> vaults = profileService.GetUserVaults(profileId);
          return vaults;  
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }
}