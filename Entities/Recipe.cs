using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Entities;

public class Recipe : BaseEntity
{
    public string Name { get; set; }
    public string Ingredients { get; set; }
    [NotMapped]
    public Dictionary<string, double> ListOfIngredients { get; set; }
    public int ForPeople { get; set; }
    public int Time { get; set; }
    
    public Recipe(string name, string ingredients, int forPeople, int time)
    {
        Name = name;
        Ingredients = ingredients;
        ForPeople = forPeople;
        Time = time;
        ListOfIngredients = new Dictionary<string, double>();
    }

    public void Recalculate(int npeople)
    {
        if (ForPeople == npeople)
        {
            Console.WriteLine("No need to recalculate");
        }
        else
        {
            var keysCopy = new List<string>(ListOfIngredients.Keys);

            foreach (var name in keysCopy)
            {
                double factor = npeople / ForPeople;
                double amount = ListOfIngredients[name];
                
                ListOfIngredients.Remove(name);
                
                ListOfIngredients.Add(name, amount * factor);
            }
        }
    }
    
}