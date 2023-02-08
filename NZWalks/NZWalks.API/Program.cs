using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
  
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQL Server connection params dependencies injection in the app.
builder.Services.AddDbContext<NZWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks"));
    // For MySql server
    // options.UseMySql(connextionString, serverVersion.AutoDetect(ConnectionString));
});

// Whenever I ask for IRegionRepository interface, give me the RegionRepository Region class Methods.
builder.Services.AddScoped<IRegionRepository, RegionRepository>();

// AutoMapper service declaration injection into the program.
builder.Services.AddAutoMapper(typeof(Program).Assembly);





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
