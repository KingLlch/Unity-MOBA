using UnityEngine;

public class SpellsController : MonoBehaviour
{
    [SerializeField] private GameObject _dragonSlave;
    [SerializeField] private GameObject _lightStrikeArray;

    private void Awake()
    {
        InputController.Instance.CastSpell.AddListener(CastSpell);
    }

    private void CastSpell(Unit startUnit, RaycastHit position, int numberSpell)
    {
        startUnit.Cooldown[numberSpell] = startUnit.Spells[numberSpell].Cooldown;
        startUnit.ApplyManaDamage(startUnit.Spells[numberSpell].ManaCost);

        if ((numberSpell == 0) && startUnit.Spells[numberSpell].Name == "Dragon Slave")
        {
            GameObject spell = Instantiate(_dragonSlave,startUnit.transform.position, Quaternion.LookRotation(position.point - startUnit.transform.position) * Quaternion.Euler(90,0,0), null);
            spell.GetComponent<DragonSlave>().StartUnit = startUnit;
            StartCoroutine(spell.GetComponent<DragonSlave>().Spell(position.point));         
        }
    }
}
