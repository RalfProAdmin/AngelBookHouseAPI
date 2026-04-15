using EkartAPI.Data;
using EkartAPI.Filters;
using EkartAPI.Models;
using EkartAPI.Models.ResponseModels;
using EkartAPI.Repositories;
using EkartAPI.Repository.Implementation;
using EkartAPI.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200", "http://localhost:61469", "http://192.168.1.6:851", "http://192.168.1.6:853", "https://www.angelbookhouse.in", "https://www.angelbookhouse.in/adminpanel") // Use specific origin for development
                       .AllowAnyMethod()
                       .AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<EkartDBcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbCon"))
);

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IDeliveryAddressRepository, DeliveryAddressRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IContactUsRepository, ContactUsRepository>();
builder.Services.AddScoped<IWishListRepository, WishListRepository>();
builder.Services.AddScoped<IOrdercheckout, OrdercheckoutRepository>();
builder.Services.AddScoped<IEmailInterface, EmailRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<ISmsRepository, SmsRepository>();
builder.Services.Configure<CashfreeSettings>(builder.Configuration.GetSection("CashfreeSettings"));
builder.Services.Configure<WhatsAppSettings>(
    builder.Configuration.GetSection("WhatsAppSettings"));

builder.Services.AddHttpClient<IWhatsAppRepository, WhatsAppRepository>();


// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? throw new InvalidOperationException("Jwt SecretKey is missing"));

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };
    });// Swagger setup
builder.Services.Configure<SendGridSettings>(
    builder.Configuration.GetSection("SendGrid")
    );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EkartAPI", Version = "v1" });

    // Register the SwaggerOptionalParametersFilter
    c.OperationFilter<SwaggerOptionalParametersFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("v1/swagger.json", "EkartAPI V1");
//    c.RoutePrefix = "ecomapi/swagger";
//});
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (error != null)
        {
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
            {
                error = error.Error.Message,
                stack = error.Error.StackTrace
            }));
        }
    });
});


app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();