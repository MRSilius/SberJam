using UnityEngine;

public class Slider : MonoBehaviour
{
    public bool IsActive;

    [SerializeField] private GameObject _activeLight;
    [SerializeField] private GameObject _deactiveLight;

    [SerializeField] private float _yPositionMax;
    [SerializeField] private float _yPositionMin;

    [SerializeField] private float _yActivePosition;

    private Vector3 _startMousePosition;
    private Vector3 _startSliderPosition;
    private Vector3 _mouseOffset;

    private ElectricMinigame _minigame;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _clip;

    private void Start()
    {
        _minigame = FindObjectOfType<ElectricMinigame>();

        CheckState();
    }

    public void Deactivate()
    {
        transform.position = new Vector3(transform.position.x, _yPositionMin, transform.position.z);
        CheckState();
    }
    public void Activate()
    {
        transform.position = new Vector3(transform.position.x, _yPositionMax, transform.position.z);
        CheckState();
    }

    private void OnMouseDown()
    {
        if (!_minigame.IsEnabled) return;

        _startMousePosition = Input.mousePosition;
        _startSliderPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        if (!_minigame.IsEnabled) return;

        _mouseOffset = _startMousePosition - Input.mousePosition;
        Vector3 mouseYOffset = new Vector3(0, _mouseOffset.y / 1080 *2, 0);
        Vector3 latePosition = _startSliderPosition - mouseYOffset;

        transform.position = Vector3.MoveTowards(transform.position, latePosition, Time.deltaTime);

        float yPosition = transform.position.y;
        yPosition = Mathf.Clamp(yPosition, _yPositionMin, _yPositionMax);
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
    }

    private void CheckState()
    {
        if (transform.position.y >= _yActivePosition)
        {
            IsActive = true;
            _deactiveLight.SetActive(false);
            _activeLight.SetActive(true);
        }
        else
        {
            IsActive = false;
            _deactiveLight.SetActive(true);
            _activeLight.SetActive(false);
        }
    }

    private void Update()
    {
        if(transform.position.y >= _yActivePosition)
        {
            if (IsActive) return;

            IsActive = true;
            _deactiveLight.SetActive(false);
            _activeLight.SetActive(true);

            _audio.PlayOneShot(_clip);

            _minigame.CheckCondition();
        }
        else
        {
            if (!IsActive) return;

            IsActive = false;
            _deactiveLight.SetActive(true);
            _activeLight.SetActive(false);

            _audio.PlayOneShot(_clip);
        }
    }
}
