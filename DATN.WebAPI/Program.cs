using CloudinaryDotNet;
using DATN.Application.Services;
using DATN.Application.Services.Implements;
using DATN.Application.Services.Interfaces;
using DATN.Infrastructure.Context;
using DATN.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình CloudinarySettings
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings"));

// Thêm Cloudinary vào DI container nếu cần
builder.Services.AddSingleton(s =>
{
    var config = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
    Account account = new Account(config.CloudName, config.ApiKey, config.ApiSecret);
    return new CloudinaryDotNet.Cloudinary(account);
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DATNContext>(ops => ops.UseSqlServer(builder.Configuration.GetConnectionString("DATNConnect")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRatingBlogService, RatingBlogService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IKoreaBlogService, KoreaBlogService>();
builder.Services.AddTransient<ICloudService, CloudService>();
builder.Services.AddTransient<ISystemLoggingService, SystemLoggingService>();
builder.Services.AddTransient<IReadingQuestionService, ReadingQuestionService>();
builder.Services.AddTransient<IRankQuestionService, RankQuestionService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IListeningQuestionService, ListeningQuestionService>();
builder.Services.AddTransient<ITestSetService, TestSetService>();

var jwtSetting = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSetting["Key"] ?? throw new InvalidOperationException("Jwt key is missing"));

if (key.Length < 32)
{
    throw new InvalidOperationException("Key must be 32 character !");
}


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = jwtSetting["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSetting["Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SystemAdminOnly", policy => policy.RequireRole("SystemAdmin"));
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

//automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
