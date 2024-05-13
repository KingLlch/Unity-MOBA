using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AttackParticle : MonoBehaviour
{
    [HideInInspector] public GameObject Target;
    [HideInInspector] public Unit StartUnit; 
    [HideInInspector] public int Speed;

    private bool destroy;

    public IEnumerator MoveParticle()
    {
        while (true && !destroy)
        {
            gameObject.transform.DOMove(Target.transform.position, Vector3.Distance(gameObject.transform.position, Target.transform.position) / Speed);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject == Target.gameObject)
        {
            destroy = true;
            gameObject.transform.DOKill();
            Target.GetComponent<Unit>().ApplyHealthDamage(StartUnit.Damage);
            StopCoroutine(MoveParticle());
            Destroy(gameObject);
        }
    }
}
