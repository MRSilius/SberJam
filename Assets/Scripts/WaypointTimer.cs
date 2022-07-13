using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointTimer : MonoBehaviour
{
    [SerializeField] private Image _timer;
    [SerializeField] private RectTransform _target;
    [SerializeField] private TargetPointer _pointer;

    private RectTransform _selfTransform;

    [SerializeField] private Vector3 _onScreenOffset;

    private void Update()
    {
        if (_pointer.OutOfScreen)
        {
            transform.position = _target.position;
        }
        else
        {
            transform.position = _target.position + _onScreenOffset;
        }

    }

    public void SetupTimer(float value)
    {
        _timer.fillAmount = 1 - value;
    }
}
