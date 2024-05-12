using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsController : MonoBehaviour
{
    [SerializeField] private InputController _inputController;

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
    }

    private void Cast2Spell(Unit startUnit, RaycastHit position)
    {
        startUnit.Cooldown[1] = startUnit.Spells[1].Cooldown;
    }

    private void Cast3Spell(Unit startUnit, RaycastHit position)
    {
        startUnit.Cooldown[2] = startUnit.Spells[2].Cooldown;
    }

    private void Cast4Spell(Unit startUnit, RaycastHit position)
    {
        startUnit.Cooldown[3] = startUnit.Spells[3].Cooldown;
    }
}
