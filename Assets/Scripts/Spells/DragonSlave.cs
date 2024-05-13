using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragonSlave : MonoBehaviour
{
    private List<Unit> _targets = new List<Unit>();

    public IEnumerator DragonSlaveCoroutine(Vector3 target)
    {
         Vector3 startPosition = transform.position;

        while (true) 
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3 ((startPosition + ((target - startPosition).normalized * 20)).x,transform.position.y, (startPosition + ((target - startPosition).normalized * 20)).z), 0.5f);
            transform.localScale += Vector3.right * 0.05f;

            if (Vector3.Distance(startPosition, transform.position) >= 10)
            {
                Destroy(gameObject);
                yield break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter(Collider target)
    {

        if (target.GetComponent<Unit>() && target.gameObject.tag != "Player" && !_targets.Contains(target.GetComponent<Unit>()))
        {
            _targets.Add(target.GetComponent<Unit>());

            Debug.Log("DealDamage");
            target.gameObject.GetComponent<Unit>().ApplyHealthDamage(SpellList.LinaSpells[0].Damage);
        }
    }
}
