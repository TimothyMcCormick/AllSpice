using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
  public class RecipesRepository
  {

    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Recipe> GetAll(string query = "")
    {
      string stringQuery = "%" + query + "%";
      string sql = @"
      SELECT
      rec.*,
      act.*
      FROM recipes rec
      JOIN accounts act ON rec.creatorId = act.id
      WHERE title LIKE @stringQuery
      ";
      return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, prof) =>
      {
        recipe.Creator = prof;
        return recipe;
      }, new { stringQuery }).ToList();
    }

    internal Recipe GetById(int recipeId)
    {
      string sql = @"
      SELECT
      rec.*,
      act.*
      FROM recipes rec
      JOIN accounts act ON rec.creatorId = act.id
      WHERE rec.id = @recipeId
      ";
      return _db.Query<Recipe, Profile, Recipe>(sql, (recipe, prof) =>
      {
        recipe.Creator = prof;
        return recipe;
      }, new { recipeId }).FirstOrDefault();
    }

    internal Recipe Create(Recipe recipeData)
    {
      string sql = @"
      INSERT INTO recipes
      (picture, title, subtitle, category, creatorId)
      VALUES
      (@Picture, @Title, @Subtitle, @Category, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, recipeData);
      recipeData.Id = id;
      return recipeData;
    }

    internal void Edit(Recipe original)
    {
      string sql = @"
      UPDATE recipes
      SET

        picture = @Picture,
        title = @Title,
        subtitle = @Subtitle,
        category = @Category
      WHERE id = @Id
      ";
      _db.Execute(sql, original);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM recipes WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }


  }
}