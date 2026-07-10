using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LocalizedAudioClip : MonoBehaviour
{
    [SerializeField] private AudioClip turkishClip;
    [SerializeField] private AudioClip englishClip;
    [SerializeField] private bool restartIfPlaying = true;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        bool wasPlaying = audioSource.isPlaying;
        audioSource.clip = language == Language.Turkish ? turkishClip : englishClip;

        if (wasPlaying && restartIfPlaying && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
