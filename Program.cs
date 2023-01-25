using DotnetPlayground;
using DotnetPlayground.DbConfig;
using DotnetPlayground.Features.Projects;
using DotnetPlayground.Features.Projects.CreateOrEdit;
using FluentValidation;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
       options.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<DataContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("WebApiDatabase")), ServiceLifetime.Transient);

builder.Services.AddTransient<IValidator<CreateOrEditRequest>, CreateOrEditValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediator();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
         policy =>
         {
             policy.AllowAnyMethod();
             policy.AllowAnyOrigin();
             policy.AllowAnyHeader();
         });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");
app.UseCors();

//projects
app.MapGet("/projects/list", ([FromQuery(Name = "page")] int page, [FromQuery(Name = "totalItemsPerPage")] int totalItemsPerPage, IMediator mediator) => mediator.Send(new DotnetPlayground.Features.Projects.List.ListRequest(page, totalItemsPerPage)));
app.MapGet("/projects/{projectId}", (Guid projectId, IMediator mediator) => mediator.Send(new DotnetPlayground.Features.Projects.GetById.GetByIdRequest(projectId)) );
app.MapPost("/projects/", (CreateOrEditRequest request,  IMediator mediator) => mediator.Send(request));
app.MapDelete("/projects", (Guid id, IMediator mediator) => mediator.Send(new DeleteRequest(id)));
//people
app.MapGet("/people/list", (IMediator mediator) => mediator.Send(new DotnetPlayground.Features.People.List.ListRequest()) );
app.MapGet("/people/{peopleId}", (Guid peopleId, IMediator mediator) => mediator.Send(new DotnetPlayground.Features.People.GetById.GetByIdRequest(peopleId)) );

app.Run();
