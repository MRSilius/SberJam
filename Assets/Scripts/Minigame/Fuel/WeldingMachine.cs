using UnityEngine;

public class WeldingMachine : MonoBehaviour
{
    private Vector3 _mousePos;
    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _weldingMachine;
    private WeldingMinigame _minigame;

    [SerializeField] private GameObject _audio;

    void Start()
    {
        _camera = Camera.main;
        _minigame = FindObjectOfType<WeldingMinigame>();
    }

    void Update()
    {
        if (!_minigame.IsEnabled)
        {
            _weldingMachine.SetActive(false);
            _audio.SetActive(false);
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _mousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            _mousePos = Input.mousePosition;

            RaycastHit hit;

            Vector3 needlePos = new Vector3();

            Ray ray = _camera.ScreenPointToRay(_mousePos);

            if (Physics.Raycast(ray, out hit, 10))
            {
                _weldingMachine.transform.position = hit.point;
                needlePos = hit.point;

                if (hit.transform.TryGetComponent(out WeldingPart part))
                {
                    _weldingMachine.SetActive(true);
                    _audio.SetActive(true);
                    _minigame.CheckCondition();

                    part.Condition += Time.deltaTime * 0.75f;
                }
            }
            else
            {
                _weldingMachine.transform.position = new Vector3(_mousePos.x, _mousePos.y, 0.9222f);
            }
        }
        else
        {
            _weldingMachine.SetActive(false);
            _audio.SetActive(false);
        }
    }
}
