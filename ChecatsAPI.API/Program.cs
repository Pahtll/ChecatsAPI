using Checats.Application.Interfaces;
using Checats.Application.Services;
using Checats.Infrastructure;
using Checats.Persistence;
using ChecatsAPI.Extensions;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddAuthentication();
services.AddAuthorization();
services.AddControllers();

services.AddDbContext<ChecatsDbContext>(
    options =>
    {
        options.UseNpgsql(config.GetConnectionString(nameof(ChecatsDbContext)));
    });


services.AddScoped<IUserService, UserService>();
services.AddScoped<IPostService, PostService>();
services.AddScoped<ICommentaryService, CommentaryService>();
services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();

services.Configure<JwtOptions>(config.GetSection(nameof(JwtOptions)));

var app = builder.Build();


app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.AddMappedEndpoints();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();