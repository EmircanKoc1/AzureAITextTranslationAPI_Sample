namespace AzureAITextTranslationAPI_Sample.Options
{
    public class AzureAITranslateOptions
    {
        public string Azure { get; set; } = nameof(AzureAITranslateOptions);

        public string APIKey { get; set; }
        public string Region { get; set; }
        public string Endpoint { get; set; }



    }
}
