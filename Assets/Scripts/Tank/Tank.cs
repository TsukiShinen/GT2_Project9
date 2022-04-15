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

    private void Awake()
    {
        Movement = GetComponent<Movement>();
        Attack = GetComponent<Attack>();
    }

    private void Start()
    {
        Movement.Speed = parameters.TankSpeed;
    }

    public void GoTo(Vector2 position)
    {
        PositionToGo = position;
        var path = pathFindingController.GeneratePath(transform.position, position);
        Movement.SetPath(path);
    }
}
