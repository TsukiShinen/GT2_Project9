using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Score", menuName = "Score")]
public class Score : ScriptableObject
{
    public float timeToScore = 10f;

    public TeamScore playerScore;
    public TeamScore enemyScore;

    public float Progression { set; get; }
    public Team TeamScoring { set; get; }

    public void Clear()
    {
        playerScore.score = 0;
        enemyScore.score = 0;

        TeamScoring = null;
        Progression = 0;
    }
}
