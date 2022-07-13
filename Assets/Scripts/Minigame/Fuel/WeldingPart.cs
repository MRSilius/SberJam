using UnityEngine;

public class WeldingPart : MonoBehaviour
{
    [SerializeField] private Transform _mesh;

    public bool IsPressed;
    public float Condition;

    public void BreakWelding()
    {
        Condition = 0;
        _mesh.localScale = Vector3.zero;
    }

    private void Update()
    {
        //if (!IsPressed) return;

        if (Condition > 1)
        {
            Condition = 1;
        }

        _mesh.localScale = Vector3.one * Condition;
    }

    private void OnMouseDown()
    {
        IsPressed = true;
    }

    private void OnMouseUp()
    {
        IsPressed = false;
    }
}
