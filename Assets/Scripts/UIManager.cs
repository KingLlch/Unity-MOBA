using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI _time;

    [SerializeField] private GameObject _pausePanel;

    private void Awake()
    {
        _time = GameObject.Find("MainCanvas/MainPanel/TimePanel/TimeText").GetComponent<TextMeshProUGUI>();
    }

    public void ChangeTimeUI(int timeSeconds, int timeMinuts)
    {
        if ((timeSeconds < 10) && (timeMinuts<10)) _time.text = "0" + timeMinuts + ":0" + timeSeconds;
        else if (timeMinuts < 10) _time.text = "0" + timeMinuts + ":" + timeSeconds;
        else if (timeSeconds < 10) _time.text = timeMinuts + ":0" + timeSeconds;
        else _time.text = timeMinuts + ":" + timeSeconds;
    }

    public void Pause(bool isPaused)
    {
        _pausePanel.SetActive(isPaused);
    }
}
