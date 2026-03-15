using Woodia.Contract.MyMemory;

namespace Woodia.Service.Interface;

public interface ITranlationService
{
    Task<string> TranslateToEnglish(TranslationRequest translationRequest);
}