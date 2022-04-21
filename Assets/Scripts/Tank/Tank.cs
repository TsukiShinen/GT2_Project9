using System.Collections;
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

    public PathFinding.PathFindingController pathFindingController;
    public Vector3 PositionToGo { get; set; }

    [SerializeField] private GameObject _tankBase;
    [SerializeField] private GameObject _tankTurret;
    [SerializeField] private GameObject _tankDestrBase;
    [SerializeField] private GameObject _tankDestrTurret;
    [SerializeField] private Animator _explosion;
    [SerializeField] private Animator _tracks;

    public bool isDead;
    
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int Death = Animator.StringToHash("TankDeath");

    private void Awake()
    {
        isDead = false;
        Movement = GetComponent<Movement>();
        Attack = GetComponent<Attack>();
    }

    private void Start()
    {
        Movement.Speed = parameters.TankSpeed;
    }

    private void Update()
    {
        _tracks.SetBool(IsMoving, Movement.isMoving);
    }

    public void GoTo(Vector2 position)
    {
        PositionToGo = position;
        var path = pathFindingController.GeneratePath(transform.position, position);
        Movement.SetPath(path);
    }

    public IEnumerator TankDeath()
    {
        isDead = true;
        _explosion.SetTrigger(Death);
        yield return new WaitForSeconds(0.1666f);
        _tankBase.SetActive(false);
        _tankTurret.SetActive(false);
        _tankDestrBase.SetActive(true);
        _tankDestrTurret.SetActive(true);
        GetComponent<Collider2D>().enabled = false;
        Movement.enabled = false;
        Attack.enabled = false;
        yield return new WaitForSeconds(1f);
        Spawn.Instance.SpawnFromTeam(team, 1, 1f);
        Destroy(gameObject);
    }

}
