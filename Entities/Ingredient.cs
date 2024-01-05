namespace Entities;

public class Ingredient : BaseEntity
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string Location { get; set; }
    public double Amount { get; set; }

    public Ingredient(string name, string category, string location, double amount)
    {
        Name = name;
        Category = category;
        Location = location;
        Amount = amount;
    }
}