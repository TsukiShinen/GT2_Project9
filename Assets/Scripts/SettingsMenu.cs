using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFinding;
using TMPro;
using System;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private PathFindingController _pathfinding;
    [SerializeField] private TMP_Dropdown _dropdown;
    private List<Algo> AlgoList = new List<Algo>();
    private List<String> AlgoStringList = new List<String>();

    private void Start()
    {
        //Création d'une list d'algo a partir de l'enum des pathfinders
        foreach(Algo algo in Enum.GetValues(typeof(Algo)))
        {
            AlgoList.Add(algo);
        }

        //Création d'une list de string d'algo a partir de la list d'algo
        foreach (Algo algo in AlgoList)
        {
            AlgoStringList.Add(algo.ToString());
        }

        //Ajout de la list de string au dropdown
        _dropdown.AddOptions(AlgoStringList);
    }

    public void SetPathfinding(int dropdownIndex)
    {
        Algo SelectedAlgoFromDropdown = AlgoList[dropdownIndex];
        _pathfinding.currentAlgo = SelectedAlgoFromDropdown;
    }
}
