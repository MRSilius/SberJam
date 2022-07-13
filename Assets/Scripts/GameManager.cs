using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ElectricMinigame _electric;
    [SerializeField] private TubeMinigame _tubes;
    [SerializeField] private WeldingMinigame _welding;
    [SerializeField] private ExtinguisherMinigame _extinguisher;

    [SerializeField] private ComputerMinigame _computer;

    private int _minigamesCount = 4;

    public MinigameManager.GameType _type;

    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _minigameUI;
    [SerializeField] private GameObject _endUI;

    [SerializeField] private Image _endBG;
    [SerializeField] private Sprite _hEnd;
    [SerializeField] private Sprite _bEnd;

    [SerializeField] private Text _endText;
    [SerializeField] private Color _green;
    [SerializeField] private Color _red;

    [SerializeField] private int _needPlates;
    [SerializeField] private int _currentPlates;
    [SerializeField] private Text _platesText;

    [SerializeField] private float _gameTime = 270;
    [SerializeField] private float _currentTime;
    [SerializeField] private Image _Timerfill;

    private bool _isEnded;

    private void Start()
    {
        _electric = GetComponent<ElectricMinigame>();
        _tubes = GetComponent<TubeMinigame>();
        _welding = GetComponent<WeldingMinigame>();
        _extinguisher = GetComponent<ExtinguisherMinigame>();
        _computer = GetComponent<ComputerMinigame>();

        StartCoroutine(GenerateMinigames());
    }

    private void Update()
    {
        float value = 0;

        _currentTime += Time.deltaTime;

        value = _currentTime / _gameTime;
        _Timerfill.fillAmount = value;

        if (_currentTime >= _gameTime)
        {
            EndGame(false);
        }
    }

    private void GenerateRandomMinigame()
    {
        int rGame = Random.RandomRange(0, _minigamesCount);

        _type = (MinigameManager.GameType)rGame;
        print(_type);

        if (_electric.IsActive && _tubes.IsActive && _welding.IsActive && _extinguisher.IsActive) return;
        ActivateMinigame(_type);
    }

    private IEnumerator GenerateMinigames()
    {
        while (true)
        {
            yield return new WaitForSeconds(17);
            GenerateRandomMinigame();
        }
    }

    private void ActivateMinigame(MinigameManager.GameType type)
    {
        switch (type)
        {
            case MinigameManager.GameType.Electric:
                if (_electric.IsActive)
                {
                    GenerateRandomMinigame();
                }
                else
                {
                    _electric.SetupMinigame();
                }
                break;
            case MinigameManager.GameType.Tubes:
                if (_tubes.IsActive)
                {
                    GenerateRandomMinigame();
                }
                else
                {
                    _tubes.SetupMinigame();
                }
                break;
            case MinigameManager.GameType.Welding:
                if (_welding.IsActive)
                {
                    GenerateRandomMinigame();
                }
                else
                {
                    _welding.SetupMinigame();
                }
                break;
            case MinigameManager.GameType.Extinguisher:
                if (_extinguisher.IsActive)
                {
                    GenerateRandomMinigame();
                }
                else
                {
                    _extinguisher.SetupMinigame();
                }
                break;
            case MinigameManager.GameType.Computer:
                break;
            default:
                break;
        }
    }

    public void AddPlate()
    {
        _currentPlates++;
        UpdateUI();
        if (_currentPlates >= _needPlates)
        {
            EndGame(true);
        }
    }

    public void EndGame(bool state)
    {
        if (_isEnded) return;

        Time.timeScale = 0;

        _gameUI.SetActive(false);
        _minigameUI.SetActive(false);
        _endUI.SetActive(true);

        if (state)
        {
            _endBG.sprite = _hEnd;
            _endText.color = _green;
            _endText.text = "YOU DID IT!";
        }
        else
        {
            _endBG.sprite = _bEnd;
            _endText.color = _red;
            _endText.text = "THE PLANET WAS DESTROYED WITH YOU";
        }
        _isEnded = true;
    }

    private void UpdateUI()
    {
        _platesText.text = _currentPlates.ToString() + "/" + _needPlates.ToString();
    }
}
