using UnityEngine;

public class ExhibitButton : MonoBehaviour
{
    [SerializeField] private GameObject buttonRoot;
    [SerializeField] private GameObject characterRoot;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private string revealAnimationState = "Z_Idle";
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool hideButtonAfterTrigger = true;
    [SerializeField] private bool snapCharacterToFloor = true;
    [SerializeField] private float characterHeightBelowButton = -0.8f;
    [SerializeField] private float resetDistance = 2f;

    private bool triggered;
    private bool audioHasStartedPlaying;
    private Transform visitorTransform;

    private void Awake()
    {
        if (characterRoot != null)
        {
            characterRoot.SetActive(false);
        }
    }

    private void Update()
    {
        if (!triggered)
        {
            return;
        }

        if (visitorTransform == null)
        {
            if (Camera.main == null)
            {
                return;
            }
            visitorTransform = Camera.main.transform;
        }

        float distance = Vector3.Distance(visitorTransform.position, transform.position);
        if (distance > resetDistance)
        {
            ResetExhibit();
            return;
        }

        // Stop the (looping) talk animation exactly when the narration audio finishes.
        if (audioSource != null && characterAnimator != null)
        {
            if (audioSource.isPlaying)
            {
                audioHasStartedPlaying = true;
            }
            else if (audioHasStartedPlaying && characterAnimator.speed != 0f)
            {
                characterAnimator.speed = 0f;
            }
        }
    }

    private void ResetExhibit()
    {
        triggered = false;
        audioHasStartedPlaying = false;

        if (audioSource != null)
        {
            audioSource.Stop();
        }

        if (characterRoot != null)
        {
            characterRoot.SetActive(false);
        }

        if (buttonRoot != null)
        {
            buttonRoot.SetActive(true);
        }
    }

    // Wire this to the ListItemButton prefab's Button (Script) > On Click () event.
    public void Reveal()
    {
        if (triggered)
        {
            return;
        }
        triggered = true;

        if (characterRoot != null)
        {
            characterRoot.SetActive(true);

            if (snapCharacterToFloor)
            {
                Vector3 pos = characterRoot.transform.position;
                pos.y = transform.position.y + characterHeightBelowButton;
                characterRoot.transform.position = pos;
            }
        }

        if (characterAnimator != null)
        {
            characterAnimator.speed = 1f;
            characterAnimator.Play(revealAnimationState, 0, 0f);
        }

        audioHasStartedPlaying = false;

        if (audioSource != null)
        {
            audioSource.Play();
        }

        if (hideButtonAfterTrigger && buttonRoot != null)
        {
            buttonRoot.SetActive(false);
        }
    }
}
