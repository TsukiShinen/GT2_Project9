using UnityEngine;
using System.Collections;

public class Tank : StateMachine<Tank>
{
    [SerializeField] private GameParameters _gameParameters;
    [SerializeField] private Team _team;
    public Team Team => _team;

    public float Speed { get; set; }
    public Vector3 PositionToGo { get; private set; }

    public TankStates States { get; private set; }
    public string NextState { get; set; }

    private void Awake()
    {
        States = new TankStates();
    }

    void Start()
    {
        Speed = _gameParameters.TankSpeed;

        _currentState = States.Idle;
    }

    public override void Update()
    {
        base.Update();
    }

    public void GoTo(Vector3 positionToGo)
    {
        PositionToGo = positionToGo;
        NextState = "GoTo";
    }
}
