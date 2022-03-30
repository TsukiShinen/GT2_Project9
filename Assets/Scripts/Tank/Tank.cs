using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameParameters GameParameters;
    [SerializeField] private Team _team;
    public Team Team => _team;

    public float Speed { get; set; }
    public Vector3 PositionToGo { get; set; }
    public Transform Target { get; set; }

    private StateMachine<Tank> _stateMachine;
    public string NextState { get; set; }
    public float TimerShoot { get; set; }

    private void Awake()
    {
        _stateMachine = new StateMachine<Tank>();
        _stateMachine.Init(this);
    }

    void Start()
    {
        Speed = GameParameters.TankSpeed;

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

    public void Move(Vector3 pos)
    {
        if (Vector3.Distance(pos, transform.position) > 0.1f)
        {
            Vector3 targetDir = pos - transform.position;
            float angle = Vector2.SignedAngle(targetDir, transform.up);
            if (angle > 1f || angle < -1f)
            {
                transform.Rotate(new Vector3(0, 0, 1f * -Mathf.Sign(angle)));
            }
            else
            {
                transform.position += transform.up * Speed * Time.deltaTime;
            }
        }
    }
}
