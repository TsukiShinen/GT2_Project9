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
    private bool isAlive = true;
    public float LifePoints;

    private void Awake()
    {
        Movement = GetComponent<Movement>();
        Attack = GetComponent<Attack>();
    }

    private void Start()
    {
        Movement.Speed = parameters.TankSpeed;
        LifePoints = parameters.TankHealth;
    }

    private void Update()
    {
        if (!isAlive) { return; }
        if (LifePoints <= 0)
        {
            isAlive = false;
            StartCoroutine(TankDeath());
        }
    }

    public void GoTo(Vector2 position)
    {
        PositionToGo = position;
        var path = pathFindingController.GeneratePath(transform.position, position);
        Movement.SetPath(path);
    }

    private IEnumerator TankDeath()
    {
        
        _explosion.SetTrigger("TankDeath");
        yield return new WaitForSeconds(0.333f);
        _tankBase.SetActive(false);
        _tankTurret.SetActive(false);
        _tankDestrBase.SetActive(true);
        _tankDestrTurret.SetActive(true);
        yield return new WaitForSeconds(0.1666f);
    }

}
