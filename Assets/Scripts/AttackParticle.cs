using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackParticle : MonoBehaviour
{
    public Unit startUnit;
    public Transform target;

    private void Update()
    {
        if(transform == target)
        {
            target.GetComponent<Unit>().ApplyDamage(startUnit.Damage);
            Destroy(gameObject);
        }
    }
}
