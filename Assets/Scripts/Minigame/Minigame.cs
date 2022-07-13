using UnityEngine;

public class Minigame : MonoBehaviour
{
    public MinigameManager.GameType Type;

    [SerializeField] private ElectricMinigame _electric;
    [SerializeField] private TubeMinigame _tubes;
    [SerializeField] private ComputerMinigame _computer;
    [SerializeField] private WeldingMinigame _welding;
    [SerializeField] private ExtinguisherMinigame _extinguisher;

    public GameObject MinigameUI;

    private bool _isActive;
    public bool IsActive
    {
        get { return _isActive; }
        set 
        { 
            _isActive = value;
            EnableGame();
        }
    }
    public Transform CameraPlacement;

    private void EnableGame()
    {
        if (_electric)
        {
            _electric.IsEnabled = IsActive;
        }

        if (_tubes)
        {
            _tubes.IsEnabled = IsActive;
        }

        if (_computer)
        {
            _computer.IsEnabled = IsActive;
        }

        if (_welding)
        {
            _welding.IsEnabled = IsActive;
        }

        if (_extinguisher)
        {
            _extinguisher.IsEnabled = IsActive;
        }
    }
}
