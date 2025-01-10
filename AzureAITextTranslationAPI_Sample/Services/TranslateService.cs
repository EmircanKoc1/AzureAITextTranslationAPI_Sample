
using Azure;
using Azure.AI.Translation.Text;
using AzureAITextTranslationAPI_Sample.Models;
using System.Net;

namespace AzureAITextTranslationAPI_Sample.Services
{
    public class TranslateService : ITranslateService
    {
        private readonly TextTranslationClient _textTranslationClient;
        private readonly ILogger<TranslateService> _logger;

        public TranslateService(TextTranslationClient textTranslationClient, ILogger<TranslateService> logger)
        {
            _ = textTranslationClient ?? throw new ArgumentNullException("The TextTranslationClient instance can not be null");
            _textTranslationClient = textTranslationClient;
            _logger = logger;
        }

        public async Task<TranslateResponseModel> TranslateAsync(string sourceLanguage, string targetLanguage, string content)
        {

            var translateOptions = new TextTranslationTranslateOptions(
             targetLanguages: [targetLanguage],
             sourceLanguage: sourceLanguage,
             content: [content]);


            var responseObject = new TranslateResponseModel(
                sourceLang: sourceLanguage,
                targetLang: targetLanguage,
                content: content,
                translatedContent: null,
                errorMessage: null,
                isSuccess: false);

            try
            {

                var result = await _textTranslationClient.TranslateAsync(translateOptions);
                var translationText = result.Value?.FirstOrDefault()?.Translations.FirstOrDefault();

                return responseObject with
                {
                    translatedContent = translationText?.Text,
                    isSuccess = true
                };
            }
            catch (RequestFailedException ex) when (ex.Status is (int)HttpStatusCode.BadRequest)
            {
                _logger.LogInformation(ex.Message);
                return responseObject with
                {
                    translatedContent = null,
                    errorMessage = ex.Message
                };
            }
            catch (RequestFailedException ex)
            {
                _logger.LogInformation(ex.Message);
                throw;
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogInformation(ex.Message);
                throw;
            }


        }

        public async Task<IEnumerable<TranslateResponseModel>?> TranslateAsync(string sourceLanguage, IEnumerable<string> targetLanguages, string content)
        {
            try
            {
                var textTranslationOptions = new TextTranslationTranslateOptions(
              targetLanguages: targetLanguages,
              sourceLanguage: sourceLanguage,
              content: [content]);
                var translateResponse = await _textTranslationClient.TranslateAsync(textTranslationOptions);
                return translateResponse?.Value?.FirstOrDefault()?.Translations?.Select(tt =>
                  {
                      return new TranslateResponseModel(
                          sourceLang: sourceLanguage,
                          targetLang: tt.TargetLanguage,
                          content: content,
                          translatedContent: tt.Text,
                          errorMessage: null,
                          isSuccess: true);
                  });
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw;
            }




        }

        public async Task<IEnumerable<TranslateResponseModel>?> TranslateAsync(string sourceLanguage, IEnumerable<string> targetLanguages, IEnumerable<string> contents)
        {
            try
            {

                var translateOptions = new TextTranslationTranslateOptions(
                   targetLanguages: targetLanguages,
                   content: contents,
                   sourceLanguage: sourceLanguage);

                var translateResponse = await _textTranslationClient.TranslateAsync(translateOptions);


                var responseList = new List<TranslateResponseModel>();



                foreach (var tti in translateResponse.Value)
                {


                    foreach (var tt in tti.Translations)
                    {
                        responseList.Add(new TranslateResponseModel(
                           sourceLang: sourceLanguage,
                           targetLang: tt.TargetLanguage,
                           content: null,
                           translatedContent: tt.Text,
                           errorMessage: null,
                           isSuccess: true));


                    }

                }



                return responseList;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw;
            }



        }


    }
}
