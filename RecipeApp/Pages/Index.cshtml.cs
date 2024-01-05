using DAL;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecipeApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly RepositoryEF _repository;
    public List<Recipe> ListOfRecipes { get; set; }
    public List<Ingredient> ListOfIngredients { get; set; }
    [BindProperty]
    public string YesListOfIngredients { get; set; }
    [BindProperty]
    public string NoListOfIngredients { get; set; }
    [BindProperty]
    public int NPeople { get; set; }
    [BindProperty]
    public int Time { get; set; }

    public IndexModel(ILogger<IndexModel> logger, RecipeDbContext ctx)
    {
        _logger = logger;
        _repository = new RepositoryEF(ctx);
        ListOfIngredients = _repository.GetAllIngredients();
        //TestDb.testingdb();
    }

    public void OnGet()
    {
        ListOfIngredients = _repository.GetAllIngredients();
        
        if ((ListOfRecipes == null) || (ListOfRecipes.Count == 0))
            ListOfRecipes = _repository.GetAllRecipes();
    }
    
    public IActionResult OnPostSetData()
    {
        //var selectedIngredients = Request.Form["selectedIngredients"];
        //Console.WriteLine("DATA");
        //Console.WriteLine(YesListOfIngredients);
        //Console.WriteLine(NoListOfIngredients);

        if (NoListOfIngredients == null)
        {
            NoListOfIngredients = "";
        }
        
        ListOfRecipes = _repository.GetAllRecipes(Time, NoListOfIngredients);

        foreach (var r in ListOfRecipes)
        {
            r.Recalculate(NPeople);
        }

        return Page();
    }
    
    
}