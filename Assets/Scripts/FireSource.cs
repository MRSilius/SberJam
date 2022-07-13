using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSource : MonoBehaviour
{
    [SerializeField] public float FireStrength;
    [SerializeField] public Transform _fire;

    public void SetFire(float value)
    {
        FireStrength = value;
    }

    private void Update()
    {
        if (FireStrength < 0)
        {
            FireStrength = 0;
        }

        _fire.localScale = Vector3.one * FireStrength;
    }

}
