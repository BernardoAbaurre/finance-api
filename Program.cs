using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using finance_api.Application.Users.Services;
using finance_api.Domain.Users.Entities;
using finance_api.Domain.Users.Services;
using finance_api.Domain.Utils.ActionsFiltersAttributes;
using finance_api.Infra.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelAttribute>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insert your JWT token in the field: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var keyVaultURL = new Uri(builder.Configuration.GetSection("KeyVault:KeyVaultURL").Value!);
var azureCredential = new DefaultAzureCredential();
builder.Configuration.AddAzureKeyVault(keyVaultURL, azureCredential);

builder.Services.AddDbContext<FinanceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection("BernardoDevConnectionString").Value);
});

builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<FinanceDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        //ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTSecretKey").Value!))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.Scan(scan => scan
    .FromAssemblyOf<UsersAppService>()
        .AddClasses(classes => classes.Where(type => !type.IsAssignableTo(typeof(Exception))))
            .AsImplementedInterfaces()
                .WithScopedLifetime());

builder.Services.Scan(scan => scan
    .FromAssemblyOf<UsersService>()
        .AddClasses(classes => classes.Where(type => !type.IsAssignableTo(typeof(Exception))))
            .AsImplementedInterfaces()
                .WithScopedLifetime());

//builder.Services.Scan(scan => scan
//    .FromAssemblyOf<UsersRepository>()
//        .AddClasses(classes => classes.Where(type => !type.IsAssignableTo(typeof(Exception))))
//            .AsImplementedInterfaces()
//                .WithScopedLifetime());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
