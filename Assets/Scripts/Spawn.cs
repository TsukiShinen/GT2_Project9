using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    #region Singleton

    public static Spawn Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    public delegate void MyTankDelegate(Tank tank);
    public MyTankDelegate OnBlueTankCreated;
    public MyTankDelegate OnRedTankCreated;

    [SerializeField]  private Team playerTeam;

    public Transform spawnBlue;
    [SerializeField] private Transform spawnRed;

    [SerializeField] private Vector3[] spawnPositions;

    public void SpawnInBlueSide(List<GameObject> tanks)
    {
        for (var i = 0; i < tanks.Count; i++)
        {
            CreateTankAt(tanks[i], spawnBlue.position + spawnPositions[i]);
        }
    }
    
    public void SpawnInRedSide(List<GameObject> tanks)
    {
        for (var i = 0; i < tanks.Count; i++)
        {
            CreateTankAt(tanks[i], spawnRed.position + spawnPositions[i]);
        }
    }

    private void CreateTankAt(GameObject tank, Vector3 position)
    {
        var TankTmp = Instantiate(tank, position + new Vector3(0, 0, -9), quaternion.identity).GetComponent<Tank>();

        if (TankTmp.Team == playerTeam) 
        {
            OnBlueTankCreated?.Invoke(TankTmp);
        }
        else
        {
            OnRedTankCreated?.Invoke(TankTmp);
        }
    }
}
