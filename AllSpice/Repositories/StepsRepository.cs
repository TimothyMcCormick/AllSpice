using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
  public class StepsRepository
  {

    private readonly IDbConnection _db;

    public StepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Step GetById(int stepId)
    {
      string sql = @"
      SELECT
      *
      FROM steps
      WHERE steps.id = @stepId
      ";
      return _db.Query<Step>(sql, new { stepId }).FirstOrDefault();
    }

    internal List<Step> GetStepsByRecipeId(int recipeId)
    {
      string sql = "SELECT * FROM steps WHERE recipeId = @recipeId";
      return _db.Query<Step>(sql, new { recipeId }).ToList();
    }

    internal Step Create(Step stepData)
    {
      string sql = @"
      INSERT INTO steps
      (position, body, recipeId)
      VALUES
      (@Position, @Body, @RecipeId);
      SELECT LAST_INSERT_ID();
      ";

      int id = _db.ExecuteScalar<int>(sql, stepData);
      stepData.Id = id;
      return stepData;
    }

    internal void Edit(Step original)
    {
      string sql = @"
      UPDATE steps
      SET
      position = @Position,
      body = @Body
      WHERE id = @Id
      ";
      _db.Execute(sql, original);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM steps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}