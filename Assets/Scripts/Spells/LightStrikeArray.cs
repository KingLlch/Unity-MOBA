using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStrikeArray : MonoBehaviour
{
    [HideInInspector] public Unit StartUnit;
    private List<Unit> _targets = new List<Unit>();

    public IEnumerator Spell(Vector3 target)
    {
        yield return new WaitForSeconds(StartUnit.Spells[1].CastDelay);

        //transform.position = target;
        transform.GetComponent<CapsuleCollider>().enabled = true;

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        yield break;
    }

    private void OnTriggerEnter(Collider target)
    {

        if (target.GetComponent<Unit>() && target.gameObject.tag != "Player" && !_targets.Contains(target.GetComponent<Unit>()))
        {
            _targets.Add(target.GetComponent<Unit>());
            target.gameObject.GetComponent<Unit>().ApplyHealthDamage(StartUnit.Spells[1].Damage);
        }
    }
}
