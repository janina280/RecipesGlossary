using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Neo4j.Driver;
using Neo4j.Driver.Mapping;
using RecipesGlossary.Models;
using RecipesGlossary.States;

namespace RecipesGlossary.Pages.HomePage;

public partial class HomeComponent : ComponentBase, IDisposable
{
    [Inject]
    private SnackbarState SnackbarState { get; set; }

    [Inject]
    private LoadingState LoadingState { get; set; }

    [Inject]
    private IConfiguration Configuration { get; set; }

    private IDriver Driver { get; set; }

    private List<Recipe> _recipes = new List<Recipe>();

    private Recipe _selectedRow = new Recipe();

    public void Dispose()
    {
        SnackbarState.OnStateChange -= StateHasChanged;
        LoadingState.OnStateChange -= StateHasChanged;

        Driver?.Dispose();
    }
    protected override async Task OnInitializedAsync()
    {
        SnackbarState.OnStateChange += StateHasChanged;
        LoadingState.OnStateChange += StateHasChanged;

        var connection = new ApiConnection();
        Configuration.GetSection("Neo4j").Bind(connection);
        Driver = GraphDatabase.Driver(new Uri(connection.Url), AuthTokens.Basic(connection.User, connection.Password));
        if (!await Driver.TryVerifyConnectivityAsync())
        {
            throw new Exception("Connection failed!");
        };
        await GetDataAsync();
    }

    private async Task GetDataAsync()
    {
        await LoadingState.ShowAsync();
        
        await using var session = Driver.AsyncSession();

        var cursor = await session.RunAsync(new Query(@"MATCH (author:Author)-[:WROTE]->(recipe:Recipe)
RETURN author.name as Author, recipe.id as Id, recipe.name as Name, recipe.skillLevel as SkillLevel,
COUNT { (recipe)-[:CONTAINS_INGREDIENT]-(:Ingredient) } as NumberOfIngredients"));
        if (await cursor.FetchAsync())
        {
            _recipes = (await cursor.ToListAsync<Recipe>()).ToList();
        }

        await LoadingState.HideAsync();
    }
}