using System.Collections;
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
    
    [SerializeField] private GameObject tankBlue;
    [SerializeField] private GameObject tankRed;

    public Team playerTeam;

    public Transform spawnBlue;
    [SerializeField] private Transform spawnRed;

    [SerializeField] private Vector3[] spawnPositions;


    public void SpawnFromTeam(Team team, int nbr, float time = 0f)
    {
        if (playerTeam == team)
        {
            SpawnInBlueSide(nbr, time);
        }
        else
        {
            SpawnInRedSide(nbr, time);
        }
        
    }
    
    private void SpawnInBlueSide(int nbr, float time = 0f)
    {
        for (var i = 0; i < nbr; i++)
        {
            StartCoroutine(CreateTankAt(tankBlue, spawnBlue.position + spawnPositions[i], time));
        }
    }
    
    private void SpawnInRedSide(int nbr, float time = 0f)
    {
        for (var i = 0; i < nbr; i++)
        {
            StartCoroutine(CreateTankAt(tankRed, spawnRed.position + spawnPositions[i], time));
        }
    }

    private IEnumerator CreateTankAt(GameObject tank, Vector3 position, float time)
    {
        yield return new WaitForSeconds(time);

        CreateTankAt(tank, position);
    }

    private void CreateTankAt(GameObject tank, Vector3 position)
    {
        var tankTmp = Instantiate(tank, position + new Vector3(0, 0, -9), quaternion.identity).GetComponent<Tank>();
        
        if (tankTmp.Team == playerTeam) 
        {
            OnBlueTankCreated?.Invoke(tankTmp);
        }
        else
        {
            OnRedTankCreated?.Invoke(tankTmp);
        }
    }
}
