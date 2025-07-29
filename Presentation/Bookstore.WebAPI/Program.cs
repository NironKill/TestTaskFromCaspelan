using Bookstore.Application;
using Bookstore.Persistence;
using Bookstore.Persistence.Common;
using Bookstore.WebAPI.Handlers;
using Serilog;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(config =>
{
    config.ReadFrom.Configuration(builder.Configuration);
    config.Enrich.FromLogContext();
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
});

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    string basePath = AppContext.BaseDirectory;

    string xmlPath = Path.Combine(basePath, "Documentation.xml");
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddExceptionHandler<HandlerException>();
builder.Services.AddProblemDetails();

builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

using (IServiceScope scope = app.Services.CreateScope())
{
    ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await Preparation.Initialize(context);
}

app.Run();