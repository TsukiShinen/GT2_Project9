using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputEvent _inputEvent;
    [SerializeField] private GameParameters parameters;
    [SerializeField] private Team playerTeam;

    private void Awake()
    {
        _inputEvent = new InputEvent(parameters, playerTeam);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _inputEvent.OnLeftClick();
        }
        if (Input.GetMouseButtonDown(1))
        {
            _inputEvent.OnRightClick();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _inputEvent.OnLeftClickRelease();
        }

        if (Input.GetKeyDown("s"))
        {
            _inputEvent.StopAllAction();
        }
    }

    private void OnGUI()
    {
        _inputEvent.OnGUI();
    }
}
