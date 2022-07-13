using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Vector3 playerInput;
    private Rigidbody playerRigidbody;
    private CharacterAnimations _animations;

    public Joystick joystick;

    private float _speed;
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _speedWithBox;

    public bool WithBox;

    public List<GameObject> targets;

    [SerializeField] private GameObject _boxInHands;
    private WaypointsManager _waypoints;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _dFootsteps;
    [SerializeField] private AudioClip _sFootsteps;
    [SerializeField] private GameObject _footstepsAudio;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        _animations = GetComponent<CharacterAnimations>();
        _waypoints = FindObjectOfType<WaypointsManager>();
    }

    private void Update()
    {
        playerInput = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        playerRigidbody.velocity = playerInput * _speed;

        if (WithBox)
        {
            _speed = _speedWithBox;
            _audio.clip = _sFootsteps;
        }
        else
        {
            _speed = _defaultSpeed;
            _audio.clip = _dFootsteps;
        }

        if ((playerInput.x > 0 || playerInput.x < 0) || (playerInput.y > 0 || playerInput.y < 0))
        {
            _animations.IsMoving = true;
            _footstepsAudio.SetActive(true);
        }
        else
        {
            _animations.IsMoving = false;
            _footstepsAudio.SetActive(false);
        }

        _animations.WithBox = WithBox;

        Rotate();
    }

    private void Rotate()
    {
        float rotY = Mathf.Atan2(-joystick.Horizontal, -joystick.Vertical) * Mathf.Rad2Deg;
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            //transform.rotation = Quaternion.Euler(0, 0, rotZ);
            Quaternion rot = Quaternion.Euler(0f, rotY - 180, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, Time.deltaTime * 600);
        }
    }

    public void TakeBox()
    {
        WithBox = true;
        _boxInHands.SetActive(WithBox);
        _waypoints.ChangeWaypointState(MinigameManager.GameType.BoxesEnd, true);
    }

    public void DropBox()
    {
        WithBox = false;
        _boxInHands.SetActive(WithBox);
        _waypoints.ChangeWaypointState(MinigameManager.GameType.BoxesEnd, false);
    }
}
