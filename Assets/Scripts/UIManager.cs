using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private InputController _inputController;
    private TextMeshProUGUI _time;

    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _heroImage;
    [SerializeField] private GameObject _spels;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private TextMeshProUGUI _healtUI;
    [SerializeField] private TextMeshProUGUI _manaUI;
    [SerializeField] private TextMeshProUGUI _healtRegenUI;
    [SerializeField] private TextMeshProUGUI _manaRegenUI;
    [SerializeField] private UnityEngine.UI.Image _healtImage;
    [SerializeField] private UnityEngine.UI.Image _manaImage;

    private void Awake()
    {
        _time = GameObject.Find("MainCanvas/MainPanel/TimePanel/TimeText").GetComponent<TextMeshProUGUI>();
        _inputController = FindObjectOfType<InputController>();
        _inputController.SelectUnit.AddListener(SelectUnit);
        _inputController.ChangeSelectUnit.AddListener(ChangeSelectUnit);
        _inputController.DeSelectUnit.AddListener(DeSelectUnit);
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

        _healtRegenUI.text = "";
        _manaRegenUI.text = "";

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

    public void ChangeUI(Unit unit)
    {
        _healtUI.text = unit.Health + "/" + unit.MaxHealth;
        _manaUI.text = unit.Mana + "/" + unit.MaxMana;

        _healtRegenUI.text = "+" + unit.HealthRegen;
        _manaRegenUI.text = "+" + unit.ManaRegen;

        if (unit.MaxHealth != 0)
            _healtImage.fillAmount = unit.Health / unit.MaxHealth;
        if (unit.MaxMana != 0)
            _manaImage.fillAmount = unit.Mana / unit.MaxMana;
    }

    private void SelectUnit(Unit unit)
    {
        ShowUI();
        ChangeUI(unit);
        unit.ChangeHealthOrMana.AddListener(ChangeUI);
    }

    private void ChangeSelectUnit(Unit unit)
    {
        unit.ChangeHealthOrMana.RemoveAllListeners();
    }

    private void DeSelectUnit()
    {
        HideUI();
    }
}
