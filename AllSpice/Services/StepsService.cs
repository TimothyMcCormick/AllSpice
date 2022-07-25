using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class StepsService
  {

    private readonly RecipesService _rs;
    private readonly StepsRepository _repo;

    public StepsService(RecipesService rs, StepsRepository repo)
    {
      _rs = rs;
      _repo = repo;
    }

    internal List<Step> GetStepsByRecipeId(int recipeId)
    {
      _rs.GetById(recipeId);
      return _repo.GetStepsByRecipeId(recipeId);
    }

    internal Step Create(Step stepData, string id)
    {
      _rs.GetById(stepData.RecipeId);

      return _repo.Create(stepData);
    }

    internal Step GetById(int stepId)
    {
      Step found = _repo.GetById(stepId);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal Step Edit(Step stepData)
    {
      Step original = GetById(stepData.Id);

      original.Position = stepData.Position ?? original.Position;
      original.Body = stepData.Body ?? original.Body;

      _repo.Edit(original);

      return original;
    }

    internal Step Delete(int id, string userId)
    {
      Step original = GetById(id);

      _repo.Delete(id);

      return original;
    }
  }
}