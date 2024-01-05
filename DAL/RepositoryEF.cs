using System.Text.Json;
using Entities;

namespace DAL;

public class RepositoryEF
{
    private readonly RecipeDbContext _ctx;

    public RepositoryEF(RecipeDbContext ctx)
    {
        _ctx = ctx;
    }

    public void AddIngredient(Ingredient i)
    {
        _ctx.Ingredients.Add(i);
        _ctx.SaveChanges();
    }

    public Ingredient GetIngredient(string name)
    {
        //Console.WriteLine("RETRIEVING INGREDIENT "+name);
        Ingredient i = _ctx.Ingredients.FirstOrDefault(i => i.Name == name);

        return i;
    }

    public List<Ingredient> GetAllIngredients()
    {
        List<Ingredient> ingredients = _ctx.Ingredients.ToList();
        Console.WriteLine("INGREDIENTS IN DB");
        foreach (var i in ingredients)
        {
            Console.WriteLine(i.Name);
        }
        return ingredients;
    }

    public List<Recipe> GetAllRecipes()
    {
        List<Recipe> recipes = _ctx.Recipes.ToList();
        Console.WriteLine("RECIPES IN DB");
        
        var jsonOptions = new JsonSerializerOptions()
        {    
            WriteIndented = true,
            AllowTrailingCommas = true,
        };

        foreach (var r in recipes)
        {
            Console.WriteLine(r.Ingredients);
            Dictionary<string, int> json = JsonSerializer.Deserialize<Dictionary<string, int>>(r.Ingredients, jsonOptions);

            foreach (var p in json.Keys)
            {
                Ingredient i = GetIngredient(p);
                r.ListOfIngredients.Add(p,json[p]);
            } 
        }
            
        return recipes;
    }
    
    public List<Recipe> GetAllRecipes(int time, string no)
    {
        List<Ingredient> ListOfIngredientsInDB = GetAllIngredients();
        List<string> NoIngredients = new List<string>();
        if (no !="")
            NoIngredients = no.Split(',').ToList();
        
        List<string> list = new List<string>();

        foreach (var i in ListOfIngredientsInDB)
        {
            list.Add(i.Name);
        }
        
        List<Recipe> recipes = _ctx.Recipes
            .Where(r => (r.Time <= time))
            .ToList();

        var jsonOptions = new JsonSerializerOptions()
        {    
            WriteIndented = true,
            AllowTrailingCommas = true,
        };

        foreach (var r in recipes)
        {
            Console.WriteLine(r.Ingredients);
            Dictionary<string, int> json = JsonSerializer.Deserialize<Dictionary<string, int>>(r.Ingredients, jsonOptions);

            foreach (var p in json.Keys)
            {
                Ingredient i = GetIngredient(p);
                r.ListOfIngredients.Add(p, json[p]);
            } 
        }

        List<Recipe> validRecipes = new List<Recipe>();
        
        foreach (var r in recipes)
        {
            Dictionary<string, double> ingr = r.ListOfIngredients;

            
            if (ingr.Keys.All(element => list.Contains(element)))
            {
                foreach (var x in ingr.Keys)
                {
                    Ingredient i = ListOfIngredientsInDB.Where(item => item.Name == x).First();
                    if (ingr[x] <= i.Amount)
                    {
                        Console.WriteLine("IT IS "+ingr[x]+" AND IT IS "+i.Amount);
                        validRecipes.Add(r);
                    }
                }
                
            }
        }

        if (NoIngredients != null && NoIngredients.Count != 0)
        {
            foreach (var n in NoIngredients)
            {
                foreach (var r in validRecipes)
                {
                    if (r.ListOfIngredients.Keys.ToList().Exists(item => item == n))
                    {
                        Console.WriteLine("REMOVING RECIPE WITH INGR "+n);
                        validRecipes.Remove(r);
                        break;
                    }
                }
            }
        }
        

        return validRecipes;
    }



    
}