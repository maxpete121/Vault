namespace Vault.Controllers;

[ApiController]
[Route("api/keeps")]
public class KeepController : ControllerBase{
    private readonly Auth0Provider  auth;
    private readonly KeepService keepService;

    public KeepController(Auth0Provider auth, KeepService keepService){
        this.auth = auth;
        this.keepService = keepService;
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keeps>> CreateKeep([FromBody] Keeps keepData){
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keeps keeps = keepService.CreateKeep(keepData);
            return Ok(keeps);
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Keeps>> GetAllKeeps(){
        try
        {
        List<Keeps> keeps = keepService.GetAllKeeps();
        return Ok(keeps);
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }

    [HttpGet("{keepId}")]
    public ActionResult<Keeps> GetOneKeepById(int keepId){
        try
        {
           Keeps keeps = keepService.GetOneKeepById(keepId);
           return Ok(keeps); 
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }
}