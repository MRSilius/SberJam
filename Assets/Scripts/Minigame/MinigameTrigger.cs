using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    [field: SerializeField] public bool Active { get; set; }
    [SerializeField] private Minigame _minigame;
    [SerializeField] private MinigameManager _manager;
    private CharacterMovement _movement;

    private void Start()
    {
        _manager = FindObjectOfType<MinigameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Active == false) return;
        if (_movement) return;

        _manager.Minigame = _minigame;
        _manager.ActivateMinigameButton(_minigame.CameraPlacement);
    }

    private void OnTriggerExit(Collider other)
    {
        if (Active == false) return;
        if (_movement) return;

        _manager.DeactivateMinigameButton();
    }
}
