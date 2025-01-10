using AzureAITextTranslationAPI_Sample.Models;

namespace AzureAITextTranslationAPI_Sample.Services
{
    public interface ITranslateService
    {
        Task<TranslateResponseModel> TranslateAsync(string sourceLanguage, string targetLanguage, string content);
        Task<IEnumerable<TranslateResponseModel>> TranslateAsync(string sourceLanguage, IEnumerable<string> targetLanguages, string content);

        Task<IEnumerable<TranslateResponseModel>> TranslateAsync(string sourceLanguage, IEnumerable<string> targetLanguages, IEnumerable<string> contents);

        //Task<IEnumerable<TranslateResponseModel>> TranslateAsync(string sourceLanguage, string targetLanguage, IEnumerable<string> contents);

    }
}
