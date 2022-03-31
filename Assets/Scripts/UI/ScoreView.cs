using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{

    [SerializeField] Score _score;
    [Space(10)]
    [SerializeField] TMP_Text _scoreTeamPlayer;
    [SerializeField] TMP_Text _scoreTeamEnemy;

    public void Update()
    {
        _scoreTeamPlayer.text = Mathf.FloorToInt(_score._playerScore.Score).ToString();
        _scoreTeamEnemy.text = Mathf.FloorToInt(_score._enemyScore.Score).ToString();
    }
}
