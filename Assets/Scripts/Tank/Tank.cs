using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameParameters parameters;
    [SerializeField] private Team team;
    public Team Team => team;

    public Movement Movement { get; private set; }
    public Attack Attack { get; private set; }

    private StateMachine<Tank> _stateMachine;
    public string NextState { get; set; }

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

    public void Update()
    {
        _stateMachine.Update();
    }
}
