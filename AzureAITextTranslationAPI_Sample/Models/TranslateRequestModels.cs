namespace AzureAITextTranslationAPI_Sample.Models;

public record TranslateRequestModel(string sourceLang, string targetLang, string content);
public record TranslateMultiTargetLangRequestModel(string sourceLang, IEnumerable<string> targetLangs, string content);

public record TranslateMultiContentAndTargetLangRequestModel(string sourceLang, IEnumerable<string> targetLangs, IEnumerable<string> contents);
