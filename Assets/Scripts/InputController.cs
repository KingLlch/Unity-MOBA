using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Collections;

public class InputController : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private UIManager _UIManager;

    private Camera _mainCamera;
    private NavMeshAgent _player;
    private bool _isHeroSelect = true;
    private Unit selectedUnit;

    [HideInInspector] public UnityEvent<Unit> SelectUnit;
    [HideInInspector] public UnityEvent<Unit> ChangeSelectUnit;
    [HideInInspector] public UnityEvent DeSelectUnit;

    private Coroutine AttackCoroutine;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _player = GameObject.Find("Player").GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            if (_timeManager.IsPaused == true)
            {
                _timeManager.IsPaused = false;
                Time.timeScale = 1;
            }

            else
            {
                _timeManager.IsPaused = true;
                Time.timeScale = 0;
            }

            _UIManager.Pause(_timeManager.IsPaused);
        }

        if ((Input.GetMouseButtonDown(1)) && (_isHeroSelect))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);

            if (hit.collider.gameObject.GetComponent<Unit>() && (hit.collider.gameObject != _player.gameObject))
            {
                if (Vector3.Distance(_player.transform.position, hit.collider.gameObject.transform.position) > _player.GetComponent<Unit>().AttackRange)
                {
                    Vector3 point = hit.collider.gameObject.transform.position - ((hit.collider.gameObject.transform.position - _player.transform.position).normalized * _player.GetComponent<Unit>().AttackRange);
                    _player.SetDestination(new Vector3(point.x,0, point.z));
                }

                if (!_player.GetComponent<Unit>().IsAttacking)
                {
                    AttackCoroutine = StartCoroutine(_player.GetComponent<Unit>().AttackCorutine(hit.collider.gameObject));
                }

            }
            else
            {
                if (AttackCoroutine != null)
                {
                    _player.GetComponent<Unit>().StopCoroutine(AttackCoroutine);
                    _player.GetComponent<Unit>().IsAttacking = false;
                }

                _player.SetDestination(hit.point);
            }
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);
            Unit unit = hit.collider.GetComponent<Unit>();

            if (unit)
            {
                if(selectedUnit == null)
                {
                    SelectUnit.Invoke(unit);
                    selectedUnit = unit;
                }
                else if (unit != selectedUnit)
                {
                    ChangeSelectUnit.Invoke(selectedUnit);
                    SelectUnit.Invoke(unit);
                    selectedUnit = unit;
                }

                if (hit.collider.gameObject.name == "Player") _isHeroSelect = true;
            }

            else
            {
                ChangeSelectUnit.Invoke(selectedUnit);
                DeSelectUnit.Invoke();
                selectedUnit = null;
                _isHeroSelect = false;
            }
        }
    }
}
