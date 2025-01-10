using Azure.AI.Translation.Text;
using Azure;
using AzureAITextTranslationAPI_Sample.Options;
using Microsoft.Extensions.Options;

namespace AzureAITextTranslationAPI_Sample.Extensions
{
    public static class OptionsPatternExtension
    {
        public static IServiceCollection AddAzureAITranslateOptions(this IServiceCollection services)
        {

            services
                .AddOptions<AzureAITranslateOptions>()
                .BindConfiguration(AzureAITranslateOptions.Azure)
                .Validate(option =>
                {
                    if (option.APIKey is null) return false;

                    if (option.Endpoint is null) return false;

                    if (option.Region is null) return false;

                    return true;
                })
                .ValidateOnStart();

            services.AddSingleton<TextTranslationClient>(provider =>
            {
                var service = provider.GetRequiredService<IOptions<AzureAITranslateOptions>>();

                var options = service.Value;

                string apiKey = options.APIKey;
                string region = options.Region;
                string endpoint = options.Endpoint;

                return new TextTranslationClient(new AzureKeyCredential(apiKey), new Uri(endpoint), region);
            });

            return services;
        }


    }
}
