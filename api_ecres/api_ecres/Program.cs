using api_ecres.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddResponseCompression(options =>
{
  options.EnableForHttps = true; // Optional: Enable compression for HTTPS requests
  options.MimeTypes = new[] { "application/json" }; // Optional: Only disable compression for specific content types
});

builder.Services.AddDbContext<EcresMreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ecres_MRE")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCompression();

//app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseCors(policy => policy
    .WithOrigins(
        "http://localhost:4200",
        "https://www5.lgm.gov.my"
    )
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
