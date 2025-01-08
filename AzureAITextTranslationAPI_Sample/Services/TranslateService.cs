
using Azure.AI.Translation.Text;

namespace AzureAITextTranslationAPI_Sample.Services
{
    public class TranslateService : ITranslateService
    {
        private readonly TextTranslationClient _textTranslationClient;

        public TranslateService(TextTranslationClient textTranslationClient)
        {
            _ = textTranslationClient ?? throw new ArgumentNullException("The TextTranslationClient instance can not be null");
            _textTranslationClient = textTranslationClient;
        }

        public async Task<string?> TranslateAsync(string sourceLanguage, string targetLanguage, string text)
        {
            var translatedResponse = await _textTranslationClient.TranslateAsync(targetLanguage, text, sourceLanguage);

            //return translateResponse.Value.FirstOrDefault().Translations;

            return translatedResponse.Value.FirstOrDefault()?.Translations.FirstOrDefault()?.Text;
        }
    }
}
