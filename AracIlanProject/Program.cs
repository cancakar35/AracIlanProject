using Business.Abstract;
using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Helpers;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddSingleton<ITokenHelper, JwtHelper>();
builder.Services.AddScoped<IAracDal, EfAracDal>();
builder.Services.AddScoped<IAracIlanDal, EfAracIlanDal>();
builder.Services.AddScoped<IRenkDal, EfRenkDal>();
builder.Services.AddScoped<IMarkaDal, EfMarkaDal>();
builder.Services.AddScoped<IKategoriDal, EfKategoriDal>();
builder.Services.AddScoped<IKasaTipiDal, EfKasaTipiDal>();
builder.Services.AddScoped<ICekisTipiDal, EfCekisTipiDal>();
builder.Services.AddScoped<IYakitTipiDal, EfYakitTipiDal>();
builder.Services.AddScoped<IVitesTipiDal, EfVitesTipiDal>();
builder.Services.AddScoped<IAracResimDal, EfAracResimDal>();
builder.Services.AddScoped<IAracService, AracManager>();
builder.Services.AddScoped<IAracIlanService, AracIlanManager>();
builder.Services.AddScoped<IOzellikService<Renk>, RenkService>();
builder.Services.AddScoped<IOzellikService<VitesTipi>, VitesTipiService>();
builder.Services.AddScoped<IOzellikService<KasaTipi>, KasaTipiService>();
builder.Services.AddScoped<IOzellikService<YakitTipi>, YakitTipiService>();
builder.Services.AddScoped<IOzellikService<CekisTipi>, CekisTipiService>();
builder.Services.AddScoped<IKategoriService, KategoriManager>();
builder.Services.AddScoped<IMarkaService, MarkaManager>();
builder.Services.AddScoped<IAracResimService, AracResimManager>();
builder.Services.AddSingleton<IFileHelper, FileHelperManager>();
builder.Services.AddSingleton<IValidator<User>, UserValidator>();
builder.Services.AddSingleton<IValidator<UserRegisterDto>, UserRegisterValidation>();
builder.Services.AddSingleton<IValidator<AddIlanDto>, AddIlanValidation>();

TokenOptions tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>()!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
        ClockSkew = TimeSpan.Zero,
        LifetimeValidator = (DateTime? notBefore, DateTime? expires,
            SecurityToken securityToken,
            TokenValidationParameters validationParameters) => expires != null ? expires > DateTime.Now : false,
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder=> builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
