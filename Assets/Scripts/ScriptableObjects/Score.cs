using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Score", menuName = "Score")]
public class Score : ScriptableObject
{
    [SerializeField] float _timeToScore = 10f;

    public float _progression = 0;
    public Team _teamScoring;

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
            _progression += Time.deltaTime * 100 / _timeToScore;
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
