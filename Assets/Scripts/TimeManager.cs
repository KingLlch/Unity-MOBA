using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;

    public int TimeSeconds;
    public int TimeMinuts;

    public bool IsPaused;

    private void Awake()
    {
        StartCoroutine(TimeChange());
    }

    private IEnumerator TimeChange()
    {
        if (IsPaused) { yield return null; }
        while (true)
        {
            TimeSeconds++;
            if (TimeSeconds >= 60)
            {
                TimeSeconds = 0;
                TimeMinuts++;
            }

            _uiManager.ChangeTimeUI(TimeSeconds, TimeMinuts);
            yield return new WaitForSeconds(1);
        }
    }
}
