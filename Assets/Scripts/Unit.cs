using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IDamageable, IHealable
{
    [field: SerializeField] public int Health { get; private set; } = 100;
    [field: SerializeField] public int MaxHealth { get; private set; }

    [field: SerializeField] public int Mana { get; private set; } = 100;
    [field: SerializeField] public int MaxMana { get; private set; }

    [field: SerializeField] public int Damage { get; private set; } = 5;
    [field: SerializeField] public int AttackSpeed { get; private set; } = 50;
    [field: SerializeField] public int AttackRange { get; private set; } = 50;

    private void Awake()
    {
        MaxHealth = Health;
        MaxMana = Mana;
    }

    public void ApplyDamage(int damage)
    {
        Health -= damage;
    }

    public void ApplyHeal(int heal)
    {
        Health += heal;
    }
}
