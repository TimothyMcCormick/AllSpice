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
  public class IngredientsController : ControllerBase
  {
    private readonly IngredientsService _ings;

    public IngredientsController(IngredientsService ings)
    {
      _ings = ings;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ingredient>> Get(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Ingredient ingredient = _ings.GetById(id);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient ingredientData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Ingredient ingredient = _ings.Create(ingredientData, userInfo.Id);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Ingredient>> EditAsync(int id, [FromBody] Ingredient ingredientData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        ingredientData.Id = id;
        Ingredient update = _ings.Edit(ingredientData);
        return Ok(update);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Ingredient>> DeleteAsync(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Ingredient deletedIngredient = _ings.Delete(id, userInfo.Id);

        return Ok($"Successfully deleted ingredient");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}