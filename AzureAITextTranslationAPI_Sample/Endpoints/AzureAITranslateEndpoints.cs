using AzureAITextTranslationAPI_Sample.Models;
using AzureAITextTranslationAPI_Sample.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AzureAITextTranslationAPI_Sample.Endpoints
{
    public static class AzureAITranslateEndpoints
    {

        public static WebApplication AddEndpoints(this WebApplication app)
        {

            app.MapPost("translate-sinle-lang", async (
                [FromServices] ITranslateService _translateService,
                [FromBody] TranslateRequestModel model) =>
            {
                try
                {
                    var result = await _translateService.TranslateAsync(
                   sourceLanguage: model.sourceLang,
                   targetLanguage: model.targetLang,
                   content: model.content);

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    
                    return Results.BadRequest(ex.Message);
                }





            })
                .Produces<TranslateResponseModel>((int)HttpStatusCode.OK)
                .Produces<string>((int)HttpStatusCode.BadRequest);

            app.MapPost("/translate-multi-lang-single-content", async (
                 [FromServices] ITranslateService _translateService,
                 [FromBody] TranslateMultiTargetLangRequestModel model) =>
            {

                try
                {
                    var result = await _translateService.TranslateAsync(
                   sourceLanguage: model.sourceLang,
                   targetLanguages: model.targetLangs,
                   content: model.content);

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {

                    return Results.BadRequest(ex.Message);
                }


            })
                .Produces<TranslateResponseModel>((int)HttpStatusCode.OK)
                .Produces<string>((int)HttpStatusCode.BadRequest);


            app.MapPost("/translate-multi-lang-multi-content", async (
                 [FromServices] ITranslateService _translateService,
                 [FromBody] TranslateMultiContentAndTargetLangRequestModel model) =>
            {

                try
                {
                    var result = await _translateService.TranslateAsync(
                   sourceLanguage: model.sourceLang,
                   targetLanguages: model.targetLangs,
                   contents: model.contents);

                    return Results.Ok(result);
                }
                catch (Exception ex)
                {

                    return Results.BadRequest(ex.Message);
                }


            })
                .Produces<TranslateResponseModel>((int)HttpStatusCode.OK)
                .Produces<string>((int)HttpStatusCode.BadRequest);



            return app;
        }


    }

}
