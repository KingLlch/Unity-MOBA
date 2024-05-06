using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using UnityEditor.SceneManagement;

public class Unit : MonoBehaviour, IDamageable, IHealable
{
    [field: SerializeField] public int Health { get; private set; } = 100;
    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public int HealthRegen { get; private set; }

    [field: SerializeField] public int Mana { get; private set; } = 100;
    [field: SerializeField] public int MaxMana { get; private set; }
    [field: SerializeField] public int ManaRegen { get; private set; }


    [field: SerializeField] public int Damage { get; private set; } = 5;
    [field: SerializeField] public int AttackSpeed { get; private set; } = 50;
    [field: SerializeField] public int AttackRange { get; private set; } = 50;
    [field: SerializeField] public int ParticleSpeed { get; private set; } = 10;

    [SerializeField] private GameObject AttackParticlePrefab;

    private void Awake()
    {
        MaxHealth = Health;
        MaxMana = Mana;

        StartCoroutine(RegenCorutine());
    }

    public void ApplyDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyHeal(int heal)
    {
        Health += heal;
    }

    public void ApplyManaHeal(int mana)
    {
        Mana += mana;
    }

    public void Attack(Transform target,float distance)
    {
        StartCoroutine(AttackCorutine(target, distance));
    }

    public IEnumerator AttackCorutine(Transform target, float distance)
    {
        while ((true) && (distance <= AttackRange))
        {
            GameObject attackParticle;

            attackParticle = Instantiate(AttackParticlePrefab,transform.position,Quaternion.identity,null);
            attackParticle.GetComponent<AttackParticle>().target = target;
            attackParticle.GetComponent<AttackParticle>().startUnit = transform.GetComponent<Unit>();
            attackParticle.transform.DOMove(target.position, distance / ParticleSpeed);

            yield return new WaitForSeconds(100/AttackSpeed);
        }
    }

    public IEnumerator RegenCorutine()
    {
        while (true)
        {
            ApplyHeal(HealthRegen);
            ApplyManaHeal(ManaRegen);

            yield return new WaitForSeconds(1f);
        }
    }
}
