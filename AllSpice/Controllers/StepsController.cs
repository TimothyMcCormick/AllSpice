using System;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{

  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class StepsController : ControllerBase
  {
    private readonly StepsService _ss;

    public StepsController(StepsService ss)
    {
      _ss = ss;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Step>> Get(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Step step = _ss.GetById(id);
        return Ok(step);
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    [HttpPost]
    public async Task<ActionResult<Step>> Create([FromBody] Step stepData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Step step = _ss.Create(stepData, userInfo.Id);
        return Ok(step);
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Step>> EditAsync(int id, [FromBody] Step stepData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        stepData.Id = id;
        Step update = _ss.Edit(stepData);
        return Ok(update);
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Step>> DeleteAsync(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Step deletedStep = _ss.Delete(id, userInfo.Id);

        return Ok($"Successfully deleted step");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}