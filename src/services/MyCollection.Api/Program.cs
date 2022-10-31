using MyCollection.Api.Setup;
using MyCollection.Data.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCorsConfiguration();
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddDependencies();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{
    DataSeeders.ApplySeeders(app.Services).Wait();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
