using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesPlacement : MonoBehaviour
{
    [SerializeField] private Transform[] _placements;

    [SerializeField] private GameObject _boxPrefab;
    public List< GameObject> Boxes;
    private CharacterMovement _movement;
    private WaypointsManager _waypoints;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _clip;

    private void Start()
    {
        _movement = FindObjectOfType<CharacterMovement>();
        _waypoints = FindObjectOfType<WaypointsManager>();
    }

    public void RemoveBox()
    {
        GameObject go = Boxes[Boxes.Count-1];
        Boxes.Remove(go);
        Destroy(go);
        _movement.TakeBox();
        CheckBoxes();
    }

    public void OrderBox()
    {
        StartCoroutine(OrderAfterTime());
    }

    private IEnumerator OrderAfterTime()
    {
        yield return new WaitForSeconds(20);

        AddBox();
    }

    public void AddBox()
    {
        if (Boxes.Count >= 3) return;

        GameObject go = Instantiate(_boxPrefab, _placements[Boxes.Count]);
        Boxes.Add(go);
        CheckBoxes();
        _audio.PlayOneShot(_clip);
    }

    private void CheckBoxes()
    {
        if(Boxes.Count > 0)
        {
            _waypoints.ChangeWaypointState(MinigameManager.GameType.Boxes, true);
        }
        else
        {
            _waypoints.ChangeWaypointState(MinigameManager.GameType.Boxes, false);
        }
    }
}
