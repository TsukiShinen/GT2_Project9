using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chrono : MonoBehaviour
{
    [SerializeField] private GameParameters parameters;
    private float _timer;
    private TMP_Text _text;
    void Start()
    {
        _timer = parameters.MatchTimeLimit;
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_timer > 0)
        {
            ShowTime();
            _timer -= Time.deltaTime;
        }
        else
        {

        }
    }

    void ShowTime()
    {
        int minutes = Mathf.FloorToInt(_timer / 60);
        int seconds = Mathf.FloorToInt(_timer - (minutes*60));
        string secondsString = seconds.ToString();
        if(seconds < 10)
        {
            secondsString = "0" + secondsString;
        }
        _text.text = minutes.ToString() + " : " + secondsString + " UWU";
    }
}
