using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsController : MonoBehaviour
{
    [SerializeField] private InputController _inputController;

    [SerializeField] private GameObject _testSpell;

    private void Awake()
    {
        _inputController.Cast1Spell.AddListener(Cast1Spell);
        _inputController.Cast2Spell.AddListener(Cast2Spell);
        _inputController.Cast3Spell.AddListener(Cast3Spell);
        _inputController.Cast4Spell.AddListener(Cast4Spell);
    }

    private void Cast1Spell(Unit startUnit, RaycastHit position)
    {
        startUnit.Cooldown[0] = startUnit.Spells[0].Cooldown;
        startUnit.ApplyManaDamage(startUnit.Spells[0].ManaCost);

        //if (startUnit.Spells[0].IsCon)
        //{
            GameObject spell = Instantiate(_testSpell,startUnit.transform.position,Quaternion.LookRotation(position.point - startUnit.transform.position) * Quaternion.Euler(90,0,0),null);
            StartCoroutine(spell.GetComponent<DragonSlave>().DragonSlaveCoroutine(position.point));         
        //}
    }

    private void Cast2Spell(Unit startUnit, RaycastHit position)
    {
        startUnit.Cooldown[1] = startUnit.Spells[1].Cooldown;
        startUnit.ApplyManaDamage(startUnit.Spells[1].ManaCost);
    }

    private void Cast3Spell(Unit startUnit, RaycastHit position)
    {
        startUnit.Cooldown[2] = startUnit.Spells[2].Cooldown;
        startUnit.ApplyManaDamage(startUnit.Spells[2].ManaCost);
    }

    private void Cast4Spell(Unit startUnit, RaycastHit position)
    {
        startUnit.Cooldown[3] = startUnit.Spells[3].Cooldown;
        startUnit.ApplyManaDamage(startUnit.Spells[3].ManaCost);
    }
}
