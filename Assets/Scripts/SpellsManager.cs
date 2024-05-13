using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct Spell
{
    public string Name;
    public string Description;


    public int Damage;
    public int Cooldown;
    public int ManaCost;
    public float CastDelay;

    public bool IsCon;
    public float ConAngle;

    public bool IsSphere;
    public float SphereRadius;

    public bool IsLine;
    public float LineWidtn;
    public float LineDistance;

    public bool IsActive;
    public bool IsPassive;


    public Spell(string name, string description,
        int damage, int cooldown, int manaCost,
        float castDelay, 
        bool isActive = false, bool isPassive = false, bool isSwitchable = false,
        bool isCon = false, float conAngle = 0, 
        bool isSphere = false, float sphereRadius = 0, 
        bool isLine = false, float lineWidth = 0, float lineDistance = 0)
    {
        Name = name;
        Description = description;

        Damage = damage;
        Cooldown = cooldown;
        ManaCost = manaCost;
        CastDelay = castDelay;

        IsCon = isCon;  
        ConAngle = conAngle;

        IsSphere = isSphere;
        SphereRadius = sphereRadius;

        IsLine = isLine;
        LineWidtn = lineWidth;
        LineDistance = lineDistance;

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
            1, true, false, true));




        SpellList.LinaSpells.Add(new Spell("Light Strike Array", "/", 1, 1, 10, 1, true, false, true));
        SpellList.LinaSpells.Add(new Spell("Fiery Soul", "/", 1, 10, 10, 1, false, true, true));
        SpellList.LinaSpells.Add(new Spell("Laguna Blade", "/", 1, 10, 10, 1, true, false, true));
    }
}
