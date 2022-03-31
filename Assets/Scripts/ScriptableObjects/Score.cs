using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Score", menuName = "Score")]
public class Score : ScriptableObject
{
    [SerializeField] float _timeToScore = 10f;

    public TeamScore _playerScore;
    public TeamScore _enemyScore;

    public float _progression { set; get; }
    public Team _teamScoring { set; get; }

    public void Capture(Team team)
    {
        if (_teamScoring == null) { _teamScoring = team; }

        if (_teamScoring != team)
        {
            _progression -= Time.deltaTime * 100/_timeToScore;
            if (_progression <= 0)
            {
                _progression = 0;
                _teamScoring = team;
            }
        } else
        {
            if (_progression < 100)
            {
                _progression += Time.deltaTime * 100 / _timeToScore;
            } else
            {
                _progression = 100;
                if (team == _playerScore.Team)
                {
                    _playerScore.Score += Time.deltaTime;
                } else
                {
                    _enemyScore.Score += Time.deltaTime;
                }
            }
        }
    }

    public void Conflict()
    {

    }

    public void Nobody()
    {
        if (_progression > 0)
        {
            _progression -= Time.deltaTime * 100 / _timeToScore / 2;

            if (_progression < 0) { _progression = 0; }
        }
    }
}
