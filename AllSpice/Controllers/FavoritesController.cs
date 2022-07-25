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
  [Authorize]
  public class FavoritesController : ControllerBase
  {
    private readonly FavoritesService _fs;

    public FavoritesController(FavoritesService fs)
    {
      _fs = fs;
    }

    [HttpGet]
    public async Task<ActionResult<List<Favorite>>> GetFavorites()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<Favorite> favorites = _fs.GetFavorites();
        return Ok(favorites);
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    [HttpPost]
    public async Task<ActionResult<Favorite>> CreateAsync([FromBody] Favorite favoriteData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        favoriteData.AccountId = userInfo.Id;
        Favorite favorite = _fs.Create(favoriteData);
        favorite.Creator = userInfo;
        return Ok(favorite);
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }
  }
}