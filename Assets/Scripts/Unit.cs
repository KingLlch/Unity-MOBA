using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    [field: SerializeField] public int AttackRange { get; private set; } = 10;
    [field: SerializeField] public int ParticleSpeed { get; private set; } = 10;

    [SerializeField] private GameObject AttackParticlePrefab;
    public bool IsAttacking;

    [HideInInspector] public UnityEvent<Unit> ChangeHealthOrMana;

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

        ChangeHealthOrMana.Invoke(GetComponent<Unit>());
    }

    public void ApplyHeal(int heal)
    {
        Health += heal;
        ChangeHealthOrMana.Invoke(GetComponent<Unit>());
    }

    public void ApplyManaHeal(int mana)
    {
        Mana += mana;
        ChangeHealthOrMana.Invoke(GetComponent<Unit>());
    }

    public IEnumerator AttackCorutine(GameObject target)
    {
        while ((true) && (Vector3.Distance(gameObject.transform.position, target.transform.position) <= AttackRange))
        {
            IsAttacking = true;
            GameObject attackParticle = Instantiate(AttackParticlePrefab,transform.position,Quaternion.identity,null);
            attackParticle.GetComponent<AttackParticle>().target = target.transform;
            attackParticle.GetComponent<AttackParticle>().startUnit = transform.GetComponent<Unit>();
            attackParticle.transform.DOMove(target.transform.position, Vector3.Distance(gameObject.transform.position, target.transform.position) / ParticleSpeed);

            yield return new WaitForSeconds(100/AttackSpeed);
        }
    }

    public IEnumerator RegenCorutine()
    {
        while (true)
        {
            if (Health<MaxHealth) ApplyHeal(HealthRegen);
            if (Mana < MaxMana) ApplyManaHeal(ManaRegen);

            ChangeHealthOrMana.Invoke(GetComponent<Unit>());

            yield return new WaitForSeconds(1f);
        }
    }
}
