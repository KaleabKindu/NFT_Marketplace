using Application;
using Persistence;
using Infrustructure;
using API.MiddleWares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureInfrustructureServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NFT Marketplace API",
        Version = "v1",
        Description = "Backend API for NFT marketplace platform",
        Contact = new OpenApiContact
        {
            Name = "NftGebeya Backend Team"
        },
    });

    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.AddCors(o =>
{
	o.AddPolicy("CorsPolicy",
		builder => builder.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseMiddleware<ExceptionHandler>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "swagger";
        c.SwaggerEndpoint("v1/swagger.json", "NFT Marketplace API v1");
    });
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
