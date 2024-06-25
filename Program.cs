using Api_Versioning;
using Asp.Versioning;

// NuGet Package Console
//
//   Install-Package Asp.Versioning.Mvc
//   Install-Package Asp.Versioning.Mvc.ApiExplorer
//   Install-Package Asp.Versioning.Http

// Net CLI
//
//   dotnet add package Asp.Versioning.Mvc --version 8.1.0
//   dotnet add package Asp.Versioning.Mvc.ApiExplorer --version 8.1.0
//   dotnet add package Asp.Versioning.Http --version 8.1.0

// youtube:
//          https://www.youtube.com/watch?v=i6kkKBsHEJs

var builder = WebApplication.CreateBuilder(args);

// 1.-
// QueryString : http://localhost:5051/api/stringlist?api-version=1.0
// Headers     : Accept -> application/json;ver=1.0
//               http://localhost:5051/api/stringlist
// URL         : http://localhost:5051/api/v3/stringlist
//
builder.Services.AddApiVersioning(o => { 
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Asp.Versioning.ApiVersion(2, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
            new QueryStringApiVersionReader("api-version"),
            new HeaderApiVersionReader("X-Version"),
            new MediaTypeApiVersionReader("ver")
        );
}).AddApiExplorer( options => { 
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// 2.-
var apiVersionSet = app.NewApiVersionSet()
        .HasDeprecatedApiVersion(new ApiVersion(1, 0))
        .HasApiVersion(new ApiVersion(2, 0))
        .HasApiVersion(new ApiVersion(3, 0))
        .ReportApiVersions()
        .Build();

// 3.-
app.MapGet("api/minimal/StringList", () =>
{
    var strings = Data.Summaries.Where(x => x.StartsWith("B"));

    return TypedResults.Ok(strings);
})
.WithApiVersionSet(apiVersionSet)
.MapToApiVersion(new ApiVersion(1, 0));

app.MapGet("api/minimal/StringList", () =>
{
    var strings = Data.Summaries.Where(x => x.StartsWith("S"));

    return TypedResults.Ok(strings);
})
.WithApiVersionSet(apiVersionSet)
.MapToApiVersion(new ApiVersion(2, 0));

app.MapGet("api/minimal/v{version:apiVersion}/StringList", () =>
{
    var strings = Data.Summaries.Where(x => x.StartsWith("C"));

    return TypedResults.Ok(strings);
})
.WithApiVersionSet(apiVersionSet)
.MapToApiVersion(new ApiVersion(3, 0));



app.Run();
