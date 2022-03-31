using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaptureView : MonoBehaviour
{
    [SerializeField] Score _score;
    [Space(10)]
    [SerializeField] TMP_Text _text;
    [SerializeField] Image _image;

    public void Update()
    {
        Team team = _score._teamScoring;
        
        if (team == null)
        {
            _image.color = Color.white;
            _image.fillAmount = 0;
            return;
        }

        _image.color = team.Color;
        Debug.Log(team.Color);
        _image.fillAmount = _score._progression / 100f;
        _text.text = Mathf.FloorToInt(_score._progression).ToString();
    }
}
