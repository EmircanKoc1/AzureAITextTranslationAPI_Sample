namespace AzureAITextTranslationAPI_Sample.Services
{
    public interface ITranslateService
    {
        Task<string> TranslateAsync(string sourceLanguage, string targetLanguage, string text);

        
    }
}
