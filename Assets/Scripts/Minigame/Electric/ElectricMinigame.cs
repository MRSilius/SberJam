using UnityEngine;

public class ElectricMinigame : MonoBehaviour
{
    public MinigameManager.GameType Type;

    [SerializeField] private Slider[] _sliders;
    [SerializeField] private Switch[] _switches;

    [SerializeField] private MinigameTrigger _trigger;
    private MinigameManager _manager;
    private GameManager _gameManager;

    [SerializeField] private WaypointTimer _timer;
    [SerializeField] private float _timeToRepair;
    private float _timeRemain;

    public bool IsEnabled;
    public bool IsActive;

    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clip;

    void Start()
    {
        _manager = FindObjectOfType<MinigameManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void SetupMinigame()
    {
        int slidersCount = 2;
        int switchesCount = 3;

        int sl = 0;
        int sw = 0;

        int t = 0;

        while (sl < slidersCount)
        {
            int rNumber = Random.Range(0, _sliders.Length);
            if (_sliders[rNumber].IsActive)
            {
                _sliders[rNumber].Deactivate();
                sl++;
            }
            t++;
        }

        t = 0;

        while (sw < switchesCount)
        {
            int rNumber = Random.Range(0, _switches.Length);
            if (_switches[rNumber].IsActive)
            {
                _switches[rNumber].Deactivate();
                sw++;
            }
            t++;
        }

        _trigger.Active = true;
        IsActive = true;
        _manager = FindObjectOfType<MinigameManager>();
        _manager.ActivateMinigame(Type);
        _source.PlayOneShot(_clip);

        _timeRemain = _timeToRepair;
    }

    public void CheckCondition()
    {
        bool slidersCompleted = false;
        bool swithcesCompleted = false;
        foreach (Slider slider in _sliders)
        {
            if (slider.IsActive == false)
            {
                slidersCompleted = false;
                break;
            }
            else
            {
                slidersCompleted = true;
            }
        }

        foreach (Switch sw in _switches)
        {
            if (sw.IsActive == false)
            {
                swithcesCompleted = false;
                break;
            }
            else
            {
                swithcesCompleted = true;
            }
        }

        if (!slidersCompleted || !swithcesCompleted) return;
        MinigameComplete();
    }

    private void MinigameComplete()
    {
        _manager.EndMinigame();
        _trigger.Active = false;
        IsActive = false;
    }

    void Update()
    {
        Timer();
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
