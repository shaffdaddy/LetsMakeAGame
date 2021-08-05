using UnityEngine;

public class NPCInteractionBounds : MonoBehaviour
{
    [SerializeField]
    private UiManager uiManager;

    private void Start()
    {
        if (!uiManager)
        {
            uiManager = GameObject.FindWithTag(TagConstants.UI_MANAGER).GetComponent<UiManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(TagConstants.PLAYER))
        {
            uiManager.SetPuzzleOverlayActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(TagConstants.PLAYER))
        {
            uiManager.SetPuzzleOverlayActive(false);
        }
    }
}
