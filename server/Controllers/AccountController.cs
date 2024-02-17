namespace Vault.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultService _vaultService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultService vaultService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _vaultService = vaultService;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaults")]
  [Authorize]
  public async Task<ActionResult<List<Vaults>>> GetMyVaults(){
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vaults> myVaults = _vaultService.GetMyVaults(userInfo.Id);
      return Ok(myVaults);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
}
