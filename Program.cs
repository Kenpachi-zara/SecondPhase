using SecondPhase.Middleware;
using SecondPhase.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();  // extension method registers the services required for MVC controllers.

builder.Services.AddScoped<IScopedServiceExample, ScopedServiceExample>();
builder.Services.AddSingleton<ISingletonServiceExample, SingletonServiceExample>();
builder.Services.AddTransient<ITransientServiceExample, TransientServiceExample>();
builder.Services.AddKeyedSingleton<ISingletonServiceExample, SingletonServiceExample>("KeySingleton"); // helps with multiple implementations of the same service
// [FromKeyedServices("KeySingleton")] ISingletonServiceExample singletonService // this is how you inject in constructor

builder.Services.AddSingleton<ITransientDependencyA, TransientDependencyA>();
builder.Services.AddScoped<ITransientDependencyB, TransientDependencyB>();

//Request delegates are configured using Run, Map, and Use extension methods. 
// Run delegates don't receive a next parameter. The first Run delegate is always terminal and terminates the pipeline. Run is a convention.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

/*
Map extensions are used as a convention for branching the pipeline. Map branches the request pipeline based on matches of the given request path. 
*/
    
app.Map("/middleware", HandleMapTest1);

static void HandleMapTest1(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Exeucted using middleware based on path");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    // Do work that can write to the Response.
    await next.Invoke();
    // Do logging or other work that doesn't write to the Response.
});

/*
app.Run(async context =>
{
    await context.Response.WriteAsync("Hello from 2nd delegate.");
});
*/

app.UseMyMiddleware(); // custom Middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();