using UnityEngine;
using UnityEngine.UI;

public class ExtinguisherMinigame : MonoBehaviour
{
    public MinigameManager.GameType Type;

    [SerializeField] private FireSource[] _sources;
    private MinigameManager _manager;
    private GameManager _gameManager;
    [SerializeField] private MinigameTrigger _trigger;

    [SerializeField] private WaypointTimer _timer;
    [SerializeField] private float _timeToRepair;
    private float _timeRemain;

    [SerializeField] private Text _progressText;

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
        SetupFire(1);
        _manager = FindObjectOfType<MinigameManager>();
        _manager.ActivateMinigame(Type);

        _audio.SetActive(true);
        _timeRemain = _timeToRepair;
    }

    private void SetupFire(float value)
    {
        foreach (FireSource source in _sources)
        {
            source.SetFire(value);
        }

        _progressText.text = "0%";
        _trigger.Active = true;
        IsActive = true;
    }

    public void CheckCondition()
    {
        float value = 0;

        foreach (FireSource source in _sources)
        {
            value += source.FireStrength / _sources.Length;
        }

        _progressText.text = Mathf.RoundToInt((1f - value) * 100).ToString() + "%";

        if (1 - value > 0.9)
        {
            MinigameComplete();
        }
    }

    private void MinigameComplete()
    {
        SetupFire(0);
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
