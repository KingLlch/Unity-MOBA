using DG.Tweening;
using UnityEngine;

public class AttackParticle : MonoBehaviour
{
    public Unit startUnit;
    public Transform target;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            target.GetComponent<Unit>().ApplyDamage(startUnit.Damage);
            gameObject.transform.DOKill();
            Destroy(gameObject);    
        }
    }
}
