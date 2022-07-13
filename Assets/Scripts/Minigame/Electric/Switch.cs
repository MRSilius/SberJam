using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool IsActive;

    [SerializeField] private Transform _switch;
    [SerializeField] private float _xRotationActive = -125f;
    [SerializeField] private float _xRotationDeactive = -45f;

    [SerializeField] private GameObject _activeLight;
    [SerializeField] private GameObject _deactiveLight;

    private Vector3 _activeRotation;
    private Vector3 _deactiveRotation;

    private ElectricMinigame _minigame;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _clip;

    private void Start()
    {
        _minigame = FindObjectOfType<ElectricMinigame>();

        _activeRotation = new Vector3(_xRotationActive, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        _deactiveRotation = new Vector3(_xRotationDeactive, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        CheckState();
    }

    public void Deactivate()
    {
        IsActive = false;
        CheckState();
    }

    private void ChangeState()
    {
        IsActive = !IsActive;
        _audio.PlayOneShot(_clip);
    }

    private void CheckState()
    {
        if (IsActive)
        {
            _deactiveLight.SetActive(false);
            _activeLight.SetActive(true);
        }
        else
        {
            _deactiveLight.SetActive(true);
            _activeLight.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (!_minigame.IsEnabled) return;

        ChangeState();
        CheckState();
        _minigame.CheckCondition();
    }

    void Update()
    {
        if (IsActive)
        {
            _switch.rotation = Quaternion.RotateTowards(_switch.rotation, Quaternion.Euler(_activeRotation), Time.deltaTime * 600f);
        }
        else
        {
            _switch.rotation = Quaternion.RotateTowards(_switch.rotation, Quaternion.Euler(_deactiveRotation), Time.deltaTime * 600f);
        }
    }
}
