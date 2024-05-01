using TMPro;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI _time;

    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _heroImage;
    [SerializeField] private GameObject _spels;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private TextMeshProUGUI _healtUI;
    [SerializeField] private TextMeshProUGUI _manaUI;
    [SerializeField] private UnityEngine.UI.Image _healtImage;
    [SerializeField] private UnityEngine.UI.Image _manaImage;

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

    public void HideUI()
    {
        _healtUI.text = "";
        _manaUI.text = "";
        _healtImage.gameObject.transform.parent.gameObject.SetActive(false);
        _manaImage.gameObject.transform.parent.gameObject.SetActive(false);
        _spels.SetActive(false);
        _heroImage.SetActive(false);
        _inventory.SetActive(false);

    }

    public void ShowUI()
    {
        _healtImage.gameObject.transform.parent.gameObject.SetActive(true);
        _manaImage.gameObject.transform.parent.gameObject.SetActive(true);
        _spels.SetActive(true);
        _heroImage.SetActive(true);
        _inventory.SetActive(true);
    }

    public void ChangeUI(int healt,int maxHealth, int mana, int maxMana)
    {
        _healtUI.text = healt + "/" + maxHealth;
        _manaUI.text = mana + "/" + maxMana;

        _healtImage.fillAmount = healt / maxHealth;
        _manaImage.fillAmount = mana / maxMana;
    }
}
