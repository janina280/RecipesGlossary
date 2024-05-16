using System.ComponentModel.DataAnnotations;

namespace RecipesGlossary.Models;

public class RecipeModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public int CookingTime { get; set; }

    public int PreparationTime { get; set; }

    public string SkillLevel { get; set; }

    public string Description { get; set; }

}