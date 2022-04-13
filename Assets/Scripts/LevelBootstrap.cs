using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBootstrap : MonoBehaviour
{
    [SerializeField] private int nbrTankPerTeam;
    [SerializeField] private GameObject tankBlue;
    [SerializeField] private GameObject tankRed;

    [SerializeField] private Score score;
    [SerializeField] private PathFinding.PathFindingController pathFindingController;
    
    private void Start()
    {
        pathFindingController.Init();
        
        // Blue spawn
        var blueTanks = new List<GameObject>();
        for (var i = 0; i < nbrTankPerTeam; i++)
        {
            blueTanks.Add(tankBlue);
        }
        Spawn.Instance.SpawnInBlueSide(blueTanks);
        
        // Red spawn
        var redTanks = new List<GameObject>();
        for (var i = 0; i < nbrTankPerTeam; i++)
        {
            redTanks.Add(tankRed);
        }
        Spawn.Instance.SpawnInRedSide(redTanks);
        
        // Camera
        Camera.main.transform.position = Spawn.Instance.spawnBlue.position + new Vector3(0, 0, -10);
        
        score.Clear();
    }
}
