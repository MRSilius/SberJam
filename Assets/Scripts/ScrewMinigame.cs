using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrewMinigame : MonoBehaviour
{
    [SerializeField] private GameObject[] _screws;
    [SerializeField] private Image _powerFill;
    private MinigameManager _manager;
    private GameManager _gameManager;
    [SerializeField] private MinigameTrigger _trigger;
    [SerializeField] private WaypointsManager _waypoints;

    private float _force;
    [SerializeField] private float _removingForce;
    [SerializeField] private float _addingForce;

    int _screwCount;

    public bool IsEnabled;

    private int _boxes;

    private void Start()
    {
        _manager = FindObjectOfType<MinigameManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _waypoints = FindObjectOfType<WaypointsManager>();
        SetupMinigame();
        CheckState();
    }

    private void Update()
    {
        if (_force > 0)
        {
            _force -= _removingForce * Time.deltaTime;
        }
        else
        {
            _force = 0;
        }

        if (_force >= 1)
        {
            _force = 1;
            AddScrew();
        }

        _powerFill.fillAmount = _force;
    }

    public void AddForce()
    {
        _force += _addingForce;
    }

    public void AddBox()
    {
        _boxes++;
        CheckState();
    }

    public void RemoveBox()
    {
        _boxes--;
        CheckState();
    }

    public void SetupMinigame()
    {
        foreach (GameObject go in _screws)
        {
            go.SetActive(false);
        }
        _screwCount = 0;
    }

    public void AddScrew()
    {
        if (_screwCount < 4)
        {
            _screws[_screwCount].SetActive(true);
            _screwCount++;
            _force = 0;
        }

        if (_screwCount == 4)
        {
            MinigameComplete();
        }
    }

    private void CheckState()
    {
        if (_boxes > 0)
        {
            _trigger.Active = true;
            _waypoints.ChangeWaypointState(MinigameManager.GameType.Screw, true);
        }
        else
        {
            _trigger.Active = false;
            _waypoints.ChangeWaypointState(MinigameManager.GameType.Screw, false);
        }
    }

    private void MinigameComplete()
    {
        _manager.EndMinigame();
        RemoveBox();
        StartCoroutine(WaitBeforeRestart());

        _gameManager.AddPlate();
    }

    private IEnumerator WaitBeforeRestart()
    {
        yield return new WaitForSeconds(1);
        SetupMinigame();
    }
}
