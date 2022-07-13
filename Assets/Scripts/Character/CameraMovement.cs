using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _target;

    private Quaternion _startRotation;

    public float _smooth;
    public float _cameraSmooth;
    public Vector3 offset;

    private Animator selfAnimator;

    private void Start()
    {
        _player = FindObjectOfType<CharacterMovement>().transform;
        _startRotation = transform.rotation;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
    public void RemoveTarget()
    {
        _target = null;
    }

    private void FixedUpdate()
    {
        if (_target)
        {
            Vector3 latePostion = new Vector3(_target.position.x, _target.position.y, _target.position.z);
            Vector3 smoothPosition = Vector3.Lerp(transform.position, latePostion, Time.deltaTime * _smooth * 2);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, _target.rotation, Time.deltaTime * _cameraSmooth);
            transform.position = smoothPosition;
        }
        else
        {
            Vector3 latePostion = new Vector3(_player.position.x, _player.position.y, _player.position.z);
            Vector3 smoothPosition = Vector3.Lerp(transform.position, latePostion + offset, Time.deltaTime * _smooth);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, _startRotation, Time.deltaTime * _cameraSmooth);
            transform.position = smoothPosition;
        }

    }
}
