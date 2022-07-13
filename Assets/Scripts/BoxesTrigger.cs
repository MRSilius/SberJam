using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesTrigger : MonoBehaviour
{
    [SerializeField] private bool _dropPlace;
    [SerializeField] private MinigameManager _manager;
    private BoxesPlacement _placement;
    private CharacterMovement _movement;
    private ScrewMinigame _screw;

    private void Start()
    {
        _manager = FindObjectOfType<MinigameManager>();
        _placement = FindObjectOfType<BoxesPlacement>();
        _movement = FindObjectOfType<CharacterMovement>();
        _screw = FindObjectOfType<ScrewMinigame>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_movement.WithBox && _dropPlace)
        {
            _movement.DropBox();
            _screw.AddBox();
            return;
        }

        if (_dropPlace) return;
        if (_placement.Boxes.Count <= 0) return;

        _manager.ActivateBoxesButton();
    }

    private void OnTriggerExit(Collider other)
    {
        //if (_placement.Boxes.Count <= 0) return;

        _manager.DeactivateBoxesButton();
    }
}
