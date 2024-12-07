using Microsoft.Extensions.DependencyInjection.Extensions;
using UniversityApi;
using UniversityApi.Common.ContextAccesor;
using UniversityApi.Common.Middlewares;

var builder = WebApplication.CreateBuilder(args);
string SpecificOrigins = "specificOrigins";

var corsOrigins = builder.Configuration.GetSection("CorsOrigins").Get<string[]>();

builder.Services.AddCors(options => options
    .AddPolicy(SpecificOrigins, builder => builder
        .WithOrigins(corsOrigins!)
        .AllowAnyHeader()
        .AllowAnyMethod())
 );
builder.Services.AddControllers();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IContextAccessor, ContextAccessor>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(SpecificOrigins);

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<CustomExceptionMiddleware>();

app.MapControllers();

app.Run();
