using System;
using System.Collections.Generic;
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
  public class RecipesController : ControllerBase
  {
    private readonly RecipesService _rs;
    private readonly IngredientsService _ings;
    private readonly StepsService _ss;

    private readonly FavoritesService _fs;

    public RecipesController(RecipesService rs, IngredientsService ings, StepsService ss, FavoritesService fs)
    {
      _rs = rs;
      _ings = ings;
      _ss = ss;
      _fs = fs;
    }

    [HttpGet]
    public ActionResult<List<Recipe>> Get(string query = "")
    {
      try
      {
        List<Recipe> recipes = _rs.GetAll();
        return Ok(recipes);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Recipe> Get(int id)
    {
      try
      {
        Recipe recipe = _rs.GetById(id);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/ingredients")]
    public ActionResult<Ingredient> GetRecipeIngredients(int id)
    {
      try
      {
        List<Ingredient> ingredients = _ings.GetIngredientsByRecipeId(id);
        return Ok(ingredients);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/steps")]
    public ActionResult<Step> GetRecipeSteps(int id)
    {
      try
      {
        List<Step> steps = _ss.GetStepsByRecipeId(id);
        return Ok(steps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/favorites")]
    public ActionResult<Ingredient> GetRecipeFavorites(int id)
    {
      try
      {
        List<Favorite> favorites = _fs.GetRecipeFavorites(id);
        return Ok(favorites);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }




    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Recipe>> CreateAsync([FromBody] Recipe recipeData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        recipeData.CreatorId = userInfo.Id;
        Recipe newRecipe = _rs.Create(recipeData);

        newRecipe.Creator = userInfo;
        newRecipe.CreatedAt = new DateTime();
        newRecipe.UpdatedAt = new DateTime();

        return Ok(newRecipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Recipe>> EditAsync(int id, [FromBody] Recipe recipeData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        recipeData.CreatorId = userInfo.Id;
        recipeData.Id = id;
        Recipe update = _rs.Edit(recipeData);

        return Ok(update);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Recipe>> DeleteAsync(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Recipe deletedRecipe = _rs.Delete(id, userInfo.Id);

        return Ok(deletedRecipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}