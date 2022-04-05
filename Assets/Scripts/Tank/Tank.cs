using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameParameters parameters;
    [SerializeField] private Team team;
    public Team Team => team;
    public GameObject bullet;
    public Transform canon;

    public Movement Movement { get; private set; }
    public Transform Target { get; set; }

    private StateMachine<Tank> _stateMachine;
    public string NextState { get; set; }
    public float TimerShoot { get; set; }
    public bool CanShoot => TimerShoot <= 0;

    private void Awake()
    {
        _stateMachine = new StateMachine<Tank>();
        _stateMachine.Init(this);

        Movement = GetComponent<Movement>();
    }

    void Start()
    {
        Movement.Speed = parameters.TankSpeed;

        _stateMachine.ChangeState(TankStates.Idle);
    }

    public void Update()
    {
        _stateMachine.Update();
        
        if (TimerShoot > 0)
        {
            TimerShoot -= Time.deltaTime;
        }
    }
}
