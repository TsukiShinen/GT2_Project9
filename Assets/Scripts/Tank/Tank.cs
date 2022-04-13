using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameParameters parameters;
    [SerializeField] private Team team;
    public Team Team => team;
    public GameObject SelectionCircle;
    public Movement Movement { get; private set; }
    public Attack Attack { get; private set; }

    private StateMachine<Tank> _stateMachine;
    public string NextState { get; set; }

    public PathFinding.PathFindingController pathFindingController;
    public Vector3 PositionToGo { get; set; }

    private void Awake()
    {
        _stateMachine = new StateMachine<Tank>();
        _stateMachine.Init(this);

        Movement = GetComponent<Movement>();
        Attack = GetComponent<Attack>();
    }

    private void Start()
    {
        Movement.Speed = parameters.TankSpeed;

        _stateMachine.ChangeState(TankStates.Idle);
    }

    public void GoTo(Vector2 position)
    {
        PositionToGo = position;
        var path = pathFindingController.GeneratePath(transform.position, position);
        Movement.SetPath(path);
    }

    public void Update()
    {
        _stateMachine.Update();
    }
}
