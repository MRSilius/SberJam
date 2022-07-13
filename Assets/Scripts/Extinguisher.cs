using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    private Vector3 _mousePos;
    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _extinguisher;
    [SerializeField] private GameObject _extinguisherModel;
    private ExtinguisherMinigame _minigame;

    [SerializeField] private GameObject _audio;

    private ParticleSystem _particles;

    void Start()
    {
        _camera = Camera.main;
        _minigame = FindObjectOfType<ExtinguisherMinigame>();
        _particles = _extinguisher.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!_minigame.IsEnabled)
        {
            _extinguisherModel.SetActive(false);
            _audio.SetActive(false);
            if (_particles.isPlaying)
            {
                _particles.Stop();
            }
            return;
        }

        _extinguisherModel.SetActive(true);

        if (Input.GetMouseButtonDown(0))
        {
            _mousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            bool isplaying = false;
            _mousePos = Input.mousePosition;
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(_mousePos);

            if (Physics.Raycast(ray, out hit, 10))
            {
                //_extinguisher.transform.position = hit.point;
                Vector3 direction = ray.direction;
                _extinguisher.transform.rotation = Quaternion.LookRotation(direction);

                if (hit.transform.TryGetComponent(out FireSource source))
                {
                    if (!_particles.isPlaying)
                    {
                        _particles.Play();
                    }
                    _audio.SetActive(true);

                    _minigame.CheckCondition();

                    source.FireStrength -= Time.deltaTime * 0.75f;
                }
                else
                {
                    if (_particles.isPlaying)
                    {
                        _particles.Stop();
                    }
                    _audio.SetActive(false);
                }
            }
        }
        else
        {
            if (_particles.isPlaying)
            {
                _particles.Stop();
            }
            _audio.SetActive(false);
        }
    }
}
