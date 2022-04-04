using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{

    [SerializeField] private Score score;
    [Space(10)]
    [SerializeField] private TMP_Text scoreTeamPlayer;
    [SerializeField] private TMP_Text scoreTeamEnemy;

    public void Update()
    {
        scoreTeamPlayer.text = Mathf.FloorToInt(score._playerScore.score).ToString();
        scoreTeamEnemy.text = Mathf.FloorToInt(score._enemyScore.score).ToString();
    }
}
