namespace AzureAITextTranslationAPI_Sample.Models;

public record TranslateResponseModel(
    string sourceLang,
    string targetLang,
    string content,
    string? translatedContent,
    string? errorMessage,
    bool isSuccess);



