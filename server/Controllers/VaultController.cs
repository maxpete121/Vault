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

    [HttpGet("{vaultId}")]
    public ActionResult<Vaults> GetVaultById(int vaultId){
        try
        {
            Vaults vaults = vaultService.GetVaultById(vaultId);
            return Ok(vaults);
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }

    [HttpPut("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<Vaults>> UpdateVault([FromBody]Vaults vaultData, int vaultId){
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            Vaults vaults = vaultService.UpdateVault(vaultData, vaultId, userInfo.Id);
            return Ok(vaults);
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }

    [HttpDelete("{vaultId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteVault(int vaultId){
        try
        {
          Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
          string message = vaultService.DeleteVault(vaultId, userInfo.Id);
          return Ok(message);
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }
}