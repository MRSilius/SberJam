using UnityEngine;
using UnityEngine.UI;

public class WeldingMinigame : MonoBehaviour
{
    [SerializeField] private WeldingPart[] _parts;
    private MinigameManager _manager;
    private GameManager _gameManager;
    [SerializeField] private MinigameTrigger _trigger;

    [SerializeField] private GameObject _fuel;

    public MinigameManager.GameType Type;

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
        _fuel.SetActive(true);

        foreach (WeldingPart part in _parts)
        {
            part.BreakWelding();
        }
        _trigger.Active = true;
        IsActive = true;
        _manager = FindObjectOfType<MinigameManager>();
        _manager.ActivateMinigame(Type);
        
        _progressText.text = "0%";
        _audio.SetActive(true);
        _timeRemain = _timeToRepair;
    }

    public void CheckCondition()
    {
        float value = 0;

        foreach (WeldingPart part in _parts)
        {
            value += part.Condition / _parts.Length;
        }

        _progressText.text = Mathf.RoundToInt(value * 100).ToString() + "%";

        if (value > 0.9)
        {
            MinigameComplete();
        }
    }

    private void MinigameComplete()
    {
        _fuel.SetActive(false);
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
