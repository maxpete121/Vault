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
           VaultKeeps vaultedKeep = vaultKeepService.CreateVaultKeep(newData, userInfo.Id);
           return Ok(vaultedKeep); 
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }

    [HttpDelete("{vaultKeepId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId){
        try
        {
             Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
             string message = vaultKeepService.DeleteVaultKeep(vaultKeepId, userInfo.Id);
             return message;
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }

}