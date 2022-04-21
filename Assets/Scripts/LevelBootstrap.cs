using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBootstrap : MonoBehaviour
{
    [SerializeField] private int nbrTankPerTeam;
    
    [SerializeField] private Team teamBlue;
    [SerializeField] private Team teamRed;

    [SerializeField] private Score score;
    [SerializeField] private PathFinding.PathFindingController pathFindingController;

    [SerializeField] private AudioSO audioLoader;

    private void Start()
    {
        pathFindingController.Init();
        
        // Blue spawn
        Spawn.Instance.SpawnFromTeam(teamBlue, nbrTankPerTeam); 
        
        // Red spawn
        Spawn.Instance.SpawnFromTeam(teamRed, nbrTankPerTeam);
        
        // Camera
        Camera.main.transform.position = Spawn.Instance.spawnBlue.position + new Vector3(0, 0, -10);
        
        score.Clear();

        audioLoader.Play("MenuMusic");
    }
}
