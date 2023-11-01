using ProgramacionTP_CS_API_Mongo.DbContexts;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Repositories;
using ProgramacionTP_CS_API_Mongo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Aqui agregamos los servicios requeridos

//El DBContext a utilizar
builder.Services.AddSingleton<MongoDbContext>();

//Los repositorios
builder.Services.AddScoped<IAutobusRepository, AutobusRepository>();
builder.Services.AddScoped<IInformeOperacionAutobusRepository, InformeOperacionAutobusRepository>();
builder.Services.AddScoped<IHorarioRepository, HorarioRepository>();
builder.Services.AddScoped<ICargadorRepository, ICargadorRepository>();
builder.Services.AddScoped<IOperacionAutobusRepository, IOperacionAutobusRepository>();

//Aqui agregamos los servicios asociados para cada EndPoint
builder.Services.AddScoped<AutobusService>();
builder.Services.AddScoped<InformeOperacionAutobusService>();
builder.Services.AddScoped<HorarioService>();
builder.Services.AddScoped<OperacionAutobusService>();

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();