var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<IRegistraionService, RegistraionService>();
builder.Services.AddScoped<IRegistraionService, RegistraionService>();
//builder.Services.AddTransient<IRegistraionService, RegistraionService>();

//builder.Services.AddKeyedScoped<IRegistraionService, RegistraionService1>("RegistraionService1");
//builder.Services.AddKeyedScoped<IRegistraionService, RegistraionService2>("RegistraionService2");
//builder.Services.AddKeyedScoped<IRegistraionService, RegistraionService3>("RegistraionService3");

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
