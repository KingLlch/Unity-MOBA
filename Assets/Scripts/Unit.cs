using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

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

    private Coroutine _attackCoroutine;
    private Coroutine _moveAttackCoroutine;

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

    public void MoveToAttack(GameObject target)
    {
        if (_moveAttackCoroutine == null)
            _moveAttackCoroutine = StartCoroutine(MoveAttackCorutine(target));
        else
        {
            StopCoroutine(_moveAttackCoroutine);
            _moveAttackCoroutine = StartCoroutine(MoveAttackCorutine(target));
        }
    }
    public void Attack(GameObject target)
    {
        if (_attackCoroutine == null)
            _attackCoroutine = StartCoroutine(AttackCorutine(target));
        else
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = StartCoroutine(AttackCorutine(target));
        }
    }

    public IEnumerator AttackCorutine(GameObject target)
    {
        while (true)
        {
            if (Vector3.Distance(gameObject.transform.position, target.transform.position) <= AttackRange)
            {
                GameObject attackParticle = Instantiate(AttackParticlePrefab, transform.position, Quaternion.identity, null);
                attackParticle.GetComponent<AttackParticle>().target = target.transform;
                attackParticle.GetComponent<AttackParticle>().startUnit = transform.GetComponent<Unit>();
                attackParticle.transform.DOMove(target.transform.position, Vector3.Distance(gameObject.transform.position, target.transform.position) / ParticleSpeed);

                yield return new WaitForSeconds(100 / AttackSpeed);
            }
        }
    }

    public IEnumerator MoveAttackCorutine(GameObject target)
    {
        IsAttacking = true;

        while (true)
        {
            if (Vector3.Distance(gameObject.transform.position, target.transform.position) <= AttackRange)
            {
                if (_attackCoroutine != null)
                {
                    StopCoroutine(_attackCoroutine);
                    _attackCoroutine = null;
                }

                Vector3 point = target.transform.position - ((target.transform.position - transform.position).normalized * AttackRange);
                gameObject.GetComponent<NavMeshAgent>().SetDestination(new Vector3(point.x, gameObject.transform.position.y, point.z));

                yield return new WaitForSeconds(5f);
            }

            else
            {
                Attack(target);
            }
        }
    }

    public IEnumerator RegenCorutine()
    {
        while (true)
        {
            if (Health < MaxHealth) ApplyHeal(HealthRegen);
            if (Mana < MaxMana) ApplyManaHeal(ManaRegen);

            ChangeHealthOrMana.Invoke(GetComponent<Unit>());

            yield return new WaitForSeconds(1f);
        }
    }

    public void Move(Vector3 target)
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
        if (_moveAttackCoroutine != null)
        {
            StopCoroutine(_moveAttackCoroutine);
            _moveAttackCoroutine = null;
        }

        gameObject.GetComponent<NavMeshAgent>().SetDestination(new Vector3(target.x, gameObject.transform.position.y, target.z));
    }
}
