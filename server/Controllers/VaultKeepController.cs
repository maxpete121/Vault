namespace Vault.Controllers;

[ApiController]
[Route("api/vaultkeeps")]

public class VaultKeepController : ControllerBase{
    private readonly Auth0Provider  auth;
    private readonly VaultKeepService vaultKeepService;

    public VaultKeepController(Auth0Provider auth, VaultKeepService vaultKeepService){
        this.auth = auth;
        this.vaultKeepService = vaultKeepService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeeps>> CreateVaultKeep([FromBody]VaultKeeps newData){
        try
        {
           Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
           newData.CreatorId = userInfo.Id;
           VaultKeeps vaultedKeep = vaultKeepService.CreateVaultKeep(newData);
           return Ok(vaultedKeep); 
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }

}