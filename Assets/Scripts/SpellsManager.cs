using System.Collections.Generic;
using UnityEngine;

public struct Spell
{
    public string Name;
    public string Description;


    public int Damage;
    public int Cooldown;
    public int ManaCost;
    public float CastRange;
    public float CastDelay;

    public bool IsActive;
    public bool IsPassive;


    public Spell(string name, string description,
        int damage, int cooldown, int manaCost,
        float castDelay, float castRange,
        bool isActive = false, bool isPassive = false, bool isSwitchable = false)
    {
        Name = name;
        Description = description;

        Damage = damage;
        Cooldown = cooldown;
        ManaCost = manaCost;
        CastDelay = castDelay;
        CastRange = castRange;

        IsActive = isActive;
        IsPassive = isPassive;
    }

}

public static class SpellList
{
    public static List<Spell> LinaSpells = new List<Spell>();
}

public class SpellsManager : MonoBehaviour
{
    private void Awake()
    {
        SpellList.LinaSpells.Add(new Spell("Dragon Slave", "/", 
            100, 0, 1, 
            1, 10, true, false, true));




        SpellList.LinaSpells.Add(new Spell("Light Strike Array", "/", 1, 1, 10, 1, 1, true, false, true));
        SpellList.LinaSpells.Add(new Spell("Fiery Soul", "/", 1, 10, 10, 1, 1, false, true, true));
        SpellList.LinaSpells.Add(new Spell("Laguna Blade", "/", 1, 10, 10, 1, 1, true, false, true));
    }
}
