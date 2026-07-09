using System;
using UnityEngine;

public enum Language
{
    English,
    Turkish
}

public class LanguageManager : MonoBehaviour
{
    private const string LanguagePrefsKey = "SelectedLanguage";

    public static LanguageManager Instance { get; private set; }

    public static event Action<Language> LanguageChanged;

    public Language CurrentLanguage { get; private set; } = Language.English;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        CurrentLanguage = (Language)PlayerPrefs.GetInt(LanguagePrefsKey, (int)Language.English);
    }

    private void Start()
    {
        LanguageChanged?.Invoke(CurrentLanguage);
    }

    public void SetLanguage(int languageIndex)
    {
        SetLanguage((Language)languageIndex);
    }

    public void SetLanguage(Language language)
    {
        if (CurrentLanguage == language)
        {
            return;
        }

        CurrentLanguage = language;
        PlayerPrefs.SetInt(LanguagePrefsKey, (int)language);
        PlayerPrefs.Save();
        LanguageChanged?.Invoke(CurrentLanguage);
    }

    public void SetTurkish() => SetLanguage(Language.Turkish);

    public void SetEnglish() => SetLanguage(Language.English);
}
