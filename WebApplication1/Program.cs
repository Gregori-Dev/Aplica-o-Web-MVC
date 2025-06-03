using Application.Intefaces;
using Infra.Autenticação;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AplicacaoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient();
builder.Services.AddTransient<TMDBClientServico>();
builder.Services.AddScoped<FilmeRepository>();

builder.Services.Configure<JwtConfiguracao>(builder.Configuration.GetSection("ConfiguracaoJwt"));
builder.Services.AddScoped<IAutenticacaoServico, AutenticacaoServico>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var config = builder.Configuration.GetSection("ConfiguracaoJwt").Get<JwtConfiguracao>();
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config.Emissor,
            ValidAudience = config.Publico,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Senha))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Filmes/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();  
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Filmes}/{action=Index}/{id?}");

app.Run();
