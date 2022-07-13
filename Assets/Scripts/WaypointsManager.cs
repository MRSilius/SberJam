using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsManager : MonoBehaviour
{
    [SerializeField] private GameObject _electricWaypoint;
    [SerializeField] private GameObject _tubesWaypoint;
    [SerializeField] private GameObject _weldingWaypoint;
    [SerializeField] private GameObject _extinguisherWaypoint;

    [SerializeField] private GameObject _boxesWaypoint;
    [SerializeField] private GameObject _endBoxesWaypoint;
    [SerializeField] private GameObject _screwWaypoint;
    [SerializeField] private GameObject _computerWaypoint;

    public MinigameManager.GameType _type;

    public void ChangeWaypointState(MinigameManager.GameType type, bool value)
    {
        switch (type)
        {
            case MinigameManager.GameType.Electric:
                _electricWaypoint.SetActive(value);
                break;
            case MinigameManager.GameType.Tubes:
                _tubesWaypoint.SetActive(value);
                break;
            case MinigameManager.GameType.Welding:
                _weldingWaypoint.SetActive(value);
                break;
            case MinigameManager.GameType.Extinguisher:
                _extinguisherWaypoint.SetActive(value);
                break;
            case MinigameManager.GameType.Computer:
                _computerWaypoint.SetActive(value);
                break;
            case MinigameManager.GameType.Boxes:
                _boxesWaypoint.SetActive(value);
                break;
            case MinigameManager.GameType.BoxesEnd:
                _endBoxesWaypoint.SetActive(value);
                break;
            case MinigameManager.GameType.Screw:
                _screwWaypoint.SetActive(value);
                break;
        }
    }
}
