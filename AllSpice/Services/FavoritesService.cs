using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class FavoritesService
  {

    private readonly RecipesService _rs;
    private readonly FavoritesRepository _repo;

    public FavoritesService(RecipesService rs, FavoritesRepository repo)
    {
      _rs = rs;
      _repo = repo;
    }

    internal List<Favorite> GetFavorites()
    {
      return _repo.GetFavorites();
    }

    internal Favorite Create(Favorite favoriteData)
    {
      return _repo.Create(favoriteData);
    }
    internal List<Favorite> GetAccountFavorites(string userId)
    {
      return _repo.GetAccountFavorites(userId);
    }
    internal List<Favorite> GetRecipeFavorites(int recipeId)
    {
      _rs.GetById(recipeId);
      return _repo.GetRecipeFavorites(recipeId);
    }


  }
}