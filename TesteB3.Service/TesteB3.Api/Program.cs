using FluentValidation;
using TesteB3.Domain.Interfaces;
using TesteB3.Domain.Services;
using TesteB3.Domain.Validators;
using TesteB3.Domain.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICdbService, CdbService>();
builder.Services.AddScoped<IValidator<CdbViewModel>, CdbViewModelValidator>();

builder.Services.AddCors(op =>
{
    op.AddDefaultPolicy(pol =>
    {
        pol.WithOrigins("http://localhost:4200");
        pol.AllowAnyHeader();
        pol.WithMethods("POST");
    }
    );
});

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

app.UseCors();

app.Run();
