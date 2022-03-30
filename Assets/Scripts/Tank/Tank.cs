using UnityEngine;

public class Tank : StateMachine<Tank>
{
    public GameParameters GameParameters;
    [SerializeField] private Team _team;
    public Team Team => _team;

    public float Speed { get; set; }
    public Vector3 PositionToGo { get; set; }
    public Transform Target { get; set; }
    public TankStates States { get; private set; }
    public string NextState { get; set; }
    public float TimerShoot { get; set; }

    private void Awake()
    {
        States = new TankStates();
    }

    void Start()
    {
        Speed = GameParameters.TankSpeed;

        _currentState = States.Idle;
    }

    public override void Update()
    {
        base.Update();
        if(TimerShoot > 0)
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
