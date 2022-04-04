using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    [SerializeField] private GameParameters parameters;
    [SerializeField] private Score score;

    private readonly Dictionary<Team, int> _tankPerTeamIn = new Dictionary<Team, int>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) { return; }
        if (!collision.gameObject.CompareTag(parameters.TagTank)) { return; }
        var team = collision.gameObject.GetComponent<Tank>().Team;
        if (_tankPerTeamIn.ContainsKey(team))
        {
            _tankPerTeamIn[team] += 1;
        }
        else
        {
            _tankPerTeamIn.Add(team, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null) { return; }
        if (!collision.gameObject.CompareTag(parameters.TagTank)) { return; }
        var team = collision.gameObject.GetComponent<Tank>().Team;
        _tankPerTeamIn[team] -= 1;
        if (_tankPerTeamIn[team] == 0)
        {
            _tankPerTeamIn.Remove(team);
        }
    }

    private void Update()
    {
        if (_tankPerTeamIn.Count == 1)
        {
            Capture();
        } 
        else if (_tankPerTeamIn.Count > 1)
        {
            score.Conflict();
        }
        else 
        {
            score.Nobody();
        }
    }

    private void Capture()
    {
        Team team = null;
        foreach (var item in _tankPerTeamIn)
        {
            team = item.Key;
        }
        score.Capture(team);
    }
}
