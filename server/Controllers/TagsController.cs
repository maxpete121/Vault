namespace Vault.Controllers;

[ApiController]
[Route("api/tags")]

public class TagsController : ControllerBase{
    private readonly Auth0Provider auth;
    private readonly TagsService tagsService;

    public TagsController(Auth0Provider auth, TagsService tagsService){
        this.auth = auth;
        this.tagsService = tagsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Tags>> CreateTag([FromBody]Tags tagData){
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            Tags tags = tagsService.CreateTag(tagData, userInfo.Id);
            return Ok(tags);
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }

    [HttpGet("{keepId}")]
    public ActionResult<List<Tags>> GetTagsByKeep(int keepId){
        try
        {
           List<Tags> tags = tagsService.GetTagsByKeep(keepId);
           return Ok(tags); 
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }

    [HttpGet("{tagId}/tag")]
    public ActionResult<Tags> GetTagById(int tagId){
        try
        {
            Tags tags = tagsService.GetTagById(tagId);
            return Ok(tags);
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    }

    [HttpDelete("{tagId}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteTag(int tagId){
        try
        {
            Account userInfo = await auth.GetUserInfoAsync<Account>(HttpContext);
            string message = tagsService.DeleteTag(tagId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception error)
        {
            
            return BadRequest(error.Message);
        }
    } 
}