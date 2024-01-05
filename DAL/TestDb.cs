using System.Text.Json;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;

namespace DAL;

public class TestDb
{
    public static void testingdb()
    {
        var contextOptions = new DbContextOptionsBuilder<RecipeDbContext>()
            .UseSqlite("Data Source=app.db")
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .Options;

        using var db = new RecipeDbContext(contextOptions);
        RepositoryEF repo = new RepositoryEF(db);
        
        
        db.Database.Migrate();
        
        db.Ingredients.Add(new Entities.Ingredient("Olio", "Condimento", "Scaffale", 50));
        db.Ingredients.Add(new Entities.Ingredient("Pasta", "Primo", "Scaffale", 50));
        db.Ingredients.Add(new Entities.Ingredient("Sale", "Condimento", "Scaffale", 50));
        db.Ingredients.Add(new Entities.Ingredient("Tonno", "Condimento", "Frigo", 50));
        
        db.Recipes.Add(new Entities.Recipe("Pasta al tonno", "{\"Pasta\": 100, \"Tonno\": 30}",1,15));
        
        db.Recipes.Add(new Entities.Recipe("Pasta al pomodoro", "{\"Pasta\": 100, \"Pomodoro\": 30}",1,25));
        
        db.Recipes.Add(new Entities.Recipe("Pasta al pesto", "{\"Pasta\": 100, \"Pesto\": 30}",1,10));
        
        var count = db.SaveChanges();
        Console.WriteLine("{0} records saved to database", count);
        

        Console.WriteLine();
        Console.WriteLine("All entries in database:");
        
        repo.GetAllIngredients();
    }
}