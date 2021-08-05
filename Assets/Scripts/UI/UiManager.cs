using Invector.vCharacterController;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject puzzleOverlayUi;

    [SerializeField] 
    private vThirdPersonInput playerInput;
    
    void Start()
    {
        if (!puzzleOverlayUi)
        {
            puzzleOverlayUi = GameObject.FindWithTag(TagConstants.PUZZLE_UI);
        }

        if (!playerInput)
        {
            playerInput = GameObject.FindWithTag(TagConstants.PLAYER).GetComponent<vThirdPersonInput>();
        }

        puzzleOverlayUi.SetActive(false);
    }

    public void SetPuzzleOverlayActive(bool active)
    {
        puzzleOverlayUi.SetActive(active);
        playerInput.rotateCameraXInput = !active ? ControlConstants.MOUSE_X : ControlConstants.DISABLED;
        playerInput.rotateCameraYInput = !active ? ControlConstants.MOUSE_Y : ControlConstants.DISABLED;
    }
}
