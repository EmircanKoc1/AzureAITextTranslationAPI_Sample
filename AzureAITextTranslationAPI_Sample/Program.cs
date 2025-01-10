using AzureAITextTranslationAPI_Sample.Endpoints;
using AzureAITextTranslationAPI_Sample.Extensions;
using AzureAITextTranslationAPI_Sample.Models;
using AzureAITextTranslationAPI_Sample.Options;
using AzureAITextTranslationAPI_Sample.Services;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.Configure<AzureAITranslateOptions>(builder.Configuration.GetSection(AzureAITranslateOptions.Azure));


builder.Services.AddOptions<AzureAITranslateOptions>();
builder.Services.AddSingleton<ITranslateService, TranslateService>();

builder.Services.AddAzureAITranslateOptions();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();



app.MapGet("/", () =>
{
    return Results.Redirect("/scalar/v1");
});

app.AddEndpoints();

app.Run();
