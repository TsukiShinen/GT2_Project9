using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowScore : MonoBehaviour
{
    [SerializeField] private TMP_Text redName;
    [SerializeField] private TMP_Text blueName;
    [SerializeField] private TMP_Text redScore;
    [SerializeField] private TMP_Text blueScore;
    [SerializeField] private TMP_Text result;

    [SerializeField] private Score score;
    [SerializeField] private Team redTeam;
    [SerializeField] private Team blueTeam;

    void Start()
    {
        redName.text = redTeam.Name.ToString();
        blueName.text = blueTeam.Name.ToString();
        redScore.text = score.enemyScore.score.ToString();
        blueScore.text = score.playerScore.score.ToString();

        if(score.enemyScore.score > score.playerScore.score)
        {
            result.text = "Défaite";
        }
        else if(score.enemyScore.score < score.playerScore.score)
        {
            result.text = "Victoire";
        }
        else
        {
            result.text = "Egalite";
        }
    }
}
