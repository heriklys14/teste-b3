using FluentValidation;
using Microsoft.Net.Http.Headers;
using System.Diagnostics.CodeAnalysis;
using TesteB3.Domain.Interfaces;
using TesteB3.Domain.Services;
using TesteB3.Domain.Validators;
using TesteB3.Domain.ViewModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICdbService, CdbService>();
builder.Services.AddScoped<IValidator<CdbViewModel>, CdbViewModelValidator>();

builder.Services.AddCors(op =>
{
    op.AddDefaultPolicy(pol =>
    {
        pol.WithOrigins("http://localhost:4200");
        pol.WithHeaders(HeaderNames.Accept, HeaderNames.ContentType, HeaderNames.Referer, HeaderNames.UserAgent, "X-Application-Id");
        pol.WithMethods("POST");
    }
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

await app.RunAsync();

[ExcludeFromCodeCoverage]
public static partial class Program { }