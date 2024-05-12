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

    private void Cast1Spell(Unit StartUnit, RaycastHit position)
    {

    }

    private void Cast2Spell(Unit StartUnit, RaycastHit position)
    {

    }

    private void Cast3Spell(Unit StartUnit, RaycastHit position)
    {

    }

    private void Cast4Spell(Unit StartUnit, RaycastHit position)
    {

    }
}
