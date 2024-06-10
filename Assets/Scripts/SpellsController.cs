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
            GameObject spell = Instantiate(_dragonSlave,startUnit.transform.position, Quaternion.Euler(new Vector3 (90, 0, -(Quaternion.LookRotation(position.point - startUnit.transform.position).eulerAngles.y))), null);
            spell.GetComponentInChildren<DragonSlave>().StartUnit = startUnit;
            StartCoroutine(spell.GetComponentInChildren<DragonSlave>().Spell(position.point));         
        }
    }
}
