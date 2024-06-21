



using jwt.Helpers;
using jwt.Models.DBContext;
using jwt.Models.Identity;
using jwt.Seed;
using jwt.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<myContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<myContext>();


builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JWT"));
builder.Services.AddScoped<IAuth,Auth>();
builder.Services.AddScoped<Seeder>();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = true;
    o.SaveToken = true;
    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetValue<string>("JWT:Issuer"),
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetValue<string>("JWT:Audence"),

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWT:key")))
    };
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var Seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
    await Seeder.Intialize();
}
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
