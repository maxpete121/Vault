namespace Vault.Controllers;

[ApiController]
[Route("api/vaults")]

public class VaultController : ControllerBase{
    private readonly Auth0Provider  auth;
    private readonly VaultService vaultService;

    public VaultController(Auth0Provider auth, VaultService vaultService){
        this.auth = auth;
        this.vaultService = vaultService;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Vaults>> CreateVault([FromBody] Vaults vaultData){
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vaults newVault = vaultService.CreateVault(vaultData);
            return Ok(newVault);
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }
}