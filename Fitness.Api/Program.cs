using AutoMapper;
// using Fitness.Application.Services.BookService;
// using Fitness.Application.Services.OrderService;
// using Fitness.Application.Services.UserService;
// using Fitness.Application.Services.RecommendationService;
// using Fitness.Application.UserService.Services;
// using Fitness.Application.Validators.BookDtoValidators;
// using Fitness.Application.Validators.OrderDtoValidators;
// using Fitness.Application.Validators.UserDtoValidators;
// using Fitness.DataAccess.Persistence;
// using Fitness.DataAccess.Repositories;
// using Fitness.DataAccess.Repositories.Impl;
// using FluentValidation.AspNetCore;
using Fitness.Application.Helpers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MySqlConnector;
using Fitness.Infra.Repositories;
using Fitness.Application.Services.UserService;
using Serilog;
using FluentValidation.AspNetCore;
using FluentValidation;
using Fitness.Application.Validators.UserValidators;
using Fitness.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Standart auth with bearer scheme",
        In = ParameterLocation.Header,
        Name = "Auhtorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
});

builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString("DefaultConnection")!);

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<IBookService, BookService>();
// builder.Services.AddScoped<IRecommendationService, RecommendationService>();
// builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<MovementRepository>();
builder.Services.AddScoped<WorkoutRepository>();

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthentication();


app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
        });

app.Run();
