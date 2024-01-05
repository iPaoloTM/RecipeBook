using DAL;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecipeApp.Pages;

public class IngredientModel : PageModel
{
    private readonly ILogger<IngredientModel> _logger;
    private readonly RepositoryEF _repository;

    [BindProperty]
    public string Name { get; set; } 
    [BindProperty]
    public string Category { get; set; } 
    [BindProperty]
    public string Location { get; set; } 
    [BindProperty]
    public int Amount { get; set; }

    public List<Ingredient> ListOfIngredients { get; set; }
    public List<Recipe> ListOfRecipes { get; set; }

    public IngredientModel(ILogger<IngredientModel> logger, RecipeDbContext ctx)
    {
        _logger = logger;
        _repository = new RepositoryEF(ctx);
    }

    public void OnGet()
    {
        ListOfIngredients = _repository.GetAllIngredients();

        ListOfRecipes = _repository.GetAllRecipes();
    }

    public IActionResult OnPostAddIngredient()
    {
        
        
        Console.WriteLine("Adding ingredient");
        Console.WriteLine("Name:"+Name);
        Console.WriteLine("Category: "+Category);
        Console.WriteLine("Location: "+Location);
        Console.WriteLine("Amount: "+Amount);
        Ingredient i = new Ingredient(Name, Category, Location, (int)Amount);

        _repository.AddIngredient(i);
        
        ListOfIngredients = _repository.GetAllIngredients();

        return Page();
    }
}