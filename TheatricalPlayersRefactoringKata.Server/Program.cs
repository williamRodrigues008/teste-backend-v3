using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection.Emit;
using System.Reflection;
using TheatricalPlayersRefactoringKata.Server.ContextDb;

var builder = WebApplication.CreateBuilder(args);
var port = "7000";

builder.Services.AddDbContext<ContextDtaBase>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao"));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swg =>
    {
    swg.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Theatrical",
        Version = "v1.0",
        Description = "API para gerenciamento de dados teatrais",
        Contact = new OpenApiContact
        {
            Email = "wrsilva008@gmail.com",
            Name = "William Silva",
        },
    });

    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    swg.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins($"https://localhost:{port}")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
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

app.Run();
