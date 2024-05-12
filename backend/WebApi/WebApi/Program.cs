using Microsoft.AspNetCore.HttpOverrides;
using sib_api_v3_sdk.Client;
using WebApi;

var builder = WebApplication.CreateBuilder(args);
var apiKey = builder.Configuration.GetSection("ApiKey").Value ?? "";
// Add services to the container.
Configuration.Default.ApiKey.TryAdd("api-key", apiKey);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200") // Add your client's origin
            .AllowAnyHeader()
            .AllowAnyMethod());
});
builder.Services.AddSwaggerGen();


var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();
