using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonSlave : MonoBehaviour
{
    [HideInInspector] public Unit StartUnit;
    private List<Unit> _targets = new List<Unit>();

    [SerializeField] private GameObject _spell;
    [SerializeField] private float _speedChangeSize = 0.1f;
    [SerializeField] private float _speedSpell = 0.5f;
    private float _spellRange;

    [SerializeField] private SpriteRenderer _spellShader;
    private float _shaderGradient = 0.3f;

    public IEnumerator Spell(Vector3 target)
    {
        Vector3 startPosition = transform.position;
        _spellRange = StartUnit.Spells[0].CastRange;
        _spellShader.gameObject.transform.localPosition = new Vector3(0.25f, _spellRange/10*6, 0);
        _spellShader.gameObject.transform.localScale = new Vector3(1, _spellRange/10, 1);


        while (true) 
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition + ((new Vector3 (target.x, startPosition.y, target.z) - startPosition).normalized * _spellRange), _speedSpell);
            transform.localScale += Vector3.right * _speedChangeSize;      
            _shaderGradient += (1.7f / (_spellRange / (_speedSpell/2)));
            _spellShader.material.SetFloat("_Gradient", _shaderGradient);

            if (Vector3.Distance(startPosition, transform.position) >= _spellRange - 0.1f)
            {
                Destroy(_spell);
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
            target.gameObject.GetComponent<Unit>().ApplyHealthDamage(StartUnit.Spells[0].Damage);
        }
    }
}
