using UnityEngine;
using System.Collections.Generic;

public class Tank : MonoBehaviour
{
    [SerializeField] private GameParameters _gameParameters;
    [SerializeField] private Team _team;
    public Team Team => _team;

    private IState state;

    public Tank(IState newstate)
    {
        state = newstate;
    }

    public void Request()
    {
        state.Handle(this);
    }

    public IState State
    {
        get { return state; }
        set { state = value; }
    }
    void Start()
    {
    }

    void Update()
    {
        GoTo(new Vector3(10, 10,0));
    }

    void GoTo(Vector3 EndPosition)
    {
        float step = _gameParameters.TankSpeed * Time.deltaTime;
        Debug.Log(Vector3.Angle(transform.up,EndPosition));
        Quaternion target = Quaternion.Euler(0, 0, Vector3.Angle(EndPosition, transform.up));
        transform.rotation = target;
        transform.position = Vector3.MoveTowards(transform.position, EndPosition, step);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position,transform.up);

    }
}
