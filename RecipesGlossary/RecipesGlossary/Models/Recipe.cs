using System.ComponentModel.DataAnnotations;

namespace RecipesGlossary.Models;

public class Recipe
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }

    public string SkillLevel { get; set; }

    public string NumberOfIngredients { get; set; }

}