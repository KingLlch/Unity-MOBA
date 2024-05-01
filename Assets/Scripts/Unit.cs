using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IDamageable, IHealable
{
    [field: SerializeField] public float Health { get; private set; } = 100;

    public void ApplyDamage(int damage)
    {
        Health -= damage;
    }

    public void ApplyHeal(int heal)
    {
        Health += heal;
    }
}
