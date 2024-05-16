using System.ComponentModel.DataAnnotations;

namespace RecipesGlossary.Models;

public class ApiConnection
{
    public string Url { get; set; }
    public string User { get; set; }
    public string Password { get; set; }

}