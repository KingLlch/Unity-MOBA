using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI _time;

    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _heroImage;
    [SerializeField] private GameObject _spels;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject[] _spelsDescription;

    [SerializeField] private TextMeshProUGUI _healtUI;
    [SerializeField] private TextMeshProUGUI _manaUI;
    [SerializeField] private TextMeshProUGUI _healtRegenUI;
    [SerializeField] private TextMeshProUGUI _manaRegenUI;
    [SerializeField] private TextMeshProUGUI[] _spellsNameUI;
    [SerializeField] private TextMeshProUGUI[] _spellsDescriptionUI;

    [SerializeField] private UnityEngine.UI.Image[] _spelsCooldown;
    [SerializeField] private UnityEngine.UI.Image _healtImage;
    [SerializeField] private UnityEngine.UI.Image _manaImage;

    private void Awake()
    {
        _time = GameObject.Find("MainCanvas/MainPanel/TimePanel/TimeText").GetComponent<TextMeshProUGUI>();
        InputController.Instance.SelectUnit.AddListener(SelectUnit);
        InputController.Instance.ChangeSelectUnit.AddListener(ChangeSelectUnit);
        InputController.Instance.DeSelectUnit.AddListener(DeSelectUnit);
    }

    public void ChangeTimeUI(int timeSeconds, int timeMinuts)
    {
        if ((timeSeconds < 10) && (timeMinuts < 10)) _time.text = "0" + timeMinuts + ":0" + timeSeconds;
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
            _healtImage.fillAmount = (float) unit.Health / unit.MaxHealth;
        if (unit.MaxMana != 0)
            _manaImage.fillAmount = (float) unit.Mana / unit.MaxMana;

        for (int i = 0; i < _spellsNameUI.Length; i++)
        {
            _spellsNameUI[i].text = unit.Spells[i].Name;
            _spellsDescriptionUI[i].text = unit.Spells[i].Description;
        }
    }

    private void SelectUnit(Unit unit)
    {
        ShowUI();
        ChangeUI(unit);
        unit.ChangeHealthOrMana.AddListener(ChangeUI);
        unit.ChangeCooldown.AddListener(ChangeCooldown);
    }

    private void ChangeSelectUnit(Unit unit)
    {
        if (unit != null)
        {
            unit.ChangeHealthOrMana.RemoveAllListeners();
            unit.ChangeCooldown.RemoveAllListeners();
        }
    }

    private void DeSelectUnit()
    {
        HideUI();
    }

    private void ChangeCooldown(Unit unit)
    {
        for (int i = 0; i < unit.Spells.Count; i++)
        {
            _spelsCooldown[i].fillAmount = (float) unit.Cooldown[i] / unit.Spells[i].Cooldown;
        }
    }

    public void ShowDescription(int number)
    {
        _spelsDescription[number].SetActive(true);
    }

    public void HideDescription(int number)
    {
        _spelsDescription[number].SetActive(false);
    }
}
