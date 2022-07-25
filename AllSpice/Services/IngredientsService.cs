using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class IngredientsService
  {
    private readonly RecipesService _rs;
    private readonly IngredientsRepository _repo;

    public IngredientsService(RecipesService rs, IngredientsRepository repo)
    {
      _rs = rs;
      _repo = repo;
    }

    internal Ingredient GetById(int ingredientId)
    {
      Ingredient found = _repo.GetById(ingredientId);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal Ingredient Create(Ingredient ingredientData, string userId)
    {
      _rs.GetById(ingredientData.RecipeId);

      return _repo.Create(ingredientData);
    }

    internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
    {
      _rs.GetById(recipeId);
      return _repo.GetIngredientsByRecipeId(recipeId);
    }

    internal Ingredient Edit(Ingredient ingredientData)
    {
      Ingredient original = GetById(ingredientData.Id);

      original.Name = ingredientData.Name ?? original.Name;
      original.Quantity = ingredientData.Quantity ?? original.Quantity;

      _repo.Edit(original);

      return original;
    }



    internal Ingredient Delete(int id, string userId)
    {
      Ingredient original = GetById(id);

      _repo.Delete(id);

      return original;
    }
  }
}