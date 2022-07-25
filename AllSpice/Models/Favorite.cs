namespace AllSpice.Models
{
  public class Favorite
  {
    public int Id { get; set; }
    public string AccountId { get; set; }
    public int RecipeId { get; set; }
    public Profile Creator { get; set; }
  }
  public class AccountFavorite : Favorite
  {

    public int FavoriteId { get; set; }
  }
}
