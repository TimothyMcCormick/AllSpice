using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{


  public class FavoritesRepository
  {
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Favorite> GetFavorites()
    {
      string sql = @"
      SELECT
      f.*,
      acts.*
      FROM favorites f
      JOIN accounts acts ON acts.id = f.accountId
      ";
      return _db.Query<Favorite, Profile, Favorite>(sql, (favorite, profile) =>
      {
        favorite.Creator = profile;
        return favorite;
      }).ToList();
    }

    internal List<Favorite> GetRecipeFavorites(int recipeId)
    {
      string sql = "SELECT * FROM favorites WHERE RecipeId = @recipeId";




      return _db.Query<Favorite>(sql, new { recipeId }).ToList();
    }



    internal Favorite Create(Favorite favoriteData)
    {
      string sql = @"
            INSERT INTO favorites
            (recipeId, accountId  )
            VALUES
            (@RecipeId, @AccountId );
            SELECT LAST_INSERT_ID();";
      favoriteData.Id = _db.ExecuteScalar<int>(sql, favoriteData);
      return favoriteData;
    }
    internal List<Favorite> GetAccountFavorites(string userId)
    {
      string sql = "SELECT * FROM favorites WHERE accountId = @userId";
      return _db.Query<Favorite>(sql, new { userId }).ToList();
    }
  }
}