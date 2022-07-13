using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeHole : MonoBehaviour
{
    [field: SerializeField] public bool Opened { get; set; }
    [SerializeField] private GameObject _hole; 
    [SerializeField] private GameObject _holePlug;

    private TubeMinigame _minigame;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _clip;

    private void Start()
    {
        _minigame = FindObjectOfType<TubeMinigame>();
    }

    private void OnMouseDown()
    {
        if (!_minigame.IsEnabled) return;
        if (!Opened) return;

        CloseHole();
        Opened = false;
    }

    public void OpenHole()
    {
        _hole.SetActive(true);
        _holePlug.SetActive(false);
        Opened = true;
    }

    private void CloseHole()
    {
        _hole.SetActive(false);
        _holePlug.SetActive(true);
        Opened = false;
        _minigame.CheckHoles();

        _audio.PlayOneShot(_clip);
    }
}
