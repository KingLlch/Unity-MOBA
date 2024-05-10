using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private ParticleManager _particleManager;

    private Camera _mainCamera;
    private GameObject _player;
    private bool _isHeroSelect = true;
    private Unit selectedUnit;

    [HideInInspector] public UnityEvent<Unit> SelectUnit;
    [HideInInspector] public UnityEvent<Unit> ChangeSelectUnit;
    [HideInInspector] public UnityEvent DeSelectUnit;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _player = GameObject.Find("Player");
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

        if (Input.GetMouseButtonDown(1) && _isHeroSelect)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);

            if (hit.collider.gameObject.GetComponent<Unit>() && (hit.collider.gameObject != _player.gameObject))
            {
                _player.GetComponent<Unit>().MoveToAttack(hit.collider.gameObject);
                _particleManager.AttackParticle(hit.point);
            }
            else
            {
                _player.GetComponent<Unit>().Move(hit.point);
                _particleManager.MoveParticle(hit.point);
            }
        }

        if (Input.GetKeyDown(KeyCode.A) && _isHeroSelect)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);

            Collider[] nearObjects = Physics.OverlapSphere(hit.point, _player.GetComponent<Unit>().AttackRange);
            Collider nearestUnit = null;

            foreach (Collider collider in nearObjects)
            {
                if (collider.GetComponent<Unit>() && collider.gameObject != _player)
                {              
                    if (nearestUnit == null)
                    nearestUnit = collider;

                    else if (Vector3.Distance(collider.transform.position, hit.point) < Vector3.Distance(nearestUnit.transform.position, hit.point))
                    {
                        nearestUnit = collider;
                    }
                }
            }

            if (nearestUnit != null)
            {
                _player.GetComponent<Unit>().MoveToAttack(nearestUnit.gameObject);
            }
            else
            {
                _player.GetComponent<Unit>().Move(hit.point);
            }

            _particleManager.AttackParticle(hit.point);

        }

        if (Input.GetKeyDown(KeyCode.M) && _isHeroSelect)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);
            _player.GetComponent<Unit>().Move(hit.point);
            _particleManager.MoveParticle(hit.point);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(ray, out hit);
            Unit unit = null;

            if (hit.collider.GetComponent<Unit>())
            {
                unit = hit.collider.GetComponent<Unit>();
            }

            if (unit != null)
            {
                if (selectedUnit == null)
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
