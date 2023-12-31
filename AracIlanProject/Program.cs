using Business.Abstract;
using Business.Concrete;
using Business.ValidationRules.FluentValidation;
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
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
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
            TokenValidationParameters validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
    };
});

var app = builder.Build();


app.UseCors(builder=> builder.WithOrigins("https://localhost:4200", "http://localhost:4200").AllowAnyHeader().AllowCredentials());

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.UseCookiePolicy(new CookiePolicyOptions
{
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always,
    MinimumSameSitePolicy = SameSiteMode.Strict
});

app.MapControllers();

app.Run();
