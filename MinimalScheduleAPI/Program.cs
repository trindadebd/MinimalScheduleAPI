using Microsoft.EntityFrameworkCore;
using MinimalScheduleAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conectionString = builder.Configuration.GetConnectionString("EventoCs");
builder.Services.AddDbContext<CardDbContext>(e => e.UseSqlServer(conectionString));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(d =>
{
    d.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "MinimalScheduleAPI",
        Version = "v1",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Brenda Trindade",
            Email = "brenda.trindade001@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/brenda-trindade-50361a176/")
        }
    });

    var xmlFile = "MinimalScheduleAPI.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    d.IncludeXmlComments(xmlPath);
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
