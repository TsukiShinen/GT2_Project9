using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Input Event", menuName = "Input Event")]
public class InputEvent : ScriptableObject
{
    private Tank _selectedTank;
    [SerializeField] private GameParameters _parameters;

    public void OnRightClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
        if (hit.collider == null) { return; }

        if (_selectedTank) { 
            Action(hit);
            return;
        }
        SelectedTank(hit);
    }

    public void OnLeftClick()
    {
        UnSelectTank();
    }

    private void SelectedTank(RaycastHit2D hit)
    {
        if (!hit.collider.CompareTag(_parameters.TagTank)) { return; }
        Debug.Log("Select Tank");
        _selectedTank = hit.collider.gameObject.GetComponent<Tank>();
    }

    private void UnSelectTank()
    {
        Debug.Log("UnSelect Tank");
        _selectedTank = null;
    }

    private void Action(RaycastHit2D hit)
    {
        if (hit.collider.CompareTag(_parameters.TagTank)) { Debug.Log("Attack"); }
        else { Debug.Log("Move"); }
        UnSelectTank();
    }
}
