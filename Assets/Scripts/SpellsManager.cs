using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct Spell
{
    public string Name;
    public string Description;


    public int Damage;
    public float Duration;
    public float Cooldown;
    public float Characteristic;

    public bool IsCon;
    public bool IsSphere;
    public bool IsLine;

    public bool IsActive;
    public bool IsPassive;


    public Spell(string name, string description, int damage, float duration, float cooldown,
        float characteristic, bool isActive = false, bool isPassive = false, bool isCon = false, bool isSphere = false, bool isLine = false)
    {
        Name = name;
        Description = description;

        Damage = damage;
        Duration = duration;
        Cooldown = cooldown;
        Characteristic = characteristic;

        IsCon = isCon;  
        IsSphere = isSphere;
        IsLine = isLine;

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
        SpellList.LinaSpells.Add(new Spell("Dragon Slave", "/", 1, 1, 10, 1, true, false, true));
        SpellList.LinaSpells.Add(new Spell("Light Strike Array", "/", 1, 1, 10, 1, true, false, true));
        SpellList.LinaSpells.Add(new Spell("Fiery Soul", "/", 1, 1, 10, 1, false, true, true));
        SpellList.LinaSpells.Add(new Spell("Laguna Blade", "/", 1, 1, 10, 1, true, false, true));
    }
}
