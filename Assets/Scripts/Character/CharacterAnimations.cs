using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public bool IsMoving;
    public bool WithBox;

    private void Update()
    {
        _animator.SetBool("IsMoving", IsMoving);
        _animator.SetBool("WithBox", WithBox);
    }
}
