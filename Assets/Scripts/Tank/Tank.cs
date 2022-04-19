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
    

    private void Awake()
    {
        Movement = GetComponent<Movement>();
        Attack = GetComponent<Attack>();
    }

    private void Start()
    {
        Movement.Speed = parameters.TankSpeed;
    }

    private void Update()
    {
        _tracks.SetBool("isMoving", Movement.isMoving);
    }

    public void GoTo(Vector2 position)
    {
        PositionToGo = position;
        var path = pathFindingController.GeneratePath(transform.position, position);
        Movement.SetPath(path);
    }

    public IEnumerator TankDeath()
    {
        _explosion.SetTrigger("TankDeath");
        yield return new WaitForSeconds(0.1666f);
        _tankBase.SetActive(false);
        _tankTurret.SetActive(false);
        _tankDestrBase.SetActive(true);
        _tankDestrTurret.SetActive(true);
        Movement.enabled = false;
        Attack.enabled = false;
        yield return new WaitForSeconds(0.3333f);
    }

}
