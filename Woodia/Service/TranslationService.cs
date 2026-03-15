using Woodia.Contract.MyMemory;
using Woodia.Service.Interface;

namespace Woodia.Service;

public class TranslationService:ITranlationService
{
    private readonly HttpClient _httpClient;

    public TranslationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> TranslateToEnglish( TranslationRequest translationRequest)
    {
        var url = $"https://api.mymemory.translated.net/get?q={Uri.EscapeDataString(translationRequest.Text)}&langpair=ar|en";
        
        var response = await _httpClient.GetAsync(url);
        var result   = await response.Content.ReadFromJsonAsync<MyMemoryResponse>();
        
        return result?.ResponseData?.TranslatedText ?? translationRequest.Text;
    }
}

