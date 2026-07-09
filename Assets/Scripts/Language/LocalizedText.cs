using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LocalizedText : MonoBehaviour
{
    [SerializeField] [TextArea] private string turkishText;
    [SerializeField] [TextArea] private string englishText;

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        LanguageManager.LanguageChanged += Apply;
        Apply(LanguageManager.Instance != null ? LanguageManager.Instance.CurrentLanguage : Language.Turkish);
    }

    private void OnDisable()
    {
        LanguageManager.LanguageChanged -= Apply;
    }

    private void Apply(Language language)
    {
        text.text = language == Language.Turkish ? turkishText : englishText;
    }
}
