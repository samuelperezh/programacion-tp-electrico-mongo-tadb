using ProgramacionTP_CS_API_Mongo.DbContexts;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Repositories;
using ProgramacionTP_CS_API_Mongo.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Aqui agregamos los servicios requeridos

//El DBContext a utilizar
builder.Services.AddSingleton<MongoDbContext>();

//Los repositorios
builder.Services.AddScoped<IAutobusRepository, AutobusRepository>();
builder.Services.AddScoped<ICargadorRepository, CargadorRepository>();
builder.Services.AddScoped<IHorarioRepository, HorarioRepository>();
builder.Services.AddScoped<IOperacionAutobusRepository, OperacionAutobusRepository>();
builder.Services.AddScoped<IUtilizacionCargadorRepository, UtilizacionCargadorRepository>();
builder.Services.AddScoped<IInformeRepository, InformeRepository>();
builder.Services.AddScoped<IInformeHoraRepository, InformeHoraRepository>();
builder.Services.AddScoped<IInformeOperacionAutobusRepository, InformeOperacionAutobusRepository>();
builder.Services.AddScoped<IInformeUtilizacionCargadorRepository, InformeUtilizacionCargadorRepository>();

//Aqui agregamos los servicios asociados para cada EndPoint
builder.Services.AddScoped<AutobusService>();
builder.Services.AddScoped<CargadorService>();
builder.Services.AddScoped<HorarioService>();
builder.Services.AddScoped<OperacionAutobusService>();
builder.Services.AddScoped<UtilizacionCargadorService>();
builder.Services.AddScoped<InformeService>();
builder.Services.AddScoped<InformeHoraService>();
builder.Services.AddScoped<InformeOperacionAutobusService>();
builder.Services.AddScoped<InformeUtilizacionCargadorService>();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Programación de Transporte Público Eléctrico - MongoDB Version",
        Description = "API para la gestión de programación de transporte público eléctrico"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();