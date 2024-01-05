using Microsoft.EntityFrameworkCore;

namespace DAL;

public class RecipeDbContext: DbContext
{
    public DbSet<Entities.Ingredient> Ingredients { get; set; } = default!;
    public DbSet<Entities.Recipe> Recipes { get; set; } = default!;

    public RecipeDbContext(DbContextOptions options) : base(options)
    {
    }

}