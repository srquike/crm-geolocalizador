using AutoMapper;
using LevantamientoDeRed.Database;
using LevantamientoDeRed.Entities;
using LevantamientoDeRed.Perfiles;
using LevantamientoDeRed.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Globalization;
using System.Security.Claims;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Aplicacion"), provider =>
    {
        provider.EnableRetryOnFailure();
        provider.UseNetTopologySuite();
    });
});

builder.Services
    .AddIdentity<Usuario, Rol>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(opciones =>
{
    opciones.Password.RequireNonAlphanumeric = false;
    opciones.Password.RequiredUniqueChars = 0;
    opciones.Password.RequireUppercase = false;

    opciones.User.AllowedUserNameCharacters = "0123456789";
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Usuarios/Acceder";
});

builder.Services.AddSingleton(NtsGeometryServices.Instance.CreateGeometryFactory(4326));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSingleton(provider =>

    new MapperConfiguration(options =>
    {
        var geometryFactory = provider.GetRequiredService<GeometryFactory>();

        options.AddProfile(new AccionPerfil());
        options.AddProfile(new CablePerfil());
        options.AddProfile(new ClientePerfil(geometryFactory));
        options.AddProfile(new GponPerfil());
        options.AddProfile(new MufaPerfil());
        options.AddProfile(new PostePerfil(geometryFactory));
        options.AddProfile(new PuntoPerfil(geometryFactory));
        options.AddProfile(new UsuarioPerfil());
        options.AddProfile(new ServicioPerfil());
        options.AddProfile(new ContratoPerfil());
    })
    .CreateMapper()
);

builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v2", new OpenApiInfo() { Title = "LevantamientoDeRedAPI", Version = "v2" });
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    var currentThreadCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
    currentThreadCulture.NumberFormat = NumberFormatInfo.InvariantInfo;

    Thread.CurrentThread.CurrentCulture = currentThreadCulture;
    Thread.CurrentThread.CurrentUICulture = currentThreadCulture;

    await next();
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        setup.SwaggerEndpoint("/swagger/v2/swagger.json", "LevantamientoDeRedAPI");
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    var userManager = (UserManager<Usuario>)scope.ServiceProvider.GetService(typeof(UserManager<Usuario>));
    var roleManager = (RoleManager<Rol>)scope.ServiceProvider.GetService(typeof(RoleManager<Rol>));

    await InicializadorDatos.SembrarDatos(userManager, roleManager);
}


app.Run();
internal class InicializadorDatos
{
    internal static async Task SembrarDatos(UserManager<Usuario>? userManager, RoleManager<Rol>? roleManager)
    {
        await SembrarRol(roleManager);
        await SembrarUsuario(userManager);
    }

    private static async Task SembrarRol(RoleManager<Rol>? roleManager)
    {
        var rolBuscado1 = await roleManager.FindByNameAsync("Administrador");

        if (rolBuscado1 is null)
        {
            var nuevoRol1 = new Rol()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            };

            await roleManager.CreateAsync(nuevoRol1);
        }

        var rolBuscado3 = await roleManager.FindByNameAsync("Jefe");

        if (rolBuscado3 is null)
        {
            var nuevoRol3 = new Rol()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Jefe",
                NormalizedName = "JEFE"
            };

            await roleManager.CreateAsync(nuevoRol3);
        }

        var rolBuscado4 = await roleManager.FindByNameAsync("Visitante");

        if (rolBuscado4 is null)
        {
            var nuevoRol4 = new Rol()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Visitante",
                NormalizedName = "VISITANTE"
            };

            await roleManager.CreateAsync(nuevoRol4);
        }

        var rolBuscado2 = await roleManager.FindByNameAsync("Tecnico");

        if (rolBuscado2 is null)
        {
            var nuevoRol2 = new Rol()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Tecnico",
                NormalizedName = "Tecnico"
            };

            await roleManager.CreateAsync(nuevoRol2);
        }
    }

    private static async Task SembrarUsuario(UserManager<Usuario>? userManager)
    {
        var usuarioBuscado = await userManager.FindByNameAsync("000000000");
        
        if (usuarioBuscado is null)
        {
            var nuevoUsuario = new Usuario()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "000000000",
                Nombre = "Administrador"
            };

            var resultado = await userManager.CreateAsync(nuevoUsuario, "admin123");

            if (resultado.Succeeded)
            {
                var resultadoRol = await userManager.AddToRoleAsync(nuevoUsuario, "Administrador");

                if (resultadoRol.Succeeded)
                {
                    await userManager.AddClaimAsync(nuevoUsuario, new Claim(ClaimTypes.Actor, "Administrador"));
                }
            }
        }
        
        var usuarioBuscado2 = await userManager.FindByNameAsync("111111111");

        if (usuarioBuscado2 is null)
        {
            var nuevoUsuario = new Usuario()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "111111111",
                Nombre = "Invitado"
            };

            var resultado = await userManager.CreateAsync(nuevoUsuario, "invitado123");

            if (resultado.Succeeded)
            {
                var resultadoRol = await userManager.AddToRoleAsync(nuevoUsuario, "Visitante");

                if (resultadoRol.Succeeded)
                {
                    await userManager.AddClaimAsync(nuevoUsuario, new Claim(ClaimTypes.Actor, "Invitado"));
                }
            }
        }
    }
}
