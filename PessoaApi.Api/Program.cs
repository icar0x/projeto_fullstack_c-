using Microsoft.EntityFrameworkCore;
using PessoaApi.Api.Data;
using PessoaApi.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// NOVO: registra o DbContext no container de DI
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPessoaService, PessoaService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();     // expõe o JSON
    app.UseSwaggerUI();   // expõe a UI visual em /swagger
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("PermitirFrontend");

app.MapControllers();

app.Run();