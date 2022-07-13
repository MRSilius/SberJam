using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ComputerMinigame : MonoBehaviour
{
    public MinigameManager.GameType Type;

    [SerializeField] private GameObject[] _spam;
    [SerializeField] private Transform[] _spamHandlers;
    [SerializeField] private GameObject _waitBeforeOrder;
    [SerializeField] private Button _orderButton;
    [SerializeField] private Text _waitTime;

    private BoxesPlacement _boxes;
    private MinigameManager _manager;

    public bool IsEnabled;

    private void Start()
    {
        _boxes = FindObjectOfType<BoxesPlacement>();
        _manager = FindObjectOfType<MinigameManager>();
        SetupMinigame();
    }

    public void SetupMinigame()
    {
        foreach (GameObject go in _spam)
        {
            go.SetActive(true);
        }
        _manager.ActivateMinigame(Type);
    }

    public void Order()
    {
        MinigameComplete();
        _boxes.OrderBox();
        _waitBeforeOrder.SetActive(true);
        _orderButton.enabled = false;
        StartCoroutine(WaitBeforeOrder());
    }

    IEnumerator WaitBeforeOrder()
    {
        int t = 30;
        while (t > 0)
        {
            _waitTime.text = t.ToString();
            yield return new WaitForSeconds(1);
            t--;
        }
        _waitBeforeOrder.SetActive(false);
        _orderButton.enabled = true;
        SetupMinigame();
    }

    private void MinigameComplete()
    {

    }
}
