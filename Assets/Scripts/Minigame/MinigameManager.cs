using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private GameObject _minigameButtonObject;
    [SerializeField] private GameObject _boxesButtonObject;

    [SerializeField] GameObject _closeMinigameButtonObject;
    [SerializeField] GameObject _playerModel;
    [SerializeField] GameObject _gameUI;

    public Minigame Minigame;
    private Button _minigameButton;
    private CameraMovement _camera;

    private WaypointsManager _waypoints;

    public enum GameType { Electric, Tubes, Welding, Extinguisher, Computer, Boxes, Screw, BoxesEnd }

    private void Start()
    {
        _camera = FindObjectOfType<CameraMovement>();
        _minigameButton = _minigameButtonObject.GetComponent<Button>();
        _waypoints = FindObjectOfType<WaypointsManager>();
    }

    public void ActivateMinigameButton(Transform cameraPlacement)
    {
        _minigameButtonObject.SetActive(true);
        _minigameButton.onClick.AddListener(() => OpenMinigame(cameraPlacement));
    }

    public void DeactivateMinigameButton()
    {
        _minigameButtonObject.SetActive(false);
        _minigameButton.onClick.RemoveAllListeners();
    }

    public void ActivateBoxesButton()
    {
        _boxesButtonObject.SetActive(true);
    }
    public void DeactivateBoxesButton()
    {
        _boxesButtonObject.SetActive(false);
    }

    private void OpenMinigame(Transform cameraPlacement)
    {
        Minigame.IsActive = true;
        _playerModel.SetActive(false);
        _gameUI.SetActive(false);
        _waypoints.ChangeWaypointState(Minigame.Type, false);

        _camera.SetTarget(cameraPlacement);
        _minigameButtonObject.SetActive(false);
        _closeMinigameButtonObject.SetActive(true);

        if (Minigame.MinigameUI)
        {
            Minigame.MinigameUI.SetActive(true);
        }
    }

    //Close for ui
    public void CloseMinigame()
    {
        Minigame.IsActive = false;
        _playerModel.SetActive(true);
        _gameUI.SetActive(true);
        _waypoints.ChangeWaypointState(Minigame.Type, true);

        _camera.RemoveTarget();
        _minigameButtonObject.SetActive(true);
        _closeMinigameButtonObject.SetActive(false);


        if (Minigame.MinigameUI)
        {
            Minigame.MinigameUI.SetActive(false);
        }
    }

    //Close for end minigame
    public void EndMinigame()
    {
        CloseMinigame();
        _waypoints.ChangeWaypointState(Minigame.Type, false);
        _minigameButtonObject.SetActive(false);
    }

    public void ActivateMinigame(GameType type)
    {
        _waypoints = FindObjectOfType<WaypointsManager>();  
        _waypoints.ChangeWaypointState(type, true);
    }
}
