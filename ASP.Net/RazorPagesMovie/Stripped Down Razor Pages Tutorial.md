# ASP.Net RazorPages

> 1) Open Visual Studio
> 
> 2) Create a Razor Pages web app
> 	2.1) Choose New
> 	2.2) Select .Net Core
> 	2.3) Select Web Application
> 		2.3.1) Click <Web and Console>
> 		2.3.2) Click <App>
> 		2.3.3) Select <Web Application>
> 		2.3.4) Click <Next>
> 	2.4) Configure your Web Application
> 		2.4.1) Name your Project
> 			- Your_Name_Looks_Like_This
> 			- YourNameLooksLikeThis
> 		2.4.2) Choose Your Location
> 		2.4.3) Version Control
> 			- Checked if it is to become it’s own repository
> 			- Unchecked if it is to be a part of another repository
> 		2.4.4) Click <Create>
> 	2.5) Run the Application
> 		- If This is the first time running an app
> 			> “HTTPS development certificate was not found”
> 				+ Select <Yes>
> 				+ Enter Password
> 				+ Select <OK>
> 
> 3) Add a data Model
> 	3.1) Add a Models folder
> 		3.1.1) Right-click the Project folder
> 		3.1.2) Select <Add>
> 		3.1.3) Select <New Folder>
> 		3.1.4) Name the folder Models
> 		3.1.5) Select <Add>
> 	3.2) Add a Model
> 		3.2.1) Right-click the Models folder
> 		3.2.2) Select <Add>
> 		3.2.3) Select <New File>
> 		3.2.4) Select <General>
> 		3.2.5) Select <Empty Class>
> 		3.2.6) Name the class
> 		3.2.7) Select <New>
> 		3.2.8) Define the Model
> 				```
> 					using System;
> 					using System.ComponentModel.DataAnnotations;
> 
> 					namespace AppName.Models
> 					{
> 						public class ModelName
> 						{
> 							public var ID { get; set; }
> 							public var Name { get; set; }
> 						}
> 					}
> 				```
> 	3.3) Scaffold the Model
> 		3.3.1) In Terminal, navigate to the folder containing the .csproj file
> 			3.3.1.1) Enter these commands, separately
> 				3.3.1.1.1) ```dotnet tool install --global dotnet-ef```
> 				3.3.1.1.2) ```dotnet tool install --global dotnet-aspnet-codegenerator```
> 				3.3.1.1.3) ```dotnet add package Microsoft.EntityFrameworkCore.SQLite```
> 				3.3.1.1.4) ```dotnet add package Microsoft.EntityFrameworkCore.Design```
> 				3.3.1.1.5) ```dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design```
> 				3.3.1.1.6) ```dotnet add package Microsoft.EntityFrameworkCore.SqlServer```
> 				3.3.1.1.7) ```dotnet aspnet-codegenerator razorpage -m <ModelName> -dc <ModelNameContext> -udl -outDir Pages/<FolderName> —referenceScriptLibraries```
> 		3.3.2) Modify the Startup.cs and the appsettings.json for connection string and SQL connection
> 			3.3.2.1) Modify the Startup.cs file
> 				3.3.2.1.1) Add to the Startup.cs ConfigureServices method
> 					```
> 						public void ConfigureServices(IServiceCollection services)
> 						{
> 							services.AddRazorPages();
> 							
> 							services.AddDbContext<ModelNameContext>(options =>
> 								options.UseSqlite(Configuration.GetConnectionString(“ModelContext”)));
> 						}
> 					```
> 			3.3.2.2) Modify the appsettings.json
> 				3.3.2.2.1) Add to the appsettings.json file
> 					```
> 						{
> 							…
> 							},
> 							“AllowedHosts”: “*”,
> 							“ConnectionStrings”: {
> 								“ModelContext”: “Data Source=MvcModel.db”
> 							}
> 						}
> 					```
> 	3.4) Initial Migration
> 		3.4.1) Return to the Terminal
> 		3.4.2) ```dotnet ef migrations add InitialCreate```
> 		3.4.3) ```dotnet ef database update```
> 		3.4.4) Test the app
> 			3.4.4.1) ```dotnet run```
> 			3.4.4.2) navigate to https://localhost:5001<FolderName>
> 			3.4.4.3) Test the Create Link
> 			3.4.4.4) Test the Edit/Details/Delete links
> 
> 4) Scaffolded Pages
> 	4.1) Update the navigation
> 		4.1.1) Pages/Shared/_Layout.cshtml	
> 			4.1.1.1) Find:
> 				```
> 					<a class=“navbar-brand” asp-area=“” asp-page=“/Index”>AppName</a>
> 				```
> 			4.1.1.2) Change to:
> 				```
> 					<a class=“nabber-brand” asp-page=“/FolderName/Index”>AppName</a>
> 				```
> 			4.1.1.3) Remove all ```asp-area=“”``` from the Layout
> 
> 5) Database functions
> 	5.1) Seed the database
> 		5.1.1) Create a new class named SeedData in the Models Folder
> 			5.1.1.1) Right-click Models
> 			5.1.1.2) Select Add
> 			5.1.1.3) Select New Class
> 			5.1.1.4) Name it SeedData
> 			5.1.1.5) Select Create
> 		5.1.2) Fill out SeedData with sample Data
> 			```
> 				using Microsoft.EntityFrameworkCore;
> 				using Microsoft.Extensions.DependencyInjection;
> 				using System;
> 				using System.Linq;
> 
> 				namespace RazorPagesMovie.Models
> 				{
>     				public static class SeedData
>     				{
>         				public static void Initialize(IServiceProvider serviceProvider)
>         				{
>            				using (var context = new MvcMovieContext(serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()))
>             				{
>                				// Look for any movies.
>                				if (context.Movie.Any())
>                				{
>                    				return;   // DB has been seeded
>                				}
>
>                				context.Movie.AddRange(
>                					new Movie
>                					{
>                						Title = "When Harry Met Sally",
>                						ReleaseDate = DateTime.Parse("1989-2-12"),
>                						Genre = "Romantic Comedy",
>                						Price = 7.99M
>                					}
>                				);
>                				context.SaveChanges();
>            				}
>        				}
>    				}	
> 				}
> 			```
> 		5.1.3) Update the Database
> 			5.1.3.1) ```dotnet ef database update```
> 			5.1.3.2) ```dotnet run```
> 		5.1.4) Update the Program.cs
> 			5.1.4.1) ADD this to the Program.cs
> 				```
> 					using Microsoft.AspNetCore.Hosting;
> 					using Microsoft.Extensions.DependencyInjection;
> 					using Microsoft.Extensions.Hosting;
> 					using Microsoft.Extensions.Logging;
> 					using RazorPagesMovie.Models;
> 					using System;
> 
> 					namespace RazorPagesMovie
> 					{
>     					public class Program
>    					{
>        					public static void Main(string[] args)
>        					{
>            					var host = CreateHostBuilder(args).Build();
> 
> 
> 								using (var scope = host.Services.CreateScope())
> 								{
>                					var services = scope.ServiceProvider;
>
>                					try
>                					{
>                					    SeedData.Initialize(services);
>                					}
>                					catch (Exception ex)
>                					{
>                    					var logger = services.GetRequiredService<ILogger<Program>>();
>                    					logger.LogError(ex, "An error occurred seeding the DB.");
>                					}
>            					}
>            					host.Run();
>        					}
>
> 							public static IHostBuilder CreateHostBuilder(string[] args) =>
>            					Host.CreateDefaultBuilder(args)
> 								.ConfigureWebHostDefaults(webBuilder =>
>                				{
> 									webBuilder.UseStartup<Startup>();
> 								});
>     					}
> 					}
> 				```
> 			5.4.1.2) ```dotnet run```
> 
> 6) Update the generated pages
> 	6.1) Update the Model
> 		6.1.1) Open the AppModel
> 		6.1.2) Add
> 			```
> 				using System.ComponentModel.DataAnnotations.Schema;
> 			```
> 		6.1.3) ```dotnet ef database update```
> 		6.1.4) ```dotnet run```
> 	6.2) Add route template
> 		6.2.1) For each of the .cshtml pages, replace:
> 			```@page``` with ```@page “{id:int}”```
> 		6.2.2) dotnet run
> 
> 7) Add a Search function
> 	7.1) Modify the Index.cshtml.cs to add Search parameters
> 		7.1.1) Add to the top of the Index.cshtml.cs 
> 			```
> 				using Microsoft.AspNetCore.Mvc.Rendering;
> 				using System.Linq;
> 			```
> 		7.1.2) Add this below the ```public IList<Model> Model  { get; set; }```
> 			```
> 				[BindPRoperty(SupportsGet = true)]
> 				public string SearchString { get; set; }
> 				public SelectList Category { get; set; }
> 				[BindProperty(SupportsGet = true)]
> 				public string Category { get; set; }
> 			```
> 		7.1.3) Replace the ```public async…``` with this:
> 			```
> 				public async Task OnGetAsync()
> 				{
> 					IQueryable<string> flavorQuery = from m in _context.Model
> 															orderby m.Genre
> 															select m.Genre;
> 					var items = from m in _context.Model
> 									select m;
> 
> 					if (!string.IsNullOrEmpty(SearchString))
> 					{
> 						items = items.Where(s => s.parameter.Contains(SearchString));
> 					}
> 					
> 					if (!string.IsNullOrEmpty(ModelFlavor))
> 					{
> 						items = items.Where(x => x.Flavor == ModelFlavor);
> 					}
> 					Flavors = new SelectList(await flavorQuery.Distinct().ToListAsync());
> 					Item = await items.ToListAsync();
> 				}
> 			```
> 	7.2) Modify the Index.cshtml file
> 		7.2.1) Add a “flavor” dropdown
> 			```
> 				<form>
> 					<p>
> 						<select asp-for=“ModelFlavor” asp-items=“Model.Flavors”>
> 							<option value=“”>
> 								All
> 							</option>
> 						</select>
> 					…
> 			```
> 		7.2.2) Add a Search Field
> 			```
> 				…
> 				Title: <input type=“text” asp-for=“SearchString” />
> 						<input type=“submit” value=“Filter” />
> 					</p>
> 				</form>
> 			```
> 
> 8) Add a new Field
> 	8.1) In the Item.cs model file, add a new property:
> 		```
> 			public class Item
> 			{
> 				…
> 				public string Property { get; set; }
> 				…
> 			}
> 		```
> 	8.2) ```dotnet ef database drop```
> 	8.3) ```dotnet ef migrations add InitialCreate```
> 	8.4) ```dotnet ef database update```
> 	8.5) Add the new property field to the Index.cshtml.cs
> 		```
> 			…
> 			<th>
> 				@Html.DisplayNameFor(model => model.Item[0].Property)
> 			</th>
> 			…
> 			<td>
> 				@Html.DisplayFor(modelItem => item.Property)
> 			</td>
> 			…
> 		```
> 
> 9) Adding Validation
> 	9.1) Generalize this code later; Modify the Model.cs file:
> 		```
> 			public class Movie
> 			{
>     			public int ID { get; set; }
>      			[StringLength(60, MinimumLength = 3)]
>     			[Required]
>     			public string Title { get; set; }
> 
>     			[Display(Name = "Release Date")]
>     			[DataType(DataType.Date)]
>  				public DateTime ReleaseDate { get; set; }
> 
>     			[Range(1, 100)]
>     			[DataType(DataType.Currency)]
>     			[Column(TypeName = "decimal(18, 2)")]
>     			public decimal Price { get; set; }
> 
>     			[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
>     			[Required]
>     			[StringLength(30)]
>     			public string Genre { get; set; }
> 
> 				[RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
>     			[StringLength(5)]
>     			[Required]
> 				public string Rating { get; set; }
> 			}
> 		```