using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            Destroy(gameObject);    
        }
    }
}
