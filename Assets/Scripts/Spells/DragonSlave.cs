using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragonSlave : MonoBehaviour
{
    public IEnumerator DragonSlaveCoroutine(Vector3 target)
    {
         Vector3 startPosition = transform.position;

        while (true) 
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3 ((startPosition + ((target - startPosition).normalized * 20)).x,transform.position.y, (startPosition + ((target - startPosition).normalized * 20)).z), 1);
            transform.localScale += Vector3.right * 0.1f;

            if (Vector3.Distance(startPosition, transform.position) >= 10)
            {
                Destroy(gameObject);
                yield break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
