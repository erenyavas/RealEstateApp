using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RealEstateApp.Data;
using RealEstateApp.Models;
using RealEstateApp.Repositories;
using RealEstateApp.Services;
using RealEstateApp.Helpers;
using RealEstateApp.Filters;
using RealEstateApp.Middleware;
using System.Text;
using Serilog;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Prometheus;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", policy =>
    {
        policy.PermitLimit = 10; 
        policy.Window = TimeSpan.FromMinutes(1);

    });
   
    
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
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
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IDistrictRepository, DistrictRepository>();
builder.Services.AddScoped<IDistrictService, DistrictService>();
builder.Services.AddScoped<INeighborhoodRepository, NeighborhoodRepository>();
builder.Services.AddScoped<INeighborhoodService, NeighborhoodService>();
builder.Services.AddScoped<IRealEstateRepository, RealEstateRepository>();
builder.Services.AddScoped<IRealEstateService, RealEstateService>();
builder.Services.AddScoped<IRealEstateTypeRepository, RealEstateTypeRepository>();
builder.Services.AddScoped<IRealEstateTypeService, RealEstateTypeService>();
builder.Services.AddScoped<IRealEstateStatusRepository, RealEstateStatusRepository>();
builder.Services.AddScoped<IRealEstateStatusService, RealEstateStatusService>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IDynamicFeatureRepository, DynamicFeatureRepository>();
builder.Services.AddScoped<IDynamicFeatureService, DynamicFeatureService>();
builder.Services.AddScoped<IRealEstateTypeFeatureRepository, RealEstateTypeFeatureRepository>();
builder.Services.AddScoped<IRealEstateTypeFeatureService, RealEstateTypeFeatureService>();
builder.Services.AddScoped<IRealEstateFeatureValueRepository, RealEstateFeatureValueRepository>();
builder.Services.AddScoped<IRealEstateFeatureValueService, RealEstateFeatureValueService>();
builder.Services.AddScoped<IFeatureCategoryRepository, FeatureCategoryRepository>();
builder.Services.AddScoped<IFeatureCategoryService, FeatureCategoryService>();
builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IRealEstateTypeFeatureCategoryRepository, RealEstateTypeFeatureCategoryRepository>();
builder.Services.AddScoped<IRealEstateTypeFeatureCategoryService, RealEstateTypeFeatureCategoryService>();
builder.Services.AddScoped<IRealEstateFeatureRepository, RealEstateFeatureRepository>();
builder.Services.AddScoped<IRealEstateFeatureService, RealEstateFeatureService>();
builder.Services.AddScoped<IProfileService, ProfileService>();

// AutoMapper Configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>(); 
});

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RealEstateApp API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] { }
    }});
});

var app = builder.Build();

app.UseMetricServer(); 
app.UseHttpMetrics();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RealEstateApp API v1"));
}

app.UseRateLimiter();


app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseStaticFiles();
app.Run();
