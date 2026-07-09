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
        }
    }

    private void ResetExhibit()
    {
        triggered = false;

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
            characterAnimator.Play(revealAnimationState);
        }

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
