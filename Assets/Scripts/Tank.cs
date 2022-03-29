using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour
{
    [SerializeField] private GameParameters _gameParameters;
    [SerializeField] private Team _team;
    public Team Team => _team;
    public float Speed;

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
        GoTo(new Vector3(-10, -10));
    }

    void GoTo(Vector3 EndPosition)
    {
        float step = _gameParameters.TankSpeed * Time.deltaTime;
        transform.position += transform.up * step;

        Vector3 targetDir = EndPosition - transform.position;
        
        float angle = Vector3.Angle(targetDir, transform.up);
        if (angle > 1f)
        {
            transform.Rotate(new Vector3(0, 0, 1));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(-10,-10),transform.position);
        Gizmos.DrawRay(transform.position, transform.up);
    }
}
