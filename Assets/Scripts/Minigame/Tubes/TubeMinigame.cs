using UnityEngine;

public class TubeMinigame : MonoBehaviour
{
    public MinigameManager.GameType Type;

    [SerializeField] private TubeHole[] _holes;
    [SerializeField] private MinigameTrigger _trigger;
    private MinigameManager _manager;
    private GameManager _gameManager;

    [SerializeField] private WaypointTimer _timer;
    [SerializeField] private float _timeToRepair;
    private float _timeRemain;

    [SerializeField] private GameObject _audio;

    public bool IsEnabled;
    public bool IsActive;

    private void Start()
    {
        _manager = FindObjectOfType<MinigameManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        Timer();
    }

    public void SetupMinigame()
    {
        OpenTubes(Random.Range(4, 6));
        _manager = FindObjectOfType<MinigameManager>();
        _manager.ActivateMinigame(Type);
        _audio.SetActive(true);
    }

    private void OpenTubes(int count)
    {
        int holesCount = 0;
        int holeNumber;

        while (holesCount < count)
        {
            holeNumber = Random.Range(0, _holes.Length);
            if (_holes[holeNumber].Opened)
            {
                continue;
            }
            else
            {
                _holes[holeNumber].OpenHole();
                holesCount++;
            }
        }

        _trigger.Active = true;
        IsActive = true;

        _timeRemain = _timeToRepair;
    }

    public void CheckHoles()
    {
        bool completed = false;
        foreach (TubeHole tube in _holes)
        {
            if (tube.Opened)
            {
                completed = false;
                break;
            }
            else
            {
                completed = true;
            }
        }

        if (!completed) return;
        MinigameComplete();
    }

    private void MinigameComplete()
    {
        _manager.EndMinigame();
        _trigger.Active = false;
        IsActive = false;

        _audio.SetActive(false);
    }
    private void Timer()
    {
        if (!IsActive) return;

        _timeRemain -= Time.deltaTime;
        _timer.SetupTimer(_timeRemain / _timeToRepair);

        if (_timeRemain <= 0)
        {
            //LOSE
            _gameManager.EndGame(false);
        }
    }
}
