using Application;
using Persistence;
using Infrastructure;
using API.MiddleWares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NFT Gebeya Backend API",
        Version = "v1",
        Description = "The NFT Gebeya Backend API serves as the backbone for the NFT Gebeya platform, providing a robust set of endpoints to facilitate the management and interaction with non-fungible tokens (NFTs). This API empowers developers, creators, and users to seamlessly integrate with the NFT Gebeya ecosystem, enabling a wide range of functionalities, including token creation, transfer, and retrieval of valuable metadata associated with unique digital assets.\n\nKey Features:\n- NFT Creation and Management\n- Metadata Retrieval for NFTs\n- Secure Token Transfers\n- User Authentication and Authorization\n\nExplore the API's capabilities and unleash the potential of decentralized digital assets in the world of NFTs!\n\nFor support and inquiries, please contact the NftGebeya Backend Team.",
        Contact = new OpenApiContact
        {
            Name = "NftGebeya Backend Team"
        },
    });

    
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

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
        c.SwaggerEndpoint("v1/swagger.json", "NFT Gebeya Backend API v1");
    });
}

app.UseRouting();
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
